using DG.Tweening;
using UI.Core;
using Ui.Game;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Game
{
    public class AnimatedGameButton : Button
    {
        [SerializeField] private InGameContext contextToSet;
        private Image buttonImage;
        protected override void Awake()
        {
            base.Awake();
            buttonImage = GetComponent<Image>();
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            if (eventData.pointerPress == this.gameObject)
            {
                DOVirtual.Float(0f, 1f, 1f, (value) =>
                    {
                        buttonImage.color = Color.Lerp(buttonImage.color, Color.red, value);
                    }
                );
            }
            transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.5f);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            transform.DOScale(Vector3.one, 0.5f);
            DOVirtual.Float(0f, 1f, 1f, (value) =>
                {
                    buttonImage.color = Color.Lerp(buttonImage.color, Color.white, value);
                }
            );
        }
        
        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            DOVirtual.Float(0f, 1f, 1f, (value) =>
                {
                    buttonImage.color = Color.Lerp(buttonImage.color, Color.red, value);
                }
            );
        }
        
        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            DOVirtual.Float(0f, 1f, 1f, (value) =>
                {
                    buttonImage.color = Color.Lerp(buttonImage.color, Color.white, value);
                }
            );
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            base.OnPointerClick(eventData);
            GameUiController.GetController<GameUiController>(CoreContext.Game).SwitchContext(contextToSet);
        }
    }
}
