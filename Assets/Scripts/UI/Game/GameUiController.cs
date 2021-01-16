using System;
using System.Collections;
using System.Collections.Generic;
using Board;
using Machine;
using Managers;
using ScriptableObjects;
using UI.Core;
using UI.Game;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Game
{
    public class GameUiController : UiCoreController<InGamePanel,InGameContext>
    {
        public GamePanel gamePanel;
        
        [SerializeField] private Image fadeImage;
        [SerializeField] private Button resetButton;
        [SerializeField] private Button restoreMove;
        [SerializeField] private Button hintMove;
        [SerializeField] private Button startAgain;
        [SerializeField] private Button gameOverMenu;
        
        protected override CoreContext CoreContext { get; set; }
        
        public override void SwitchContext(InGameContext context)
        {
            switch (context)
            {
                case InGameContext.Main:
                    SetPanel(InGamePanel.MainPanel);
                    break;
                case InGameContext.GameOver:
                    SetPanel(InGamePanel.GameOverPanel);
                    break;
                case InGameContext.Menu:
                    SceneLoader.Fade(fadeImage, FadeOption.In, () => SceneLoader.LoadScene(Scenes.Menu));
                    break;
                case InGameContext.Save:
                    BallsManager.Instance.SaveBoard();
                    PotManager.Instance.SavePots();
                    Settings.Instance.SaveBoard();
                    break;
                
            }
        }

        protected override void Awake()
        {
            FindPanelsInHierarchy();
            CoreContext = CoreContext.Game;
            SetPanel(InGamePanel.MainPanel);
            AssignReferenceToCore(this);
            gamePanel = GetComponentInChildren<UI.Game.GamePanel>();
            PotManager.Instance.OnUpdateScoreAction += gamePanel.RefreshScore;
            StateMachineManager.Instance.OnGameOverAction += ()=>SwitchContext(InGameContext.GameOver);
        }

        private void OnDestroy()
        {
            StateMachineManager.Instance.OnGameOverAction -= ()=>SwitchContext(InGameContext.GameOver);
        }

        protected override void Start()
        {
            resetButton.onClick.AddListener(RestartGame);
            restoreMove.onClick.AddListener(RestoreMove);
            hintMove.onClick.AddListener(HintMove);
            gameOverMenu.onClick.AddListener(GameOverBackToMenu);
            startAgain.onClick.AddListener(TryAgain);
        }
        
        private void RestartGame()
        {
            StateMachineManager.Instance.stateMachine.Fire(MachineTrigger.StartAgain);
            
        }

        private void RestoreMove()
        {
            BallsManager.Instance.RestoreLastMove();
        }

        private void HintMove()
        {
            BallsManager.Instance.HintBallWithPossibleMove();
        }

        private void GameOverBackToMenu()
        {
            SwitchContext(InGameContext.Menu);
        }

        private void TryAgain()
        {
            SwitchContext(InGameContext.Main);
            StateMachineManager.Instance.stateMachine.Fire(MachineTrigger.StartAgain);
        }
        
        

}
}
