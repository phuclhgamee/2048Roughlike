using System;
using UnityEngine;

namespace Roughlike2048
{
    [CreateAssetMenu(menuName = "Data/Upgrade/HighRiskHighReward", fileName = "HighRiskHighReward")]
    public class HighRiskHighRewardUpgrade : Upgrade
    {
        public HighRiskHighRewardUpgradeData Data;
    }
    [Serializable]
    public class HighRiskHighRewardUpgradeData
    {
        public bool IsActivated;
        public int BiggsetValueToTrigger;
        public int Move;
    }
}