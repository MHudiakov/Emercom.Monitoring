using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;
using DevExpress.XtraEditors;

namespace xGraphic
{
    public partial class xDesignerBase : DesignerCanvas
    {

        protected object ParentBuilderObject { get; set; }
        
        /// <summary>
        /// Основнйо конструктор
        /// </summary>
        /// <param name="parent">основной объект, относительно которой строим все.</param>
        public xDesignerBase(object parent)
            : base()
        {
            ParentBuilderObject = parent;
            this.RenderTransform = m_Scale;
        }

        #region Scale & Scroll

        ScaleTransform m_Scale = new ScaleTransform(1, 1);
        /// <summary>
        /// Масштаб
        /// </summary>
        public ScaleTransform Scale
        {
            get
            {
                return m_Scale;
            }
            set
            {
                m_Scale = value;
                if (m_ZoomControl != null)
                    m_ZoomControl.Value = Convert.ToInt32(m_Scale.ScaleX * 100);
                AplayTransform();
            }
        }

        TranslateTransform m_Translate = new TranslateTransform(0, 0);
        public TranslateTransform Translate
        {
            get
            {
                return m_Translate;
            }
            set
            {
                m_Translate = value;
                if (m_Translate.X > 0)
                    m_Translate.X = 0;
                if (m_Translate.Y > 15)
                    m_Translate.Y = 15;

                AplayTransform();
            }
        }

        double m_MinScale = 0.01;
        /// <summary>
        /// Минимальный масштаб
        /// </summary>
        public double MinScale
        {
            get { return m_MinScale; }
            set
            {
                if (value > 0)
                    m_MinScale = value;
                else
                    m_MinScale = 0.01;
            }
        }

        double m_MaxScale = 100;
        /// <summary>
        /// Максимальный масштаб
        /// </summary>
        public double MaxScale
        {
            get { return m_MaxScale; }
            set
            {
                if (value > 0)
                    m_MaxScale = value;
                else
                    m_MaxScale = 0.01;
            }
        }

        VScrollBar m_VScroll = null;

        public VScrollBar VScroll
        {
            get { return m_VScroll; }
            set { m_VScroll = value; }
        }

        HScrollBar m_HScroll = null;

        public HScrollBar HScroll
        {
            get { return m_HScroll; }
            set
            {
                m_HScroll = value;
                // m_HScroll.Maximum = Convert.ToInt32(2000 * m_Scale.ScaleX);
                // m_HScroll.Value = Convert.ToInt32(m_Translate.X / m_Scale.ScaleX);
                // m_HScroll.in
            }
        }

        ZoomTrackBarControl m_ZoomControl = null;
        public ZoomTrackBarControl ZoomControl
        {
            get { return m_ZoomControl; }
            set
            {
                m_ZoomControl = value;
                m_ZoomControl.Value = Convert.ToInt32(m_Scale.ScaleX * 100);
                m_ZoomControl.EditValueChanging += delegate(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
                {
                    double xScale = Convert.ToDouble((sender as ZoomTrackBarControl).Value) / 100;
                    Scale = new ScaleTransform(xScale, xScale);
                };

            }
        }


        private void MouseWheelScaleScroll(System.Windows.Input.MouseWheelEventArgs e)
        {
            if ((Keyboard.Modifiers & ModifierKeys.Control) != ModifierKeys.None)
            {
                double oldScale = Scale.ScaleX;
                double newScale = oldScale;
                if (e.Delta > 0)
                    newScale = oldScale * 1.2;
                if (e.Delta < 0)
                    newScale = oldScale / 1.2;
                // SetScale(newScale);
                Scale = new ScaleTransform(newScale, newScale);
            }
            else
                if ((Keyboard.Modifiers & ModifierKeys.Shift) != ModifierKeys.None)
                {
                    double OldX = Translate.X;
                    double OldY = Translate.Y;
                    double newX = OldX;
                    if (e.Delta > 0)
                        newX = OldX + 50;
                    if (e.Delta < 0)
                        newX = OldX - 50;
                    // SetScale(newScale);
                    Translate = new TranslateTransform(newX, OldY);
                }
                else
                {
                    double OldX = Translate.X;
                    double OldY = Translate.Y;
                    double newY = OldY;
                    if (e.Delta > 0)
                        newY = OldY + 50;
                    if (e.Delta < 0)
                        newY = OldY - 50;
                    Translate = new TranslateTransform(OldX, newY);
                }
        }

        private void AplayTransform()
        {
            Matrix m = new Matrix(m_Scale.Value.M11, m_Scale.Value.M12, m_Scale.Value.M21, m_Scale.Value.M22, m_Scale.Value.OffsetX, m_Scale.Value.OffsetY);
            m.Translate(m_Translate.X, m_Translate.Y);
            RenderTransform = new MatrixTransform(m);
        }

        /// <summary>
        /// Установить масштаб, с учетом цента
        /// </summary>
        /// <param name="scale">(1 = 100%)</param>
        /// <param name="centerX">Будущий центр экрана после масштабирования по X</param>
        /// <param name="centerY">Будущий центр экрана после масштабирования по Y</param>
        public void SetScale(double scale, double centerX, double centerY)
        {
            m_Scale.SetValue(ScaleTransform.CenterXProperty, scale);
            m_Scale.SetValue(ScaleTransform.CenterYProperty, scale);
            m_Scale.SetValue(ScaleTransform.ScaleXProperty, centerX);
            m_Scale.SetValue(ScaleTransform.ScaleYProperty, centerY);
        }

        /// <summary>
        /// Установить масштаб
        /// </summary>
        /// <param name="scale">Маштаб (1 = 100%)</param>
        public void SetScale(double scale)
        {
            SetScale(scale, 0, 0);
        }

        #endregion Scale & Scroll

        System.Windows.Controls.ContextMenu m_SelectionMenu = null;
        /// <summary>
        /// Основное меню элементов
        /// </summary>
        public System.Windows.Controls.ContextMenu SelectionMenu
        {
            get { return m_SelectionMenu; }
            set
            {
                m_SelectionMenu = value;
                this.ContextMenu = m_SelectionMenu;
            }
        }

        /// <summary>
        /// Событие колесика мыши
        /// </summary>
        protected override void OnMouseWheel(System.Windows.Input.MouseWheelEventArgs e)
        {
            MouseWheelScaleScroll(e);
            base.OnMouseWheel(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.Key == Key.Delete)
                DeleteCurrentSelection();
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            DoOnMouseDownInState(e);
            if ( true /* если ни по кому не кликнули - тогда */ )
            {

            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (State == DiagramState.Default)
                base.OnMouseMove(e);
            else
                DoOnMouseMoveInState(e);
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);
            DoOnMouseUpInState(e);
        }
    }
}
