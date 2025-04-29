using System;
using UnityEngine;

namespace Roughlike2048
{
    [CreateAssetMenu(menuName = "Data/Upgrade/PowerMilestone", fileName = "PowerMilestone")]
    public class PowerMilestoneUpgrade : Upgrade
    {
        public PowerMilestoneUpgradeData Data;
    }

    [Serializable]
    public class PowerMilestoneUpgradeData
    {
        public bool IsActivated;
    }
}