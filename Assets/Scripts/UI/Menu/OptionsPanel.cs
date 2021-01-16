using DG.Tweening;
using ScriptableObjects;
using TMPro;
using UI.Core;
using UnityEngine;

namespace UI.Menu
{
    public class OptionsPanel : MonoBehaviour,IPanelViewController
    {
        public AnimatedButton backButton;
        private CanvasGroup optionsGroup;
        
        
        private Tweener singleTweener;
        
        [Header("Options References")]
        [SerializeField] TextMeshProUGUI resolutionText;
        
        private void Awake()
        {
            optionsGroup = GetComponent<CanvasGroup>();
            optionsGroup.alpha = 0f;
            backButton.contextToSet = MainMenuContext.Main;
        }


        public void ShowPanel()
        {
            singleTweener?.Kill();
            singleTweener = DOVirtual.Float(optionsGroup.alpha, 1f, Config.TIME_FOR_SHOW_MENU_PANEL, (value) => optionsGroup.alpha = value);
            optionsGroup.blocksRaycasts = true;
        }

        public void HidePanel()
        {
            Settings.Instance.SaveSettings();
            singleTweener?.Kill();
            singleTweener = DOVirtual.Float(optionsGroup.alpha, 0f, Config.TIME_FOR_HIDE_MENU_PANEL,(value) =>
            {
                optionsGroup.alpha = value;
            });
            optionsGroup.blocksRaycasts = false;
        }
    }
}
