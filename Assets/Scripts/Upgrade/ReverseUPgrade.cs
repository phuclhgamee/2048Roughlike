using System;
using UnityEngine;

namespace Roughlike2048
{
    [CreateAssetMenu(menuName = "Data/Upgrade/ReverseUpgrade", fileName = "ReverseUpgrade")]
    public class ReverseUpgrade : Upgrade
    {
        public ReverseUpgradeData Data;
    }

    [Serializable]
    public class ReverseUpgradeData
    {
        public int LimitedValue;
        public bool CanReverseRowAndColumn;
        public int Multiple;
    }
}