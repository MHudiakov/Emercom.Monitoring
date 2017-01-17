using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using xGraphic;
//using Platform.Base;

namespace xGraphic
{
    public class xDesignerItemBase : DesignerItem
    {
        xDesignerBase m_ParentDesigner = null;
        /// <summary>
        /// Родительский объект дизайнера
        /// </summary>
        public xDesignerBase ParentDesigner
        {
            get { return m_ParentDesigner; }
            set { m_ParentDesigner = value; }
        }

        /// <summary>
        /// Входные коннекторы
        /// </summary>
        public List<Connector> InConnectors
        {
            get
            {
                return Container.Children.OfType<Connector>().Where(c => c.ConnectorMode != ConnectorMode.Out).ToList();
            }
        }

        /// <summary>
        /// Выходные коннекторы
        /// </summary>
        public List<Connector> OutConnectors
        {
            get
            {
                return Container.Children.OfType<Connector>().Where(c => c.ConnectorMode != ConnectorMode.In).ToList();
            }
        }

        /// <summary>
        /// Один вход - один выход
        /// </summary>
        public bool IsInOutConnectorMode
        {
            get
            {
                if (Container.Children.OfType<Connector>().Count() == 2)
                    if ((InConnectors.Count == 1) & (OutConnectors.Count == 1))
                        return true;
                return false;
            }
        }

        /// <summary>
        /// Объект, который отображается текущим объектом
        /// </summary>
        protected readonly object BindObject;

        Shape m_MainShape = null;
        /// <summary>
        /// Основная фигура объекта
        /// </summary>
        public Shape MainShape
        {
            get { return m_MainShape; }
            set { m_MainShape = value; }
        }

        DragThumb m_DragThumb = null;
        /// <summary>
        /// Основной тхамб для перемещения
        /// </summary>
        public DragThumb DragThumb
        {
            get { return m_DragThumb; }
            set { m_DragThumb = value; }
        }

        Grid m_Container = null;
        /// <summary>
        /// Содержит все вложенные объекты
        /// </summary>
        /// <remarks>Содержит все вложенные объекты, такие как части отрисовки, линии, точки связи, тхамбы, тексты и тд...</remarks>
        public Grid Container
        {
            get { return m_Container; }
            set { m_Container = value; }
        }
        
        /// <summary>
        /// Конструктор без параметров. Для инициализации элемента нцжно вызвать xInitItem()
        /// </summary>
        public xDesignerItemBase()
            : base()
        {            
        }

        /// <summary>
        /// Получить смещение Marign для определения смещения элемента на Left/Top
        /// </summary>
        /// <param name="left">Смещение по X</param>
        /// <param name="top">Смещение по Y</param>
        /// <returns>Значение, которое можно подставлять в Marign элемента</returns>
        public Thickness GetMoveTo(double left, double top)
        {
            Thickness t = new Thickness(left, top, -left, -top);
            return t;
        }

        /// <summary>
        /// Добавить иконку
        /// </summary>
        /// <param name="va">Выравнивание по вертикали</param>
        /// <param name="ha">Выравнивание по горизонтали</param>
        /// <param name="index">Номер иконки при данном выравнивании. Например, 2-ая справа сверху, 1-ая слева сверху и тп.</param>
        public virtual Image AddImage(VerticalAlignment va, HorizontalAlignment ha, int index, string ImageName)
        {
            /*
            // ImageSource() = Imagebi;
            Ellipse e = new Ellipse();
            //AddChild(e);
            e.VerticalAlignment = va;
            e.HorizontalAlignment = ha;
            e.Fill = Brushes.AliceBlue;
            e.Stroke = Brushes.Black;
            e.Width = 16;
            e.Height = 16;
            // считаем сколько их слева.
            if (ha == HorizontalAlignment.Right)
                index = - index;
            e.Margin = GetMoveTo(index * 18, -18);
            */

            Image simpleImage = new Image();
            simpleImage.Stretch = Stretch.Fill;
            
            // Create source.
            BitmapImage bi = new BitmapImage();
            
            // BitmapImage.UriSource must be in a BeginInit/EndInit block.
            bi.BeginInit();
//            bi.UriSource = new Uri(Platform.Properties.Settings.Default.ImagePath + ImageName, UriKind.RelativeOrAbsolute);            
            bi.EndInit();
            // Application.Current.Resources
            

            // Set the image source.            
            //simpleImage.Source = bi;
            simpleImage.Source = bi;
            simpleImage.Width = 16;
            simpleImage.Height = 16;
            if (ha == HorizontalAlignment.Right)
                index = -index;
            simpleImage.Margin = GetMoveTo(index * 18, -18);
            AddChild(simpleImage);
            simpleImage.VerticalAlignment = va;
            simpleImage.HorizontalAlignment = ha;
            
            // каждая иконка = свойство отдельным классом? не... слишком...
            return simpleImage;
        }


        /// <param name="parent">Родительский дизайнер</param>
        public xDesignerItemBase(xDesignerBase parent)
            : this()
        {
            xInitItem(parent);
        }

        /// <summary>
        /// Создание объекта и всязанного с ним.
        /// </summary>
        /// <param name="bindObject">Связанный объект</param>
        /// <param name="parent">Родительский дизайне</param>
        public xDesignerItemBase(object bindObject, xDesignerBase parent)
            : this()
        {
            BindObject = bindObject;
        }

        protected void xInitItem(xDesignerBase parent)
        {
            xInitItem(parent, true);           
        }

        protected void xInitItem(xDesignerBase parent, bool addToDesigner)
        {
            if (addToDesigner)
                parent.Children.Add(this);
            m_ParentDesigner = parent;
            Container = new Grid();
            Content = Container;
            InitSize();
            CreateMainShape();
            CreateThumb(parent);
            CreateConnectors();
            CreateContextMenu();
        }

        /// <summary>
        /// Инициализация размеров элемента. Может быть перекрыт в потомках
        /// </summary>
        /// <remarks>По умолчанию - 80*40</remarks>
        protected virtual void InitSize()
        {
            Width = 80;
            Height = 40;

        }

        /// <summary>
        /// Создание тхамбов.
        /// </summary>
        protected virtual void CreateThumb(xDesignerBase parent)
        {
            DragThumb = new DragThumb();
            Container.Children.Add(DragThumb);
            DragThumb.DataContext = this;
            DragThumb.Cursor = Cursors.SizeAll;
            DragThumb.Opacity = 0;                        
        }

        /// <summary>
        /// Создать точки связи.
        /// </summary>
        /// <remarks>Позволяет потомкам создавать точки, с которыми будет связываться данный элемент</remarks>
        protected virtual void CreateConnectors()
        {
            
        }

        /// <summary>
        /// Создание основной фигуры объекта
        /// </summary>
        /// <remarks>Тут же создаются и тексты, линии...</remarks>
        protected virtual void CreateMainShape()
        {
            LinearGradientBrush lgb = new LinearGradientBrush(
                Color.FromArgb(255, 240, 250, 240),
                Color.FromArgb(255, 220, 250, 220),
                45);
            
            /*MainShape = new Rectangle();
            MainShape.Fill = lgb;
            MainShape.Stroke = null;
            MainShape.StrokeThickness = 0;
            Container.Children.Add(MainShape);
            MainShape.Width = 80;
            MainShape.Height = 40;
            MainShape.RenderTransform = new SkewTransform(-10, -10);
            MainShape.Margin = new Thickness(5, 5, -5, -5);
            MainShape.Fill = new SolidColorBrush(Color.FromArgb(100, 50, 50, 50));
            BlurBitmapEffect bbe = new BlurBitmapEffect();
            bbe.Radius = 2;
            MainShape.BitmapEffect = bbe;*/
                            
            MainShape = new Rectangle();
            MainShape.Fill = lgb;
            MainShape.Stroke = Brushes.Black;
            MainShape.StrokeThickness = 1;
            Container.Children.Add(MainShape);
            MainShape.Width = 80;
            MainShape.Height = 40;
            //MainShape.RenderTransform = new SkewTransform(-10, -10);
            SetShadow();
        }

        public virtual void SetShadow()
        {
            DropShadowBitmapEffect dsbe;
            dsbe = new DropShadowBitmapEffect();
            dsbe.Color = Colors.Gray;
            dsbe.ShadowDepth = 6;
            dsbe.Opacity = 0.2;
            dsbe.Softness = 0;
            dsbe.Direction = 320;
            dsbe.Noise = 0;
            MainShape.BitmapEffect = dsbe;
        }

        /// <summary>
        /// Добавить элемент в стуктуру данного объекта
        /// </summary>
        /// <param name="value">Добавляемый объект</param>
        protected override void AddChild(object value)
        {
            Container.Children.Add(value as UIElement);            
        }

        /// <summary>
        /// Отслеживание некоторых изменений свойств объекта
        /// </summary>
        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue == e.OldValue)
                return;
            if (e.Property == xDesignerItemBase.IsSelectedProperty)
                if ((bool)e.NewValue)
                    OnSelect();
                else
                    OnUnSelect();

            if (e.Property == xDesignerItemBase.IsDragConnectionOverProperty)
                if ((bool)e.NewValue)
                {
                    OnDragConnectionOver();
                }
                else
                {
                    OnUnDragConnectionOver();
                }
                                        
            base.OnPropertyChanged(e);
        }

        /// <summary>
        /// Выделение объекта
        /// </summary>
        protected virtual void OnSelect()
        {
            
            if (MainShape == null)
                return;
            MainShape.StrokeThickness = 2;
            MainShape.Stroke = new SolidColorBrush(Colors.DarkBlue);
        }

        /// <summary>
        /// Сниятие выделения объекта
        /// </summary>
        protected virtual void OnUnSelect()
        {
            if (MainShape == null)
                return;

            MainShape.StrokeThickness = 1;
            MainShape.Stroke = new SolidColorBrush(Colors.Black);
        }

        /// <summary>
        /// Когда закончили проводить зажатую мышь "не готовой" связью
        /// </summary>
        protected virtual void OnDragConnectionOver()
        {
            
            if (MainShape != null)
            {
                MainShape.StrokeThickness = 2;
                MainShape.Stroke = new SolidColorBrush(Colors.DarkBlue);            
            }
            ShowConnector();
        }

        protected virtual void OnUnDragConnectionOver()
        {
            if (MainShape != null)
            {
                MainShape.StrokeThickness = 1;
                MainShape.Stroke = new SolidColorBrush(Colors.Black);
            }
            HideConnector();
        }

        /// <summary>
        /// Событие мыши. Покажим коннекторы
        /// </summary>
        protected override void OnMouseEnter(MouseEventArgs e)
        {
            ShowConnector();
            if (MainShape != null) 
                MainShape.StrokeThickness = 2;
            base.OnMouseEnter(e);
        }

        /// <summary>
        /// Показать коннекторы
        /// </summary>
        /// <remarks>Вызывается при событиях от мыши</remarks>
        public override void ShowConnector()
        {
            foreach (Connector c in Container.Children.OfType<Connector>())
                c.Show();
            //m_BottomConnector.Show();
        }

        /// <summary>
        /// Событие мыши. Спрячем коннекторы.
        /// </summary>
        protected override void OnMouseLeave(MouseEventArgs e)
        {
            HideConnector();
            if (!IsSelected)
                if (MainShape != null)
                    MainShape.StrokeThickness = 1;
            
            base.OnMouseLeave(e);
        }

        /// <summary>
        /// Спрятать коннекторы
        /// </summary>
        /// <remarks>Вызывается при событиях от мыши</remarks>
        public override void HideConnector()
        {
            foreach (Connector c in Container.Children.OfType<Connector>())
                c.Hide();
            
            //m_TopConnector.Hide();
            //m_BottomConnector.Hide();
        }

        /// <summary>
        /// Получить коннекктор, с котором можно связать текущую связь
        /// </summary>
        /// <remarks>Используется когда тянется связь от одного окннектора к другому. Когда мы находимся над элементом задачи, вызывается этот метод, который подыскивает наиболее подходящий коннектор или говрит, что связь не возможна</remarks>
        /// <param name="connectorAdorner">Объект тянущейся связи</param>
        /// <param name="position">Позиция курсора мыши над элементом</param>
        /// <returns>наиболее подходящий коннектор или null, если связь с элементом не возможна</returns>
        public virtual Connector GetHitConnector(ConnectorAdorner connectorAdorner, Point position)
        {
            if (Container.Children.OfType<Connector>().Count() == 0)
                return null;
            if (Container.Children.OfType<Connector>().Count() == 1)
                return Container.Children.OfType<Connector>().First();
            else
            {

                var element = Container.Children.OfType<Connector>().First();
                /*Type t = element.GetType();
                t.
                ITopBottomConnector<t.GetType()> asdf = null;
                */

                //if ((connectorAdorner.SourceConnector.ParentDesignerItem as xDesignerItemBase).IsInOutConnectorMode)
                var fromItem = connectorAdorner.SourceConnector.ParentDesignerItem as xDesignerItemBase;
                if (fromItem == null) 
                    return null;
                if (connectorAdorner.SourceConnector.ConnectorMode != ConnectorMode.Both)
                {
                    var List = InConnectors;
                    if (fromItem.InConnectors.IndexOf(connectorAdorner.SourceConnector) >= 0)
                        List = OutConnectors;

                    return GetConnector(position, List);
                }
                else
                {
                    // может быть связан как со входом, так и с выходом...
                    var List = InConnectors;
                    List.AddRange(OutConnectors);

                    return GetConnector(position, List);
                }
            }          
        }

        /// <summary>
        /// Полчить ближайший к точке коннектор из списка
        /// </summary>
        /// <param name="position"></param>
        /// <param name="?"></param>
        /// <param name="List"></param>
        /// <returns></returns>
        public Connector GetConnector(Point position,  List<Connector> List)
        {
            Connector con = null;
            double OldD = double.MaxValue;
            double NewD;
            foreach (var c in List)
            {
                var dx = (position.X - c.Position.X);
                var dy = (position.Y - c.Position.Y);
                NewD = dx * dx + dy * dy;
                if (NewD < OldD)
                {
                    con = c;
                    OldD = NewD;
                }
            }
            return con;
        }

        /// <summary>
        /// Создание новой связи определенного типа
        /// </summary>
        /// <remarks>Может "переворачивать стрелку", те менять местами sourse и  sink, если это необходимо</remarks>
        /// <param name="sourceConnector">От куда связь</param>
        /// <param name="sinkConnector">Куда связь</param>
        /// <returns>Связь между точками</returns>
        public virtual Connection GetNewConnection(Connector sourceConnector, Connector sinkConnector)
        {
            if ((sourceConnector.ParentDesignerItem as xDesignerItemBase).IsInOutConnectorMode)
            {
                if ((sourceConnector.ParentDesignerItem as xDesignerItemBase).OutConnectors.First() != sourceConnector)
                {
                    Connector tmp = sinkConnector;
                    sinkConnector = sourceConnector;
                    sourceConnector = tmp;
                }
            }

            /*!!!!*/
            return null;
            /*
            xConnectionLinkBase connectionObj = new xConnectionLinkBase((sourceConnector as TaskConnector).ConnectorObj,
                                                                        (sinkConnector as TaskConnector).ConnectorObj);
            connectionObj.New();
            */
            //return new QueryConnection(connectionObj, sourceConnector, sinkConnector);
        }

        /// <summary>
        /// Удаление элемента из диаграммы
        /// </summary>
        /// <remarks>Удлаяются все его связи и коннекторы</remarks>
        public override void Delete()
        {
            foreach (Connector connector in Container.Children.OfType<Connector>())
            {
                foreach (Connection c in connector.Connections)
                {
                    c.Delete(connector);
                }
            }
            Container.Children.Clear();
        }
        /// <summary>
        /// Добавить текст
        /// </summary>
        /// <param name="width">Ширина</param>
        /// <param name="heigth">Длинна</param>
        /// <param name="text">Собственно текст</param>
        /// <param name="fontSize">Рамер шрифта</param>
        /// <param name="left">Смещение слева</param>
        /// <param name="top">Смещение сверху</param>
        /// <param name="textAlignment">Привязка текста к краю</param>
        protected virtual TextBlock AddText(double width, 
                                            double heigth, 
                                            string text,
                                            double fontSize,
                                            double left,
                                            double top,
                                            TextAlignment textAlignment
                                            )
        {
            TextBlock tb = new TextBlock();
            tb.Width = width;
            tb.Height = heigth;
            tb.Background = null;// Brushes.Transparent; 
            //tb.Background = Brushes.Transparent;
            tb.Text = text;
            AddChild(tb);
            tb.FontSize = fontSize;
            tb.Margin = new Thickness(left, top, -left, -top);
            tb.TextAlignment = textAlignment;
            tb.TextWrapping = TextWrapping.Wrap;
            tb.IsHitTestVisible = false;
            
            return tb;
        }

        private TextBlock m_MainText = null;
        /// <summary>
        /// Основной текст объекта
        /// </summary>
        public TextBlock MainText
        {
            get { return m_MainText; }
            set { m_MainText = value; }
        }


        protected virtual void CreateContextMenu()
        {
            
        }

        public virtual void EndMoveElement()
        {

        }

    }
}