using Roughlike2048.Event;
using UnityEngine;

namespace Roughlike2048
{
    [CreateAssetMenu(menuName = "Data/Upgradegroup/Merge2048UpgradeGroup", fileName = "Merge2048UpgradeGroup")]
    public class Merge2048UpgradeGroup : UpgradeGroup
    {
        [SerializeField] private Merge2048Variable _merge2048Variable;

        public override void ListenEvent()
        {
            base.ListenEvent();
            Merge2048Upgrade merge2048Upgrade = upgrade as Merge2048Upgrade;
            _merge2048Variable.Value = merge2048Upgrade.Data;
        }

        public override void ReplayReset()
        {
            base.ReplayReset();
            _merge2048Variable.Reset();
        }
    }
}