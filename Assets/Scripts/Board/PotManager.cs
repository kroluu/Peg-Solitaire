using System;
using System.Collections.Generic;
using System.Linq;
using Pattern;
using ScriptableObjects;
using UnityEditor;
using UnityEngine;

namespace Board
{
    public class PotManager : Singleton<PotManager>
    {
        public event Action OnUpdateScoreAction;
        
        public List<Pot> potsList = new List<Pot>();
        private void Awake()
        {
            AssignInstance(this);
        }

        private void OnDestroy()
        {
            OnUpdateScoreAction = null;
        }

        public void OnUpdateScore()
        {
            OnUpdateScoreAction?.Invoke();
        }

        public void CoordsInit()
        {
            potsList.Clear();
            int potCounter = 0;
            for (int i = 0; i < Config.BOARD_WIDTH; i++)
            {
                for (int j = 0; j < Config.BOARD_HEIGHT; j++)
                {
                    if (i == 0 || i == 1 || i == 5 || i == 6)
                    {
                        if (j == 0 || j == 1)
                        {
                            j = 2;
                        }
                        else if (j == 5 || j == 6)
                        {
                            j = Config.BOARD_HEIGHT;
                            continue;
                        }
                    }
                    
                    Pot pot = transform.GetChild(potCounter).GetComponent<Pot>();
                    pot.potState = PotState.Occupied;
                    potCounter++;
                    if (pot == null)
                    {
                        continue;
                    }

                    pot.potId = potCounter;
                    pot.coordInfo.SetCoord(i,j);
                    potsList.Add(pot);
                }
            }
        }

        public Pot FindPotByCoords(CoordInfo _potCoords)
        {
            return potsList.FirstOrDefault(x =>
                x.coordInfo.coord[0] == _potCoords.coord[0] && x.coordInfo.coord[1] == _potCoords.coord[1]);
        }
        
        public Pot FindPotByCoords(int _x, int _z)
        {
            return potsList.FirstOrDefault(x =>
                x.coordInfo.coord[0] == _x && x.coordInfo.coord[1] == _z);
        }

        public void SetPotState(Pot _pot,PotState _potStateToSet)
        {
            _pot.potState = _potStateToSet;
        }

        public void SavePots()
        {
            List<SavablePot> savablePots = new List<SavablePot>();
            foreach (Pot pot in potsList)
            {
                SavablePot savablePot = new SavablePot()
                {
                    id = pot.potId,
                    potStatus = (int)pot.potState
                    
                };
                savablePots.Add(savablePot);
            }

            Settings.Instance.savableBoard.savablePots = savablePots;
        }

        
    }
    
    
}
