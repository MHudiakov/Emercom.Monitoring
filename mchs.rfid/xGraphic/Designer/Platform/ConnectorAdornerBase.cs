using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using xGraphic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace xGraphic
{
    /// <summary>
    /// Объект, организующий логику связывания
    /// </summary>
    public abstract class ConnectorAdornerBase : ConnectorAdorner
    {
        public Point HitPoint { get; set; }

        public ConnectorAdornerBase(DesignerCanvas designer, Connector sourceConnector)
            : base(designer, sourceConnector)
        { }

        protected override void DoLink()
        {
            /*if (HitDesignerItem != null)
            {
                this.HitDesignerItem.IsDragConnectionOver = false;
                if (this.HitDesignerItem is xTaskItemBase)
                {
                    xTaskItemBase task = (xTaskItemBase)this.HitDesignerItem;
                    HitConnector = task.GetHitConnector(this, HitPoint);
                }
            }

            if (HitConnector != null)
            {
                TaskConnector sourceConnector = (TaskConnector)this.sourceConnector;
                TaskConnector sinkConnector = (TaskConnector)this.HitConnector;
                TaskConnection newConnection = sinkConnector.ParentTask.GetNewConnection(sourceConnector, sinkConnector);
                // sourceConnector.ConnectorObj.ca
                Canvas.SetZIndex(newConnection, designerCanvas.Children.Count);
                this.designerCanvas.Children.Add(newConnection);
            }*/
        }

        protected override void UpdatePaintLine(MouseEventArgs e)
        {
        }
    }
}
