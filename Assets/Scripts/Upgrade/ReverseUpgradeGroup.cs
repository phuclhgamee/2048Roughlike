using Roughlike2048.Event;
using UnityEngine;

namespace Roughlike2048
{
    [CreateAssetMenu(menuName = "Data/Upgradegroup/ReverseUpgradeGroup", fileName = "ReverseUpgradeGroup")]
    public class ReverseUpgradeGroup : UpgradeGroup
    {
        [SerializeField] private ReverseUpgradeVariable reverseUpgradeVariable;

        public override void ListenEvent()
        {
            base.ListenEvent();
            ReverseUpgrade reverseUpgrade = upgrade as ReverseUpgrade;
            reverseUpgradeVariable.Value = reverseUpgrade.Data;
        }

        public override void ReplayReset()
        {
            base.ReplayReset();
            reverseUpgradeVariable.Reset();
        }
    }
}