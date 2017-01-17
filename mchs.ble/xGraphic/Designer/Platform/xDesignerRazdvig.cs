using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows;

namespace xGraphic
{
    public class xDesignerRazdvig : xDesignerItemBase
    {
        bool m_IsDown = false;
        public bool IsDown
        {
            get { return m_IsDown; }
            set { m_IsDown = value; }
        }

        Rectangle MainRectangle = new Rectangle();
        List<xDesignerItemBase> MoveItems = new List<xDesignerItemBase>();
        double startTop = 0;

        public xDesignerRazdvig(xDesignerBase parent)
            : base(parent)
        {
            DragThumb.Visibility = System.Windows.Visibility.Visible;
            DragThumb.CaptureMouse();
        }

        protected override void InitSize()
        {
            Width = ParentDesigner.Width;
            Height = 5;
        }

        protected override void CreateMainShape()
        {
            
            MainRectangle.StrokeThickness = 2;
            MainRectangle.Stroke = Brushes.Blue;
            MainRectangle.Fill = new SolidColorBrush(Color.FromArgb(100, 100, 100, 255));
            AddChild(MainRectangle);
            MainRectangle.Visibility = Visibility.Hidden;

            Canvas.SetTop(MainRectangle, -5);
            Width = 1000;
            Height = 2;
            MainRectangle.IsHitTestVisible = false;
            //DragThumb.IsHitTestVisible = false;
            this.IsHitTestVisible = false;
        }

        public void Down(MouseButtonEventArgs e)
        {
            IsDown = true;
            MoveItems.Clear();
            startTop = Canvas.GetTop(this);
            foreach (xDesignerItemBase i in ParentDesigner.Children.OfType<xDesignerItemBase>())
            {
                if (Canvas.GetTop(i) > startTop)
                    MoveItems.Add(i);
            }
        }

        public void Move(MouseEventArgs e)
        {
            if (!IsDown)
            {
                MainRectangle.Visibility = Visibility.Visible;
                Canvas.SetTop(this, e.GetPosition(ParentDesigner).Y);
            }
            else            
            {
                double nowTop = e.GetPosition(ParentDesigner).Y;
                bool isIncreace = startTop < nowTop;
                double delta;
                if (isIncreace)
                    delta = nowTop - startTop - Height;
                else
                    delta = nowTop - startTop + Height;

                if (isIncreace)
                    Height += delta;
                else
                {
                    Height -= delta;
                    Canvas.SetTop(this, nowTop - delta);
                }

                foreach (xDesignerItemBase i in MoveItems)
                {
                    Canvas.SetTop(i, Canvas.GetTop(i) + delta);
                }
            }
        }

        public void Up(MouseButtonEventArgs e)
        {
            IsDown = false;
        }
    }
}
