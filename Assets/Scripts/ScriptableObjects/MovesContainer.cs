using System;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "MovesContainer", menuName = "ScriptableObject/MovesContainer", order = 2)]
    public class MovesContainer:ScriptableObject
    {
        public List<MoveInfo> movesMade = new List<MoveInfo>();
    }
    [Serializable]
    public class MoveInfo
    {
        public int[] from = new int[2];
        public int[] between = new int[2];
        public int[] to = new int[2];
    }
}
