using System;
using DG.Tweening;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Menu
{
    public class MainMenuResolutionController : MonoBehaviour
    {
        private TextMeshProUGUI resolutionText;

        private void Awake()
        {
            resolutionText = GetComponentInChildren<TextMeshProUGUI>();
            SetResolution();
        }

        public void SetResolution(ArrowState _arrowState)
        {
            int currentResolution = Settings.Instance.gameResolution;
            currentResolution += _arrowState == ArrowState.Next?1:-1;
            
            if (currentResolution > Settings.Instance.availableResolutions.Length - 1)
            {
                currentResolution = 0;
            }
            else if (currentResolution < 0)
            {
                currentResolution = Settings.Instance.availableResolutions.Length - 1;
            }

            Settings.Instance.gameResolution = currentResolution;
            SetResolution();
        }

        private void SetResolution()
        {
            Resolution selectedResolution = Settings.Instance.availableResolutions[Settings.Instance.gameResolution];
            resolutionText.text = $"{selectedResolution.width}x{selectedResolution.height}";
            Settings.Instance.SetResolution();
        }
    }
}
