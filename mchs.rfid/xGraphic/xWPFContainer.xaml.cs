using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace xGraphic
{
    /// <summary>
    /// Interaction logic for xWPFContainer.xaml
    /// </summary>
    public partial class xWPFContainer : UserControl
    {
        DesignerCanvas MainCanv;

        ContentControl ContentControl
        { get { return xContentControl; } }


        public class DesignerItem1 : DesignerItem
        {
            public DesignerItem1()
                : base()
            { 
                
            }
            public void Add(object value)
            {
                AddChild(value);
            }
        
        }

        public xWPFContainer()
        {
            InitializeComponent();            
        }

        void OnMouseMove(object sender, MouseEventArgs e)
        {
            
        }

        void OnMouseDown(object sender, MouseEventArgs e)
        {
            
        }

        void OnMouseMove1(object sender, MouseEventArgs e)
        {
            
        }

        private TextBlock Text(double x, double y, string text, Color color)
        {
            
            TextBlock textBlock = new TextBlock();
            textBlock.Text = text;
            textBlock.Foreground = new SolidColorBrush(color);
            Canvas.SetLeft(textBlock, x);
            Canvas.SetTop(textBlock, y);            
            return textBlock;
            
        }

        void OnMouseEnter(object sender, MouseEventArgs e)
        {
            
            

        }

        void OnMouseExit(object sender, MouseEventArgs e)
        {
            
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void UserControl_DragEnter(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Copy;
        }
    }

    public class MyControl : ContentControl, ISelectable
    {
        Rectangle r = new Rectangle();
        Rectangle r2 = new Rectangle();
        public bool IsSelected { get; set; }
        Canvas MainCanvas;


        public MyControl(Canvas mainCanvas)
            : base()
        {
            MainCanvas = mainCanvas;
            Canvas.SetLeft(r, 10);
            Canvas.SetTop(r, 20);
            r.Width = 100;
            r.Height = 50;
            GradientStopCollection gsc = new GradientStopCollection(2);
            gsc.Add(new GradientStop(Color.FromArgb(200, 200, 255, 100), 0));
            gsc.Add(new GradientStop(Colors.Orange, 1));
            LinearGradientBrush ItemBrush = new LinearGradientBrush(gsc);
            // r.Fill = new SolidColorBrush(Colors.Blue);            
            r.Fill = ItemBrush;
            r.Cursor = Cursors.Hand;
            r.Name = "xrect";
            //MainCanvas.Children.Add(r);
            Grid g = new Grid();
            AddChild(g);
            g.Children.Add(r);
            MainCanvas.Children.Add(this);
            /*
            DragThumb dt = new DragThumb();
            dt.Cursor = Cursors.SizeAll;
            dt.Background = null;// new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
            dt.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
            dt.Width = 100;
            dt.Height = 100;
            dt.Margin = new Thickness(0, 0, -10, 0);
            
            //dt.VerticalAlignment = VerticalAlignment.Bottom;
            //dt.HorizontalAlignment = HorizontalAlignment.Right;

            g.Children.Add(dt);
            
            dt.DataContext = this;
            //MainCanvas.Children.Add(dt);
            //dt.SetBinding(Canvas.LeftProperty, new Binding("{Binding Elemen=xrect, Path=Value}");

            r2.Margin = new Thickness(10, 10, 0, 0);
            r2.Width = 30;
            r2.Height = 30;
            r2.Fill = new SolidColorBrush(Colors.Blue);
            r2.Fill = Brushes.Transparent;
            r2.Cursor = Cursors.No;
            r2.Name = "xrect";
            g.Children.Add(r2);*/
            

        }
    }
}
