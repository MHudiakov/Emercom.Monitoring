using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace xGraphic
{
    public class Connector : ContentControl, INotifyPropertyChanged
    {
        
        // drag start point, relative to the DesignerCanvas
        private Point? dragStartPoint = null;

        public ConnectorOrientation Orientation { get; set; }

        // center position of this Connector relative to the DesignerCanvas
        private Point position;
        public Point Position
        {
            get { return position; }
            set
            {
                if (position != value)
                {
                    position = value;
                    OnPropertyChanged("Position");
                }
            }
        }

        // the DesignerItem this Connector belongs to;
        // retrieved from DataContext, which is set in the
        // DesignerItem template
        private DesignerItem parentDesignerItem;
        public DesignerItem ParentDesignerItem
        {
            get
            {
                if (parentDesignerItem == null)
                    parentDesignerItem = this.DataContext as DesignerItem;

                return parentDesignerItem;
            }
        }

        // keep track of connections that link to this connector
        private List<Connection> connections;
        public List<Connection> Connections
        {
            get
            {
                if (connections == null)
                    connections = new List<Connection>();
                return connections;
            }
        }

        public ConnectorMode ConnectorMode { get; set; }
        
        private UIElement TransponentElement = null;
        private UIElement VisualElement = null;


        public Connector()
        {             

            // fired when layout changes
            base.LayoutUpdated += new EventHandler(Connector_LayoutUpdated);            
        }

        public Connector(DesignerItem parent, 
                         HorizontalAlignment horizontalAlignment, 
                         VerticalAlignment verticalAlignment,
                         ConnectorOrientation orientation,
                         Size size,
                         double marignValue,
                         ConnectorMode connectorMode): this()

        {
            ConnectorMode = connectorMode;
            Height = size.Height;
            Width = size.Width;
            HorizontalAlignment = horizontalAlignment;
            VerticalAlignment = verticalAlignment;
            Orientation = orientation;
            DataContext = parent;
            Cursor = Cursors.Cross;
            //SnapsToDevicePixels = true;
            
            switch (orientation)
            {
                case ConnectorOrientation.Top: Margin = new Thickness(0, marignValue, 0, 0); break;
                case ConnectorOrientation.Bottom: Margin = new Thickness(0, 0, 0, marignValue); break;
                case ConnectorOrientation.Left: Margin = new Thickness(marignValue, 0, 0, 0); break;
                case ConnectorOrientation.Right: Margin = new Thickness(0, 0, marignValue, 0); break;
            }

            Grid g;
            g = new Grid();
            
            Ellipse e = new Ellipse();
            g.Children.Add(e);
            e.Fill = Brushes.Transparent;

            e = new Ellipse();
            e.Width = 2 * size.Width / 3;
            e.Height = 2 * size.Height / 3;
            TransponentElement = e;            
            g.Children.Add(e);
            e.Fill = Brushes.Lavender;
            e.StrokeThickness = 1;
            e.Stroke = new SolidColorBrush(Color.FromArgb(170, 0, 0, 80));
            VisualElement = e;
            
            Content = g;
            Show();
            if (!ParentDesignerItem.IsMouseOver)
                Hide();
        }
        public Connector(DesignerItem parent,
                         HorizontalAlignment horizontalAlignment,
                         VerticalAlignment verticalAlignment,
                         ConnectorOrientation orientation,
                         Size size)
            : this(parent, horizontalAlignment, verticalAlignment, orientation, size, -size.Width + 2, ConnectorMode.Both)
        { }

        // when the layout changes we update the position property
        void Connector_LayoutUpdated(object sender, EventArgs e)
        {
            DesignerCanvas designer = GetDesignerCanvas(this);
            if (designer != null)
            {
                //get centre position of this Connector relative to the DesignerCanvas
                Point p = new Point(this.Width / 2, this.Height / 2);
                double divMarign = 3;
                switch (this.Orientation)
                {
                    case ConnectorOrientation.Bottom: p = new Point(this.Width / 2, divMarign); break;
                    case ConnectorOrientation.Top: p = new Point(this.Width / 2, this.Height - divMarign); break;
                    case ConnectorOrientation.Right: p = new Point(divMarign, this.Height / 2); break;
                    case ConnectorOrientation.Left: p = new Point(this.Width - divMarign, this.Height / 2); break;                    
                }
                this.Position = this.TransformToAncestor(designer).Transform(p);               
            }
        }

        public void Show()
        {
            Visibility = Visibility.Visible;
            VisualElement.Visibility = Visibility.Visible;
        }

        public void Hide()
        {
           VisualElement.Visibility = Visibility.Hidden;
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DesignerCanvas canvas = GetDesignerCanvas(this);
            if (canvas != null)
            {
                // position relative to DesignerCanvas
                this.dragStartPoint = new Point?(e.GetPosition(canvas));
                e.Handled = true;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            // if mouse button is not pressed we have no drag operation, ...
            if (e.LeftButton != MouseButtonState.Pressed)
                this.dragStartPoint = null;

            // but if mouse button is pressed and start point value is set we do have one
            if (this.dragStartPoint.HasValue)
            {
                // create connection adorner 
                DesignerCanvas canvas = GetDesignerCanvas(this);
                if (canvas != null)
                {
                    AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(canvas);
                    if (adornerLayer != null)
                    {
                        ConnectorAdorner adorner = GetConnectionAdorner(canvas);
                        if (adorner != null)
                        {
                            adornerLayer.Add(adorner);
                            e.Handled = true;
                        }
                    }
                }
            }
        }

        protected virtual ConnectorAdorner GetConnectionAdorner(DesignerCanvas canvas)
        {
            ConnectorAdorner adorner = new ConnectorAdorner(canvas, this);
            return adorner;
        }

        internal ConnectorInfo GetInfo()
        {
            ConnectorInfo info = new ConnectorInfo();
            info.DesignerItemLeft = DesignerCanvas.GetLeft(this.ParentDesignerItem);
            info.DesignerItemTop = DesignerCanvas.GetTop(this.ParentDesignerItem);
            info.DesignerItemSize = new Size(this.ParentDesignerItem.ActualWidth, this.ParentDesignerItem.ActualHeight);
            info.Orientation = this.Orientation;
            info.Position = this.Position;
            return info;
        }

        // iterate through visual tree to get parent DesignerCanvas
        private DesignerCanvas GetDesignerCanvas(DependencyObject element)
        {
            while (element != null && !(element is DesignerCanvas))
                element = VisualTreeHelper.GetParent(element);

            return element as DesignerCanvas;
        }

        #region INotifyPropertyChanged Members

        // we could use DependencyProperties as well to inform others of property changes
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        #endregion
    }

    // provides compact info about a connector; used for the 
    // routing algorithm, instead of hand over a full fledged Connector
    internal struct ConnectorInfo
    {
        public double DesignerItemLeft { get; set; }
        public double DesignerItemTop { get; set; }
        public Size DesignerItemSize { get; set; }
        public Point Position { get; set; }
        public ConnectorOrientation Orientation { get; set; }
    }

    public enum ConnectorOrientation
    {
        None,
        Left,
        Top,
        Right,
        Bottom
    }

    public enum ConnectorMode
    {
        In,
        Out,
        Both
    }
}
