using System;
using DG.Tweening;
using Managers;
using UI.Core;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI.Menu
{
    public class MainMenuUiController : UiCoreController<MainMenuPanel, MainMenuContext>
    {
        protected override CoreContext CoreContext { get; set; }
        [SerializeField] private Image fadeImage;
        public override void SwitchContext(MainMenuContext _context)
        {
            switch (_context)
            {
                case MainMenuContext.Main:
                    SetPanel(MainMenuPanel.MainPanel);
                    break;
                case MainMenuContext.Load:
                    SetPanel(MainMenuPanel.LoadPanel);
                    break;
                case MainMenuContext.Options:
                    SetPanel(MainMenuPanel.OptionsPanel);
                    break;
                case MainMenuContext.Quit:
                    SetPanel(MainMenuPanel.QuitPanel);
                    break;
                case MainMenuContext.Start:
                    SceneLoader.Fade(fadeImage,FadeOption.In,()=>SceneLoader.LoadScene(Scenes.Game));
                    break;
            }

            PreviousContext = CurrentContext;
            CurrentContext = _context;
        }

        protected override void Awake()
        {
            FindPanelsInHierarchy();
            CoreContext = CoreContext.Menu;
            SetPanel(MainMenuPanel.MainPanel);
            SceneLoader.Fade(fadeImage,FadeOption.Out);
            AssignReferenceToCore(this);
        }

        protected override void Start()
        {

        }

        private void ShowBackgroundElements()
        {
            
        }

        /*private void Fade(FadeOption _fadeOption,Action _onCompleteFadeAction)
        {
            float fadeAmount = 0f;
            switch (_fadeOption)
            {
                case FadeOption.In:
                    fadeAmount = 1f;
                    break;
            }

            DOVirtual.Float(fadeImage.color.a, fadeAmount, 1.5f, (_value) =>
            {
                Color color = fadeImage.color;
                color.a = _value;
                fadeImage.color = color;
            }).OnComplete(() =>
            {
               _onCompleteFadeAction?.Invoke();
            });

            
        }*/
    }
}
