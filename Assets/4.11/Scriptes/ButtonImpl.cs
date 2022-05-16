using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public enum ButtonEvent
    {
        Highlighted,
        Normal,
        Selected
    }
    public class ButtonImpl : Button
    {
        public delegate void MouseEventDelegate(ButtonEvent @event);

        public MouseEventDelegate MouseEvent;
        protected override void DoStateTransition(SelectionState state, bool instant)
        {
            // base.DoStateTransition(state, instant);
            Debug.Log($"{state}  {instant}");
            switch (state)
            {
                case SelectionState.Highlighted:
                    MouseEvent?.Invoke(ButtonEvent.Highlighted);
                    break;
                case SelectionState.Normal: 
                    MouseEvent?.Invoke(ButtonEvent.Normal);
                    break; 
                // case SelectionState.Selected:
                //     MouseEvent?.Invoke(ButtonEvent.Highlighted);
                //     break;
            }
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            // base.OnPointerClick(eventData);
            Debug.Log("submit");
            MouseEvent?.Invoke(ButtonEvent.Selected);
        }

        
    }
}