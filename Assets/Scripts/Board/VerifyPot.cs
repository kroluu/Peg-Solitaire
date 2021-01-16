using System.Linq;
using UnityEngine;

namespace Board
{
    public class VerifyPot
    {
        public bool CanSelectPot(CoordInfo _potCoord)
        {
            CoordInfo ballCoord = BallsManager.Instance.ballToMakeMove.coordInfo;
            
            return CheckIfBetweenBalExist(ballCoord,_potCoord) &&
                   CheckIfPotIsInLine(ballCoord,_potCoord) && 
                   (Mathf.Abs((ballCoord.coord[0] + ballCoord.coord[1]) - (_potCoord.coord[0] + _potCoord.coord[1])) == 2);
        }

        private bool CheckIfPotIsInLine(CoordInfo _selectedBall, CoordInfo _potCoord)
        {
            
            return (_selectedBall.coord[0] - _potCoord.coord[0] == 0) || (_selectedBall.coord[1] - _potCoord.coord[1] == 0);
        }

        private bool CheckIfBetweenBalExist(CoordInfo _selectedBall, CoordInfo _potCoord)
        {
            int[] betweenCoord = new int[2];
            if (_selectedBall.coord[0] - _potCoord.coord[0] != 0)
            {
                betweenCoord[0] = _selectedBall.coord[0] + (_selectedBall.coord[0] - _potCoord.coord[0] > 0 ? -1:1);
                betweenCoord[1] = _selectedBall.coord[1];
            }
            if (_selectedBall.coord[1] - _potCoord.coord[1] != 0)
            {
                betweenCoord[0] = _selectedBall.coord[0];
                betweenCoord[1] = _selectedBall.coord[1] + (_selectedBall.coord[1] - _potCoord.coord[1] > 0 ? -1:1);
            }

            Ball betweenBall = BallsManager.Instance.ballsLeft.FirstOrDefault(x =>
                x.coordInfo.coord[0] == betweenCoord[0]
                && x.coordInfo.coord[1] == betweenCoord[1]);

            return BallsManager.Instance.ballsLeft.Contains(betweenBall);
        }

    }
}
