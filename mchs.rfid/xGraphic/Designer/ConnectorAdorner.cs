using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Controls;
using xGraphic.ArrowLines;

namespace xGraphic
{
    public class ConnectorAdorner : Adorner
    {
        
        protected ArrowLine Line;
        protected DesignerCanvas designerCanvas;
        protected Connector sourceConnector;
        public Connector SourceConnector
        {
            get { return sourceConnector; }
            set { sourceConnector = value; }
        }

        protected Pen drawingPen;

        private DesignerItem hitDesignerItem;
        protected DesignerItem HitDesignerItem
        {
            get { return hitDesignerItem; }
            set
            {
                if (hitDesignerItem != value)
                {
                    if (hitDesignerItem != null)
                        hitDesignerItem.IsDragConnectionOver = false;

                    hitDesignerItem = value;

                    if (hitDesignerItem != null)
                        hitDesignerItem.IsDragConnectionOver = true;
                }
            }
        }

        private Connector hitConnector;
        protected Connector HitConnector
        {
            get { return hitConnector; }
            set
            {
                if (hitConnector != value)
                {
                    hitConnector = value;
                }
            }
        }

        public ConnectorAdorner(DesignerCanvas designer, Connector sourceConnector)
            : base(designer)
        {
            this.designerCanvas = designer;
            this.sourceConnector = sourceConnector;
            drawingPen = new Pen(Brushes.LightSlateGray, 1);
            drawingPen.LineJoin = PenLineJoin.Round;
            this.Cursor = Cursors.Cross;
            InitConnector();
        }

        protected virtual void InitConnector()
        {

        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            DoLink();

            if (this.IsMouseCaptured) 
                this.ReleaseMouseCapture();

            BeforeDelete();
            AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(this.designerCanvas);
            if (adornerLayer != null)
                adornerLayer.Remove(this);
        }
        
        protected virtual void BeforeDelete()
        {}
        
        protected virtual void DoLink()
        {
            if (HitConnector != null)
            {
                Connector sourceConnector = this.sourceConnector;
                Connector sinkConnector = this.HitConnector;
                Connection newConnection = new Connection(sourceConnector, sinkConnector);

                Canvas.SetZIndex(newConnection, designerCanvas.Children.Count);
                this.designerCanvas.Children.Add(newConnection);
            }

            if (HitDesignerItem != null)
                this.HitDesignerItem.IsDragConnectionOver = false;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (IsDoingLink(e))
            {
                if (!this.IsMouseCaptured) 
                    this.CaptureMouse();
                HitTesting(e.GetPosition(this));
                UpdatePaintLine(e);
            }
            else
                if (this.IsMouseCaptured) 
                    this.ReleaseMouseCapture();
        }

        protected virtual void UpdatePaintLine(MouseEventArgs e)
        {
            //this.pathGeometry = GetPathGeometry(e.GetPosition(this));
            this.InvalidateVisual();
            sourceConnector.Visibility = Visibility.Visible;
        }

        protected virtual bool IsDoingLink(MouseEventArgs e)
        {
            return e.LeftButton == MouseButtonState.Pressed;
        }

        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            //dc.DrawGeometry(null, drawingPen, this.pathGeometry);

            // without a background the OnMouseMove event would not be fired   
            // Alternative: implement a Canvas as a child of this adorner, like
            // the ConnectionAdorner does.                                     
            dc.DrawRectangle(Brushes.Transparent, null, new Rect(RenderSize));
        }

        private PathGeometry GetPathGeometry(Point position)
        {
            PathGeometry geometry = new PathGeometry();
            
            ConnectorOrientation targetOrientation;
            if (HitConnector != null)
                targetOrientation = HitConnector.Orientation;
            else
                targetOrientation = ConnectorOrientation.None;

            List<Point> pathPoints = PathFinder.GetConnectionLine(sourceConnector.GetInfo(), position, targetOrientation);

            if (pathPoints.Count > 0)
            {
                PathFigure figure = new PathFigure();
                figure.StartPoint = pathPoints[0];
                pathPoints.Remove(pathPoints[0]);
                figure.Segments.Add(new PolyLineSegment(pathPoints, true));
                geometry.Figures.Add(figure);
            }

            return geometry;
        }

        protected virtual void HitTesting(Point hitPoint)
        {
            bool hitConnectorFlag = false;
            
            DependencyObject hitObject = designerCanvas.InputHitTest(hitPoint) as DependencyObject;

            while (hitObject != null &&
                   hitObject != sourceConnector.ParentDesignerItem &&
                   hitObject.GetType() != typeof(DesignerCanvas))
            {
                if (hitObject is Connector)
                {
                    HitConnector = hitObject as Connector;
                    hitConnectorFlag = true;
                }

                if (hitObject is DesignerItem)
                {
                    HitDesignerItem = hitObject as DesignerItem;
                    if (!hitConnectorFlag)
                        HitConnector = null;
                    return;
                }
                hitObject = VisualTreeHelper.GetParent(hitObject);                                
                
            }

            HitConnector = null;
            HitDesignerItem = null;
        }           
    }
}
