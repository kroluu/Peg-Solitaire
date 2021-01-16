using System;
using System.Collections.Generic;
using DG.Tweening;
using ScriptableObjects;
using UI.Core;
using UnityEngine;

namespace UI.Menu
{
    public class MainPanel : MonoBehaviour,IPanelViewController
    {
        private CanvasGroup menuGroup;
        [SerializeField] private CanvasGroup backgroundGroup;
        [SerializeField] private Transform buttonContent;
        
        private List<AnimatedButton> listOfButtons = new List<AnimatedButton>();


        private Tweener singleTweener;
        private void Awake()
        {
            DOVirtual.Float(0, 1f, Config.TIME_FOR_SHOW_MENU_PANEL, (value) => backgroundGroup.alpha = value);
            menuGroup = GetComponent<CanvasGroup>();
            menuGroup.alpha = 0f;
            MainMenuContext[] contexts = (MainMenuContext[]) Enum.GetValues(typeof(MainMenuContext));
            foreach (Transform child in buttonContent)
            {
                AnimatedButton animatedButton = child.GetComponent<AnimatedButton>();
                animatedButton.contextToSet = contexts[listOfButtons.Count+1];
                listOfButtons.Add(animatedButton);
            }
        }

        public void ShowPanel()
        {
            singleTweener?.Kill();
            singleTweener = DOVirtual.Float(menuGroup.alpha, 1f, Config.TIME_FOR_SHOW_MENU_PANEL, (value) => menuGroup.alpha = value);
            menuGroup.blocksRaycasts = !menuGroup.blocksRaycasts;
        }

        public void HidePanel()
        {
            singleTweener?.Kill();
            singleTweener = DOVirtual.Float(menuGroup.alpha, 0f, Config.TIME_FOR_HIDE_MENU_PANEL,(value) =>
            {
                menuGroup.alpha = value;
            });
            menuGroup.blocksRaycasts = !menuGroup.blocksRaycasts;
        }
    }
}
