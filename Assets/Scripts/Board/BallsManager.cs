using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Pattern;
using ScriptableObjects;
using UnityEngine;

namespace Board
{
    public class BallsManager : Singleton<BallsManager>
    {
        public MovesContainer movesContainer;
        public Ball ballToMakeMove;
        public List<Ball> ballsLeft = new List<Ball>();
        public List<Ball> balls = new List<Ball>();
        public MoveDirectionCoord moveDirectionCoord;
        
        readonly Dictionary<MoveDirection,Vector3> directions = new Dictionary<MoveDirection, Vector3>()
        {
            {MoveDirection.Down,Vector3.back},
            {MoveDirection.Up,Vector3.forward},
            {MoveDirection.Left,Vector3.left},
            {MoveDirection.Right,Vector3.right},
        };
        private void Awake()
        {
            AssignInstance(this);
            movesContainer.movesMade.Clear();
        }
        
        public void CoordsInit()
        {
            ballsLeft.Clear();
            int ballCounter = 0;
            for (int i = 0; i < Config.BOARD_WIDTH; i++)
            {
                for (int j = 0; j < Config.BOARD_HEIGHT; j++)
                {
                    Ball ball = transform.GetChild(ballCounter).GetComponent<Ball>();
                    ballCounter++;
                    if (ball == null)
                    {
                        continue;
                    }

                    ball.ballId = ballCounter;
                    ball.coordInfo.SetCoord(i,j);
                    ballsLeft.Add(ball);
                }
            }
            balls.AddRange(ballsLeft);
            balls.ForEach(x=>x.gameObject.SetActive(true));
        }

        private void SaveMove(CoordInfo _selectedBall, CoordInfo _betweenBall, CoordInfo _selectedPot)
        {
            MoveInfo moveInfo = new MoveInfo()
            {
                from = new[] {_selectedBall.coord[0], _selectedBall.coord[1]},
                between = new[] {_betweenBall.coord[0], _betweenBall.coord[1]},
                to = new[] {_selectedPot.coord[0], _selectedPot.coord[1]}

            };
            movesContainer.movesMade.Add(moveInfo);
        }

        public void RestoreLastMove()
        {
            if (movesContainer.movesMade.Count <= 0)
            {
                return;
            }
                
            MoveInfo lastMove = movesContainer.movesMade.Last();
            Vector3 vector = Vector3.zero;
            if (lastMove.from[0] - lastMove.to[0] != 0)
            {
                vector = new Vector3(0f,0,(lastMove.to[0] - lastMove.from[0])/2f);
            }

            if (lastMove.from[1] - lastMove.to[1] != 0)
            {
                vector = new Vector3((lastMove.from[1] - lastMove.to[1])/2f,0,0);
            }
            
            Ball ballToRestore = ballsLeft.FirstOrDefault(x =>
                x.coordInfo.coord[0] == lastMove.to[0] && x.coordInfo.coord[1] == lastMove.to[1]);
            ballToRestore.transform.position += vector;
            ballToRestore.coordInfo.SetCoord(lastMove.from[0],lastMove.from[1]);

            Ball betweenBall = balls.FirstOrDefault(x =>
                x.coordInfo.coord[0] == lastMove.between[0] && x.coordInfo.coord[1] == lastMove.between[1]);
            betweenBall.gameObject.SetActive(true);
            ballsLeft.Add(betweenBall);
            
            PotManager.Instance.SetPotState(PotManager.Instance.FindPotByCoords(lastMove.from[0],lastMove.from[1]),PotState.Occupied);
            PotManager.Instance.SetPotState(PotManager.Instance.FindPotByCoords(lastMove.between[0],lastMove.between[1]),PotState.Occupied);
            PotManager.Instance.SetPotState(PotManager.Instance.FindPotByCoords(lastMove.to[0],lastMove.to[1]),PotState.Free);
            
            
            movesContainer.movesMade.RemoveAt(movesContainer.movesMade.Count-1);
            PotManager.Instance.OnUpdateScore();
        }

        public void RemoveBall(Ball _ballToRemove)
        {
            ballsLeft.Remove(_ballToRemove);
            _ballToRemove.gameObject.SetActive(false);
            
        }

        public void MoveBallToCoord(Ball _ballToMove, CoordInfo _destinationCoord,Ball _betweenBall)
        {
            MoveDirection direction = FindMoveDirection(_ballToMove, _destinationCoord);
            _ballToMove.transform.position += directions[direction];
            PotManager.Instance.SetPotState(PotManager.Instance.FindPotByCoords(_ballToMove.coordInfo),PotState.Free);
            PotManager.Instance.SetPotState(PotManager.Instance.FindPotByCoords(_betweenBall.coordInfo),PotState.Free);
            PotManager.Instance.SetPotState(PotManager.Instance.FindPotByCoords(_destinationCoord),PotState.Occupied);
            SaveMove(_ballToMove.coordInfo,_betweenBall.coordInfo,_destinationCoord);
            _ballToMove.coordInfo.SetCoord(_destinationCoord);
            RemoveBall(_betweenBall);
            PotManager.Instance.OnUpdateScore();
        }

        private MoveDirection FindMoveDirection(Ball _ballToMove, CoordInfo _destinationCoord)
        {
            if (_ballToMove.coordInfo.coord[0] - _destinationCoord.coord[0] != 0)
            {
                return _ballToMove.coordInfo.coord[0] - _destinationCoord.coord[0] > 0
                    ? MoveDirection.Up
                    : MoveDirection.Down;
            }
            if (_ballToMove.coordInfo.coord[1] - _destinationCoord.coord[1] != 0)
            {
                return _ballToMove.coordInfo.coord[1] - _destinationCoord.coord[1] > 0
                    ? MoveDirection.Left
                    : MoveDirection.Right;
            }

            return default;
        }

        public bool CheckIfMoveExist()
        {
            return ballsLeft.Any(ball => ball.CheckIfPossibleMoveExist());
        }

        public void HintBallWithPossibleMove()
        {
            foreach (Ball ball in ballsLeft)
            {
                if (ball.CheckIfPossibleMoveExist())
                {
                    ball.collider.enabled = false;
                    ball.meshRenderer.material.DOColor(Color.red, 0.5f).OnComplete(() =>
                    {
                        ball.meshRenderer.material.DOColor(Color.white, 0.5f).OnComplete(()=>
                        {
                            ball.collider.enabled = true;
                        });
                    });
                }
            }
        }

        public void SaveBoard()
        {
            List<SavableBall> savableBalls = new List<SavableBall>();
            foreach (Ball ball in ballsLeft)
            {
                SavableBall savableBall = new SavableBall()
                {
                    id = ball.ballId,
                    coord = ball.coordInfo.coord,
                    savableVector3 = ball.transform.position.ToSavableVector3()
                };
                savableBalls.Add(savableBall);
            }

            Settings.Instance.savableBoard.savableBalls = savableBalls;
        }
        
    }
}
