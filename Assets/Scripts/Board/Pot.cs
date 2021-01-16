using System;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Board
{
    
    
    public class Pot : MonoBehaviour,IMouseDetect
    {
        public int potId;
        public CoordInfo coordInfo;
        private readonly VerifyPot verifyPot = new VerifyPot();
        public PotState potState;
        private void Awake()
        {
            coordInfo = new CoordInfo();
        }

        public void OnMouseEnter()
        {
            if (IsPotValid())
            {
                RaycastInfo.detectedGameElement = this;
                RaycastInfo.OnPotDetect();
            }
                
        }

        public void OnMouseExit()
        {
            if (IsPotValid())
            {
                RaycastInfo.detectedGameElement = null;
                if(BallsManager.Instance.ballToMakeMove!= null)
                    RaycastInfo.OnPotDetect();
            }
            
        }

        private bool IsPotValid()
        {
            return potState == PotState.Free && BallsManager.Instance.ballToMakeMove && verifyPot.CanSelectPot(coordInfo);
        }
        
        public void LoadPot(SavablePot _savablePot)
        {
            potState = (PotState) _savablePot.potStatus;
        }
    
    }
}
