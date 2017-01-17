using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Shapes;

namespace xGraphic
{
    /// <summary>
    /// Базовый класс связи между 2-мя коннкеторами
    /// </summary>
    public class Connection : ContentControl, ISelectable, INotifyPropertyChanged
    {
        private Adorner connectionAdorner;

        #region Properties

        /// <summary>
        /// ID линии
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// Контейнер, который в себе будет содержать линию
        /// </summary>
        public Grid Container { get { return this.Content as Grid; } }

        private Connector source;
        
        /// <summary>
        /// К какому конектору идет лини
        /// </summary>
        public Connector Source
        {
            get
            {
                return source;
            }
            set
            {
                if (source != value)
                {
                    if (source != null)
                    {
                        source.PropertyChanged -= new PropertyChangedEventHandler(OnConnectorPositionChanged);
                        source.Connections.Remove(this);
                    }

                    source = value;

                    if (source != null)
                    {
                        source.Connections.Add(this);
                        source.PropertyChanged += new PropertyChangedEventHandler(OnConnectorPositionChanged);
                    }

                    UpdatePathGeometry();
                }
            }
        }

        private Connector sink;
        /// <summary>
        /// От какого конектору идет лини
        /// </summary>
        public Connector Sink
        {
            get { return sink; }
            set
            {
                if (sink != value)
                {
                    if (sink != null)
                    {
                        sink.PropertyChanged -= new PropertyChangedEventHandler(OnConnectorPositionChanged);
                        sink.Connections.Remove(this);
                    }

                    sink = value;

                    if (sink != null)
                    {
                        sink.Connections.Add(this);
                        sink.PropertyChanged += new PropertyChangedEventHandler(OnConnectorPositionChanged);
                    }
                    UpdatePathGeometry();
                }
            }
        }

        ArrowLines.ArrowLine line = null;
        /// <summary>
        /// Линия, которая показана на экране (со стрелкой)
        /// </summary> 
        public ArrowLines.ArrowLine Line
        {
            get { return line; }
            set { line = value; }
        }

        double activeLineWidth = 2;
        /// <summary>
        /// Ширина линии в момент активации
        /// </summary>
        public double ActiveLineWidth
        {
            get { return activeLineWidth; }
            set { activeLineWidth = value; }
        }

        double lineWidth = 1;
        /// <summary>
        /// Обычная ширина линии 
        /// </summary>
        public double LineWidth
        {
            get { return lineWidth; }
            set { lineWidth = value; }
        }

        // connection path geometry
        private PathGeometry pathGeometry;
        
        /// <summary>
        /// PathGeometry Path который объединяет в себе линию
        /// </summary>
        public PathGeometry PathGeometry
        {
            get { return pathGeometry; }
            set
            {
                if (pathGeometry != value)
                {                    
                    pathGeometry = value;
                    UpdateLine();
                }
            }
        }

        /// <summary>
        /// Обновить линию
        /// </summary>
        public virtual void UpdateLine()
        {
            ArrowLines.ArrowLine l = GetArrowLinePath();
            if (!(this.Content is Grid))
                xInit();
            else
                ClearLine();
            (this.Content as Grid).Children.Add(l);
            UpdateAnchorPosition();
            OnPropertyChanged("PathGeometry");
        }

        /// <summary>
        /// Убить линию
        /// </summary>
        protected virtual void ClearLine()
        {
            ArrowLines.ArrowLine l = Container.Children.OfType<ArrowLines.ArrowLine>().First() as ArrowLines.ArrowLine;
            Container.Children.Remove(l);
        }

        /// <summary>
        /// Инициализация для перекрытия
        /// </summary>
        protected virtual void xInit()
        {
            this.Content = new Grid();
        }
        /// <summary>
        /// Получнить стрелку линии. Может перекрываться.
        /// </summary>
        /// <returns>Полученная стрелка ArrowLine</returns>
        public virtual ArrowLines.ArrowLine GetArrowLinePath()
        {
            ArrowLines.ArrowLine l = new xGraphic.ArrowLines.ArrowLine();
            l.IsArrowClosed = true;
            l.ArrowAngle = 20;
            Line = l;
            l.SnapsToDevicePixels = true;
            l.Stroke = Brushes.Black;
            l.StrokeThickness = lineWidth;
            l.ArrowEnds = xGraphic.ArrowLines.ArrowEnds.End;

            l.X1 = Source.Position.X;
            l.Y1 = Source.Position.Y;
            l.X2 = Sink.Position.X;
            l.Y2 = Sink.Position.Y;
            l.Fill = l.Stroke;
            return l;
        }

        // between source connector position and the beginning 
        // of the path geometry we leave some space for visual reasons; 
        // so the anchor position source really marks the beginning 
        // of the path geometry on the source side        
        private Point anchorPositionSource;
        /// <summary>
        /// between source connector position and the beginning 
        /// of the path geometry we leave some space for visual reasons; 
        /// so the anchor position source really marks the beginning 
        /// of the path geometry on the source side
        /// </summary>
        public Point AnchorPositionSource
        {
            get { return anchorPositionSource; }
            set
            {
                if (anchorPositionSource != value)
                {
                    anchorPositionSource = value;
                    OnPropertyChanged("AnchorPositionSource");
                }
            }
        }

        // slope of the path at the anchor position
        // needed for the rotation angle of the arrow
        private double anchorAngleSource = 0;
        /// <summary>
        /// slope of the path at the anchor position
        /// needed for the rotation angle of the arrow        
        /// </summary>
        public double AnchorAngleSource
        {
            get { return anchorAngleSource; }
            set
            {
                if (anchorAngleSource != value)
                {
                    anchorAngleSource = value;
                    OnPropertyChanged("AnchorAngleSource");
                }
            }
        }

        // analogue to source side
        private Point anchorPositionSink;
        /// <summary>
        /// analogue to source side
        /// </summary>
        public Point AnchorPositionSink
        {
            get { return anchorPositionSink; }
            set
            {
                if (anchorPositionSink != value)
                {
                    anchorPositionSink = value;
                    OnPropertyChanged("AnchorPositionSink");
                }
            }
        }
        
        // analogue to source side
        private double anchorAngleSink = 0;
        /// <summary>
        /// Угол стрелки начала
        /// </summary>
        public double AnchorAngleSink
        {
            get { return anchorAngleSink; }
            set
            {
                if (anchorAngleSink != value)
                {
                    anchorAngleSink = value;
                    OnPropertyChanged("AnchorAngleSink");
                }
            }
        }

        private ArrowSymbol sourceArrowSymbol = ArrowSymbol.None;
        /// <summary>
        /// Тип знака линии в Конце
        /// </summary>
        public ArrowSymbol SourceArrowSymbol
        {
            get { return sourceArrowSymbol; }
            set
            {
                if (sourceArrowSymbol != value)
                {
                    sourceArrowSymbol = value;
                    OnPropertyChanged("SourceArrowSymbol");
                }
            }
        }

        ArrowSymbol sinkArrowSymbol = ArrowSymbol.Arrow;
        /// <summary>
        /// Тип знака линии на Начале
        /// </summary>
        public ArrowSymbol SinkArrowSymbol
        {
            get { return sinkArrowSymbol; }
            set
            {
                if (sinkArrowSymbol != value)
                {
                    sinkArrowSymbol = value;
                    OnPropertyChanged("SinkArrowSymbol");
                }
            }
        }

        // specifies a point at half path length
        private Point labelPosition;
        /// <summary>
        /// Точка LabelPosition
        /// </summary>
        public Point LabelPosition
        {
            get { return labelPosition; }
            set
            {
                if (labelPosition != value)
                {
                    labelPosition = value;
                    OnPropertyChanged("LabelPosition");
                }
            }
        }

        // pattern of dashes and gaps that is used to outline the connection path
        private DoubleCollection strokeDashArray;
        /// <summary>
        /// Тип линии для StrokeDashArray
        /// </summary>
        public DoubleCollection StrokeDashArray
        {
            get
            {
                return strokeDashArray;
            }
            set
            {
                if (strokeDashArray != value)
                {
                    strokeDashArray = value;
                    OnPropertyChanged("StrokeDashArray");
                }
            }
        }
        // if connected, the ConnectionAdorner becomes visible
        private bool isSelected;
        /// <summary>
        /// Является ли выделенным
        /// </summary>
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                if (isSelected != value)
                {
                    isSelected = value;
                    OnPropertyChanged("IsSelected");
                    if (isSelected)
                    {
                        ShowAdorner();
                        OnSelect();
                    }
                    else
                    {
                        OnUnSelect();
                        HideAdorner();
                    }
                }
            }
        }

        #endregion

        /// <summary>
        /// Конструктор, связывающий 2 коннектора
        /// </summary>
        /// <param name="source">коннектор, К которому связывается</param>
        /// <param name="sink">коннектор, От которогоидет связь</param>
        public Connection(Connector source, Connector sink)
        {
            this.ID = Guid.NewGuid();
            this.Source = source;
            this.Sink = sink;
            base.Unloaded += new RoutedEventHandler(Connection_Unloaded);
        }

        /// <summary>
        /// Особая обработка при изменении некоторых параметров
        /// </summary>
        /// <param name="e">информация об изменяемых параметрах</param>
        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == e.OldValue)
                return;
            
            if (e.Property == Connection.IsMouseOverProperty)
                if ((bool)e.NewValue)
                {
                    OnMouseOverProperty();
                }
                else
                {
                    OnUnMouseOverProperty();
                }
            base.OnPropertyChanged(e);
        }

        /// <summary>
        /// Подсвечивать при выделении
        /// </summary>
        protected virtual void OnSelect()
        {
            Line.StrokeThickness = activeLineWidth;   
        }
        
        /// <summary>
        /// Убравть подсведку при Un-выделении
        /// </summary>
        protected virtual void OnUnSelect()
        {
            Line.StrokeThickness = lineWidth;
        }

        /// <summary>
        /// Метод для потомков при наведении мыши OnMouseOverProperty
        /// </summary>
        protected virtual void OnMouseOverProperty()
        {
            Line.StrokeThickness = activeLineWidth;
        }

        /// <summary>
        /// Метод для потомков при уходе мыши OnUnMouseOverProperty
        /// </summary>
        protected virtual void OnUnMouseOverProperty()
        {
            if (!IsSelected)
                Line.StrokeThickness = lineWidth;
        }
        
        /// <summary>
        /// Особовая обработка нажатия мыши
        /// </summary>
        /// <param name="e">события мышки</param>
        protected override void OnMouseDown(System.Windows.Input.MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);

            // usual selection business
            DesignerCanvas designer = VisualTreeHelper.GetParent(this) as DesignerCanvas;
            if (designer != null)
            {
                if ((Keyboard.Modifiers & (ModifierKeys.Shift | ModifierKeys.Control)) != ModifierKeys.None)
                    if (this.IsSelected)
                    {
                        designer.SelectionService.RemoveFromSelection(this);
                    }
                    else
                    {
                        designer.SelectionService.AddToSelection(this);
                    }
                else if (!this.IsSelected)
                {
                    designer.SelectionService.SelectItem(this);
                }

                Focus();
            }
            e.Handled = false;
        }

        void OnConnectorPositionChanged(object sender, PropertyChangedEventArgs e)
        {
            // whenever the 'Position' property of the source or sink Connector 
            // changes we must update the connection path geometry
            if (e.PropertyName.Equals("Position"))
            {
                UpdatePathGeometry();
            }
        }

        private void UpdatePathGeometry()
        {
            if (Source != null && Sink != null)
            {
                UpdateLine();
                //PathGeometry geometry = new PathGeometry();
                //List<Point> linePoints = PathFinder.GetConnectionLine(Source.GetInfo(), Sink.GetInfo(), true);
                //if (linePoints.Count > 0)
                {
                    //PathFigure figure = new PathFigure();
                    //figure.StartPoint = linePoints[0];
                    //linePoints.Remove(linePoints[0]);
                    //figure.Segments.Add(new PolyLineSegment(linePoints, true));
                    //geometry.Figures.Add(figure);
                    
                    //this.PathGeometry = geometry;
                }
            }
        }

        private void UpdateAnchorPosition()
        {
            /*
            Point pathStartPoint, pathTangentAtStartPoint;
            Point pathEndPoint, pathTangentAtEndPoint;
            Point pathMidPoint, pathTangentAtMidPoint;

            // the PathGeometry.GetPointAtFractionLength method gets the point and a tangent vector 
            // on PathGeometry at the specified fraction of its length
            this.PathGeometry.GetPointAtFractionLength(0, out pathStartPoint, out pathTangentAtStartPoint);
            this.PathGeometry.GetPointAtFractionLength(1, out pathEndPoint, out pathTangentAtEndPoint);
            this.PathGeometry.GetPointAtFractionLength(0.5, out pathMidPoint, out pathTangentAtMidPoint);

            // get angle from tangent vector
            this.AnchorAngleSource = Math.Atan2(-pathTangentAtStartPoint.Y, -pathTangentAtStartPoint.X) * (180 / Math.PI);
            this.AnchorAngleSink = Math.Atan2(pathTangentAtEndPoint.Y, pathTangentAtEndPoint.X) * (180 / Math.PI);

            // add some margin on source and sink side for visual reasons only
            pathStartPoint.Offset(-pathTangentAtStartPoint.X * 5, -pathTangentAtStartPoint.Y * 5);
            pathEndPoint.Offset(pathTangentAtEndPoint.X * 5, pathTangentAtEndPoint.Y * 5);

            this.AnchorPositionSource = pathStartPoint;
            this.AnchorPositionSink = pathEndPoint;
            this.LabelPosition = pathMidPoint;
            */ 
        }

        private void ShowAdorner()
        {
            // the ConnectionAdorner is created once for each Connection
            if (this.connectionAdorner == null)
            {
                DesignerCanvas designer = VisualTreeHelper.GetParent(this) as DesignerCanvas;

                AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this);
                if (adornerLayer != null)
                {
                    this.connectionAdorner = new ConnectionAdorner(designer, this);
                    adornerLayer.Add(this.connectionAdorner);
                }
            }
            this.connectionAdorner.Visibility = Visibility.Visible;
        }

        internal void HideAdorner()
        {
            if (this.connectionAdorner != null)
                this.connectionAdorner.Visibility = Visibility.Collapsed;
        }

        void Connection_Unloaded(object sender, RoutedEventArgs e)
        {
            // do some housekeeping when Connection is unloaded

            // remove event handler
            this.Source = null;
            this.Sink = null;

            // remove adorner
            if (this.connectionAdorner != null)
            {
                DesignerCanvas designer = VisualTreeHelper.GetParent(this) as DesignerCanvas;

                AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this);
                if (adornerLayer != null)
                {
                    adornerLayer.Remove(this.connectionAdorner);
                    this.connectionAdorner = null;
                }
            }
        }

        /// <summary>
        /// При удалении графического объекта необходимо удалить связи на него
        /// </summary>
        /// <param name="connector"></param>
        public virtual void Delete(Connector connector)
        {
            if (sink != connector)
                sink.Connections.Remove(this);
            if (source != connector)
                source.Connections.Remove(this);
            DesignerCanvas designer = VisualTreeHelper.GetParent(this) as DesignerCanvas;
            
            if (designer != null)
                designer.Children.Remove(this);
        }

        #region INotifyPropertyChanged Members

        /// <summary>
        /// we could use DependencyProperties as well to inform others of property changes
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        
        /// <summary>
        /// Изменение свойства объекта
        /// </summary>
        /// <param name="name"></param>
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

    /// <summary>
    /// Тип знака линии
    /// </summary>
    public enum ArrowSymbol
    {
        /// <summary>
        /// Без знака
        /// </summary>
        None,
        /// <summary>
        /// Стрелка
        /// </summary>
        Arrow,
        /// <summary>
        /// Ромбик
        /// </summary>
        Diamond
    }
}
