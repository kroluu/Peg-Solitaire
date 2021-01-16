using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Menu
{
    
    public class MainMenuArrowButton : AnimatedButton
    {
        [SerializeField] private ArrowsType arrowsType; 
        [SerializeField] private ArrowState arrowState;
        private MainMenuResolutionController mainMenuResolutionController;
        private MainMenuQualityController mainMenuQualityController;
        protected new void Awake()
        {
            switch (arrowsType)
            {
                case ArrowsType.Resolution:
                    mainMenuResolutionController = GetComponentInParent<MainMenuResolutionController>();
                    break;
                case ArrowsType.Quality:
                    mainMenuQualityController = GetComponentInParent<MainMenuQualityController>();
                    break;
            }
        }

        public override void OnPointerClick(PointerEventData eventData)
        {
            switch (arrowsType)
            {
                case ArrowsType.Resolution:
                    mainMenuResolutionController.SetResolution(arrowState);
                    break;
                case ArrowsType.Quality:
                    mainMenuQualityController.SetQuality(arrowState);
                    break;
            }
        }
    }
}
