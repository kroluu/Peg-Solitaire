using System;
using UnityEngine;

namespace Board
{
    [Serializable]
    public class CoordInfo
    {
        public int[] coord = new int[2];
        public void SetCoord(CoordInfo _coord)
        {
            coord[0] = _coord.coord[0];
            coord[1] = _coord.coord[1];
        }
        
        public void SetCoord(int _x, int _z)
        {
            coord[0] = _x;
            coord[1] = _z;
        }
    }
}
