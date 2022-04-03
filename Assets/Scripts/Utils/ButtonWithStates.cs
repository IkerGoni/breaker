using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Utils
{
    public class ButtonWithStates : Button, IPointerDownHandler, IPointerUpHandler
    {
        public bool Pressed = false;
    
        public void OnPointerDown(PointerEventData eventData)
        {
            Pressed = true;
        }
 
        public void OnPointerUp(PointerEventData eventData)
        {
            Pressed = false;
        }
        public void Update()
        {
            if(IsPressed())
            {
                WhilePressed();
            }
        }
 
        public void WhilePressed()
        {
            //Move your guys
        }
    }
}

