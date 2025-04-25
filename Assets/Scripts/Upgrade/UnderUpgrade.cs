using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Roughlike2048
{
    [CreateAssetMenu(fileName = "New Game Over", menuName = "Data/Upgrade/Under")]
    public class UnderUpgrade : Upgrade
    {
        public UnderData Data;
    }

    [Serializable]
    public class UnderData
    {
        public int GoBackTurns;
        public int CoolDownTurns;
        public bool IsActivated;
    }
}

