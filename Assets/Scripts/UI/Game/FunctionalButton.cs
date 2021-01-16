using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Game
{
    public class FunctionalButton : Button
    {
        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.5f);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            transform.DOScale(Vector3.one, 0.5f);
        }

    }
}
