using System;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class ShowCanvasItemController : MonoBehaviour
    {
        public Text ShowText;
        public ButtonImpl ButtonImpl;
        public Color FocusColor;
        public Color NormalColor;
        private int _index = 0;

        public delegate void OnClickHandlerDelegate(int index);

        public OnClickHandlerDelegate OnClickHandler;

        public delegate void OnFocusHandlerDelegate(int index);

        public OnFocusHandlerDelegate OnFocusHandler;

        private void Start()
        {
            ButtonImpl.MouseEvent += MouseEvent;
        }

        public void SetIndex(int index)
        {
            _index = index;
        }

        public void SetFocusColor()
        {
            ShowText.color = FocusColor;
            OnFocusHandler?.Invoke(_index);
        }

        public void SetNormalColor()
        {
            ShowText.color = NormalColor;
        }

        private void MouseEvent(ButtonEvent @event)
        {
            // Debug.Log($"mouse:{@event}");
            switch (@event)
            {
                case ButtonEvent.Highlighted:
                    SetFocusColor();
                    break;
                case ButtonEvent.Normal:
                    SetNormalColor();
                    break;
                case ButtonEvent.Selected:
                    OnClickHandler?.Invoke(_index);
                    break;
            }
        }

        private void OnDestroy()
        {
            ButtonImpl.MouseEvent -= MouseEvent;
        }
    }
}