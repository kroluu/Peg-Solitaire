using System;
using System.Collections.Generic;
using System.Linq;
using ScriptableObjects;
using UnityEngine;

namespace Board
{
    public class Ball : MonoBehaviour, IMouseDetect
    {
        public int ballId;
        public CoordInfo coordInfo;
        public MeshRenderer meshRenderer;
        public Collider collider;
        private void Awake()
        {
            coordInfo = new CoordInfo();
        }

        public void OnMouseEnter()
        {
            RaycastInfo.detectedGameElement = this;
        }

        public void OnMouseExit()
        {
            RaycastInfo.detectedGameElement = null;
        }

        public bool CheckIfPossibleMoveExist()
        {
            foreach (MoveDirection direction in (MoveDirection[])Enum.GetValues(typeof(MoveDirection)))
            {
                if (IsMoveValid(direction,typeof(Ball)))
                {
                    if (IsMoveValid(direction, typeof(Pot)))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool IsMoveValid(MoveDirection _direction,Type type)
        {
            CoordInfo ballCoord = new CoordInfo();
            if (type == typeof(Ball))
            {
                ballCoord.coord[0] = coordInfo.coord[0] +
                                     BallsManager.Instance.moveDirectionCoord.checkMoves[(int) _direction][0];
                ballCoord.coord[1] = coordInfo.coord[1] +
                                     BallsManager.Instance.moveDirectionCoord.checkMoves[(int) _direction][1];

                return BallsManager.Instance.ballsLeft.Exists(x => x.coordInfo.coord[0] == ballCoord.coord[0]
                                                                   && x.coordInfo.coord[1] == ballCoord.coord[1]);
            }

            if (type == typeof(Pot))
            {
                ballCoord.coord[0] = coordInfo.coord[0] +
                                     BallsManager.Instance.moveDirectionCoord.checkMoves[(int) _direction][0]*2;
                ballCoord.coord[1] = coordInfo.coord[1] +
                                     BallsManager.Instance.moveDirectionCoord.checkMoves[(int) _direction][1]*2;

                Pot pot = PotManager.Instance.potsList.FirstOrDefault(x => x.coordInfo.coord[0] == ballCoord.coord[0]
                                                                         && x.coordInfo.coord[1] == ballCoord.coord[1]);
                return pot && pot.potState == PotState.Free;
            }

            return false;
        }

        public void LoadBall(SavableBall _savableBall)
        {
            transform.position = _savableBall.savableVector3.ConvertToVector3();
            coordInfo.SetCoord(_savableBall.coord[0],_savableBall.coord[1]);
        }
    }
    
    
}


