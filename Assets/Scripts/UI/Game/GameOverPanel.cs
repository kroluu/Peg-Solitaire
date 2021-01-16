using System;
using Board;
using DG.Tweening;
using TMPro;
using UI.Core;
using UnityEngine;

namespace UI.Game
{
    public class GameOverPanel : MonoBehaviour,IPanelViewController
    {
        private CanvasGroup gameOverGroup;
        [SerializeField] private CanvasGroup backgroundGroup;

        [SerializeField] private TextMeshProUGUI scoreTitleText;
        [SerializeField] private TextMeshProUGUI scoreText;

        private Tweener singleTweener;

        private void Awake()
        {
            gameOverGroup = GetComponent<CanvasGroup>();
            gameOverGroup.alpha = 0f;
        }

        public void ShowPanel()
        {
            ShowEndScore();
            singleTweener?.Kill();
            singleTweener = DOVirtual.Float(gameOverGroup.alpha, 1f, Config.TIME_FOR_SHOW_MENU_PANEL, (value) => gameOverGroup.alpha = value);
            gameOverGroup.blocksRaycasts = true;
        }

        public void HidePanel()
        {
            singleTweener?.Kill();
            singleTweener = DOVirtual.Float(gameOverGroup.alpha, 0f, Config.TIME_FOR_HIDE_MENU_PANEL,(value) =>
            {
                gameOverGroup.alpha = value;
            });
            gameOverGroup.blocksRaycasts = false;
        }

        private void ShowEndScore()
        {
            scoreTitleText.text = BallsManager.Instance.ballsLeft.Count == 1 ? "YOU WIN": "GAME OVER" ;
            
            scoreText.text = $"LEFT BALL" + (BallsManager.Instance.ballsLeft.Count > 1 ? "S" : "") + $": {BallsManager.Instance.ballsLeft.Count}";
        }
        
    }
}
