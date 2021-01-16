using System;
using UnityEngine;

namespace Board
{
    public class PotCoordAssign : MonoBehaviour
    {
        public void Awake()
        {
            int ballCounter = 0;
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
                    
                    CoordInfo coordInfo = transform.GetChild(ballCounter).GetComponent<CoordInfo>();
                    ballCounter++;
                    if (coordInfo == null)
                    {
                        continue;
                    }
                    coordInfo.SetCoord(i,j);
                }
            }
        }
        
    }
}
