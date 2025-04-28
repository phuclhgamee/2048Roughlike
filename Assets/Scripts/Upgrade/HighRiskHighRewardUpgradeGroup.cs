using Roughlike2048.Event;
using UnityEngine;

namespace Roughlike2048
{
    [CreateAssetMenu(menuName = "Data/Upgradegroup/HighRiskHighRewardUpgradeGroup", fileName = "HighRiskHighRewardUpgradeGroup")]
    public class HighRiskHighRewardUpgradeGroup : UpgradeGroup
    {
        [SerializeField] private HighRiskHighRewardVariable _highRiskHighRewardVariable;

        public override void ListenEvent()
        {
            base.ListenEvent();
            HighRiskHighRewardUpgrade highRiskHighRewardUpgrade = (HighRiskHighRewardUpgrade)upgrade;
            _highRiskHighRewardVariable.Value = highRiskHighRewardUpgrade.Data;
        }

        public override void ReplayReset()
        {
            base.ReplayReset();
            _highRiskHighRewardVariable.Reset();
        }
    }
}