using System.Collections.Generic;
using UnityEngine;


namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "MoveDirectionCoord", menuName = "ScriptableObject/MoveDirectionCoord", order = 3)]
    public class MoveDirectionCoord : ScriptableObject
    {
        public Dictionary<int,int[]> checkMoves = new Dictionary<int, int[]>()
        {
            {0,new[]{0,-1}},
            {1,new []{0,1}},
            {2,new []{-1,0}},
            {3,new []{1,0}}
        };
    }
}
