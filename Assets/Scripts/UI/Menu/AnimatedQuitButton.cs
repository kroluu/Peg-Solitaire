using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Menu
{
    public class AnimatedQuitButton : AnimatedButton
    {
        public override void OnPointerClick(PointerEventData eventData)
        {
            Application.Quit();
        }
    }
}
