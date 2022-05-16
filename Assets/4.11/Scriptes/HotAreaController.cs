using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class HotAreaController : MonoBehaviour
    {
        public ButtonImpl ButtonImpl;
        public Sprite NormalSprite;
        public Sprite SelectSprite;
        public Image ShowImage;
        
        private void Start()
        {
            ButtonImpl.MouseEvent += MouseEvent;
        }
        
        public void SetFocusColor()
        {
            ShowImage.sprite = SelectSprite;
        }

        public void SetNormalColor()
        {
            ShowImage.sprite = NormalSprite;
        }

        private void MouseEvent(ButtonEvent @event)
        {
            switch (@event)
            {
                case ButtonEvent.Highlighted:
                    SetFocusColor();
                    break;
                case ButtonEvent.Normal:
                    SetNormalColor();
                    break;
                case ButtonEvent.Selected:
                    // SetNormalColor();
                    Global.GetInstance().Back2Mars();
                    break;
            }
        }
    }
}