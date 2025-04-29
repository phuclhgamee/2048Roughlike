using Roughlike2048.Event;
using UnityEngine;

namespace Roughlike2048
{
    [CreateAssetMenu(menuName = "Data/Upgradegroup/PowerMilestoneGroup", fileName = "PowerMilestoneGroup")]
    public class PowerMilestoneUpgradeGroup : UpgradeGroup
    {
        [SerializeField] PowerMilestoneVariable variable;

        public override void ListenEvent()
        {
            base.ListenEvent();
            PowerMilestoneUpgrade powerMilestoneUpgrade = upgrade as PowerMilestoneUpgrade;
            variable.Value = powerMilestoneUpgrade.Data;
        }

        public override void ReplayReset()
        {
            base.ReplayReset();
            variable.Reset();
        }
    }
}