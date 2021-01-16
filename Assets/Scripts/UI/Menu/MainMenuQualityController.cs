using ScriptableObjects;
using TMPro;
using UnityEngine;

namespace UI.Menu
{
    public class MainMenuQualityController : MonoBehaviour
    {
        private TextMeshProUGUI resolutionText;

        private void Awake()
        {
            resolutionText = GetComponentInChildren<TextMeshProUGUI>();
            SetQuality();
        }

        public void SetQuality(ArrowState _arrowState)
        {
            int currentQuality = (int)Settings.Instance.Quality;
            currentQuality += _arrowState == ArrowState.Next?1:-1;
            
            if (currentQuality > Extensions.GetEnumCount<Quality>()-1)
            {
                currentQuality = 0;
            }
            else if (currentQuality < 0)
            {
                currentQuality = Extensions.GetEnumCount<Quality>() - 1;
            }

            Settings.Instance.Quality = (Quality)currentQuality;
            SetQuality();
        }

        private void SetQuality()
        {
            resolutionText.text = $"{Settings.Instance.Quality}";
            Settings.Instance.SetQuality();
        }
    }
}
