using Roughlike2048.Event;
using UnityEngine;

namespace Roughlike2048
{
    [CreateAssetMenu(menuName = "Data/Upgradegroup/UnderUpgradeGroup", fileName = "UnderUpgradeGroup")]
    public class UnderUpgradeGroup : UpgradeGroup
    {
        [SerializeField] private UnderVariable _underVariable;

        public override void ListenEvent()
        {
            base.ListenEvent();
            UnderUpgrade underUpgrade = (UnderUpgrade)upgrade;
            _underVariable.Value = underUpgrade.Data;
        }

        public override void ReplayReset()
        {
            base.ReplayReset();
            _underVariable.Reset();
        }
    }
}