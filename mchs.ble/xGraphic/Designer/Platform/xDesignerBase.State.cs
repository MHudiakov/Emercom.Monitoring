using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using xGraphic;
using System.Windows.Controls;

namespace xGraphic
{
    /// <summary>
    /// Класс, управляющий диаграммой задач
    /// </summary>
    public partial class xDesignerBase : DesignerCanvas
    {

        public enum DiagramState
        {
            Default,
            PaintTaskArrow,
            PaintTimeArrow,
            RazdvigatDiagram,
            MoveXY,
        }

        DiagramState m_State = DiagramState.Default;
        public DiagramState State
        {
            get { return m_State; }
            set 
            {
                DiagramState old = m_State;
                m_State = value;
                OnSetState(old, m_State);
            }
        }

        xDesignerRazdvig m_ActualDesignerRazdvig = null;
        public xDesignerRazdvig ActualDesignerRazdvig
        {
            get { return m_ActualDesignerRazdvig; }
            set { m_ActualDesignerRazdvig = value; }
        }

        protected virtual void OnSetState(DiagramState oldState, DiagramState newState)
        {
            if (State == DiagramState.Default)
            {
                Cursor = Cursors.Arrow;
            }
            
            if (State == DiagramState.RazdvigatDiagram)
            {
                ActualDesignerRazdvig = new xDesignerRazdvig(this);
                //Cursor = Cursors.SizeNS;
                Cursor = Cursors.ScrollNS;
            }
        }

        protected virtual void DoOnMouseDownInState(MouseButtonEventArgs e)
        {
            if (State == DiagramState.RazdvigatDiagram)
            {
                if (ActualDesignerRazdvig != null)
                    ActualDesignerRazdvig.Down(e);
            }
        }

        protected virtual void DoOnMouseUpInState(MouseButtonEventArgs e)
        {
            if (State == DiagramState.RazdvigatDiagram)
            {
                if (ActualDesignerRazdvig != null)
                {
                    ActualDesignerRazdvig.Up(e);
                    Children.Remove(ActualDesignerRazdvig);
                    ActualDesignerRazdvig = null;
                    State = DiagramState.Default;
                }
            }

            if (State == DiagramState.MoveXY)
            {
                State = DiagramState.Default;
                DoEndMoveXY(e);
            }
        }

        private void DoEndMoveXY(MouseButtonEventArgs e)
        {
            throw new NotImplementedException();
        }
        
        protected virtual void DoOnMouseMoveInState(MouseEventArgs e)
        {
            if (State == DiagramState.RazdvigatDiagram)
            {
                if (ActualDesignerRazdvig != null)
                    ActualDesignerRazdvig.Move(e);                
            }

            if (State == DiagramState.MoveXY)
            {
                DoMoveXY(e);
            }
        }

        private void DoMoveXY(MouseEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}