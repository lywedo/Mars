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

        private void Start()
        {
            ButtonImpl.MouseEvent += MouseEvent;
        }

        public void SetIndex(int index)
        {
            _index = index;
        }

        private void MouseEvent(ButtonEvent @event)
        {
            // Debug.Log($"mouse:{@event}");
            switch (@event)
            {
                case ButtonEvent.Highlighted:
                    ShowText.color = FocusColor;
                    break;
                case ButtonEvent.Normal:
                    ShowText.color = NormalColor;
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