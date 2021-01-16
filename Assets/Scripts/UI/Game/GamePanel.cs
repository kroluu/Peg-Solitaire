using System;
using Board;
using DG.Tweening;
using TMPro;
using UI.Core;
using UnityEngine;

namespace UI.Game
{
    public class GamePanel : MonoBehaviour,IPanelViewController
    {
        private CanvasGroup canvasGroup;
        [SerializeField] private TextMeshProUGUI scoreText;
        private CanvasGroup leftScoreCanvasGroup;
        private Tweener singleTweener;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup.alpha = 0f;
            leftScoreCanvasGroup = scoreText.GetComponent<CanvasGroup>();
        }

        public void ShowPanel()
        {
            singleTweener?.Kill();
            singleTweener = DOVirtual.Float(canvasGroup.alpha, 1f, Config.TIME_FOR_SHOW_MENU_PANEL, (value) => canvasGroup.alpha = value);
            canvasGroup.blocksRaycasts = !canvasGroup.blocksRaycasts;
        }

        public void HidePanel()
        {
            singleTweener?.Kill();
            singleTweener = DOVirtual.Float(canvasGroup.alpha, 0f, Config.TIME_FOR_HIDE_MENU_PANEL,(value) =>
            {
                canvasGroup.alpha = value;
            });
            canvasGroup.blocksRaycasts = !canvasGroup.blocksRaycasts;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                RefreshScore();
            }
        }

        public void RefreshScore()
        {
            DOVirtual.Float(leftScoreCanvasGroup.alpha, 0f, 0.25f, (_value) =>
            {
                leftScoreCanvasGroup.alpha = _value;
            }).OnComplete((() =>
            {
                scoreText.text = BallsManager.Instance.ballsLeft.Count.ToString();
                DOVirtual.Float(leftScoreCanvasGroup.alpha, 1f, 0.25f, (_value) =>
                {
                    leftScoreCanvasGroup.alpha = _value;
                });
            }));
        }
    }
}
