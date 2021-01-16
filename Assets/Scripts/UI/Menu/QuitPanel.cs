using System;
using DG.Tweening;
using UI.Core;
using UnityEngine;

namespace UI.Menu
{
    public class QuitPanel : MonoBehaviour,IPanelViewController
    {
        private CanvasGroup exitGroup;
        private Tweener singleTweener;
        [SerializeField] private AnimatedButton closeGameAnimatedButton;
        [SerializeField] private AnimatedButton backToGameAnimatedButton;


        private void Awake()
        {
            exitGroup = GetComponent<CanvasGroup>();
            backToGameAnimatedButton.contextToSet = MainMenuContext.Main;
        }

        public void ShowPanel()
        {
            singleTweener?.Kill();
            singleTweener = DOVirtual.Float(exitGroup.alpha, 1f, Config.TIME_FOR_SHOW_MENU_PANEL, (value) => exitGroup.alpha = value);
            exitGroup.blocksRaycasts = true;
        }

        public void HidePanel()
        {
            singleTweener?.Kill();
            singleTweener = DOVirtual.Float(exitGroup.alpha, 0f, Config.TIME_FOR_HIDE_MENU_PANEL,(value) =>
            {
                exitGroup.alpha = value;
            });
            exitGroup.blocksRaycasts = false;
        }
    }
}
