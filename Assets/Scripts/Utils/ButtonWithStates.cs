using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Utils
{
    public class ButtonWithStates : Button
    {
        public bool Pressed = false;
    
        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            Pressed = true;
        }
 
        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            Pressed = false;
        }
    }
}

