using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace xGraphic
{
    public class DragThumb : Thumb
    {
        public DragThumb()
        {
            base.DragDelta += new DragDeltaEventHandler(DragThumb_DragDelta);
            base.DragLeave += new System.Windows.DragEventHandler(DragThumb_DragLeave);
            base.DragCompleted += new DragCompletedEventHandler(DragThumb_DragCompleted);
        }

        void DragThumb_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            EndDrug();
        }

        void DragThumb_DragLeave(object sender, System.Windows.DragEventArgs e)
        {
            EndDrug();
        }

        private void EndDrug()
        {
            if (OnAfterDrug != null)
            {
                DesignerItem designerItem = this.DataContext as DesignerItem;
                if (designerItem == null)
                    designerItem = GetParent();
                double left = Canvas.GetLeft(designerItem);
                double top = Canvas.GetTop(designerItem);
                OnAfterDrug(new AfterDrugEventArgs() { Left = left, Top = top, DeltaHorizontal = lastDeltaHorizontal, DeltaVertical = lastDeltaVertical });
            }
        }

        private double lastDeltaHorizontal = 0;
        private double lastDeltaVertical = 0;

        void DragThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            DesignerItem designerItem = this.DataContext as DesignerItem;
            if (designerItem == null)
                designerItem = GetParent();
            
            DesignerCanvas designer = VisualTreeHelper.GetParent(designerItem) as DesignerCanvas;
            if (designerItem != null && designer != null && designerItem.IsSelected)
            {
                double minLeft = double.MaxValue;
                double minTop = double.MaxValue;

                // we only move DesignerItems
                var designerItems = designer.SelectionService.CurrentSelection.OfType<DesignerItem>();

                foreach (DesignerItem item in designerItems)
                {
                    double left = Canvas.GetLeft(item);
                    double top = Canvas.GetTop(item);

                    minLeft = double.IsNaN(left) ? 0 : Math.Min(left, minLeft);
                    minTop = double.IsNaN(top) ? 0 : Math.Min(top, minTop);
                }

                double deltaHorizontal = Math.Max(-minLeft, e.HorizontalChange);
                double deltaVertical = Math.Max(-minTop, e.VerticalChange);
                lastDeltaHorizontal = deltaHorizontal;
                lastDeltaVertical = deltaVertical;
                foreach (DesignerItem item in designerItems)
                {
                    double left = Canvas.GetLeft(item);
                    double top = Canvas.GetTop(item);

                    if (double.IsNaN(left)) left = 0;
                    if (double.IsNaN(top)) top = 0;

                    Canvas.SetLeft(item, left + deltaHorizontal);
                    Canvas.SetTop(item, top + deltaVertical);
                }

                designer.InvalidateMeasure();
                e.Handled = true;


                
            }
        }

        public event AfterDrugDelegate OnAfterDrug;
       

        private DesignerItem GetParent()
        {
            var parent = VisualTreeHelper.GetParent(this);
            while (parent != null)
            {
                parent = VisualTreeHelper.GetParent(parent);
                if (parent is DesignerItem)
                    return parent as DesignerItem;
            }
            return null;
        }
    }

    public delegate void AfterDrugDelegate(AfterDrugEventArgs e);

    public class AfterDrugEventArgs
    {
        public double Left { get; set; }
        public double Top { get; set; }
        public double DeltaHorizontal { get; set; }
        public double DeltaVertical { get; set; }
    }
}
