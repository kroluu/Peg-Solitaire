using DG.Tweening;
using Managers;
using UI.Core;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Menu
{
    public class AnimatedButton : Button
    {
        [SerializeField]private Image buttonImage;
        public MainMenuContext contextToSet;
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
            if (contextToSet == MainMenuContext.Start)
            {
                SceneLoader.loadGameFromSave = false;
            }
            MainMenuUiController.GetController<MainMenuUiController>(CoreContext.Menu).SwitchContext(contextToSet);
        }
    }
}
