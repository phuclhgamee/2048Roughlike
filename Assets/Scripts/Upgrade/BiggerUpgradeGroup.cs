using Roughlike2048.Event;
using UnityEngine;

namespace Roughlike2048
{
    [CreateAssetMenu(menuName = "Data/Upgradegroup/BiggerUpgradeGroup", fileName = "BiggerUpgradeGroup")]
    public class BiggerUpgradeGroup : UpgradeGroup
    {
        [SerializeField] private BiggerVariable biggerVariable;

        public override void ListenEvent()
        {
            base.ListenEvent();
            BiggerUpgrade biggerUpgrade = upgrade as BiggerUpgrade;
            biggerVariable.Value = biggerUpgrade.Data;
        }

        public override void ReplayReset()
        {
            base.ReplayReset();
            biggerVariable.Reset();
        }
    }
}