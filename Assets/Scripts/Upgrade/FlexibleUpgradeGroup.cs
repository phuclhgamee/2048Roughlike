using Roughlike2048.Event;
using UnityEngine;

namespace Roughlike2048
{    
    [CreateAssetMenu(menuName = "Data/Upgradegroup/FlexibleUpgradeGroup", fileName = "FlexibleUpgradeGroup")]
    public class FlexibleUpgradeGroup : UpgradeGroup
    {
        [SerializeField] private FlexibleUpgradeVariable _flexibleUpgradeVariable;
        [SerializeField] private Event.Event EnableBoomButtonEvent;
        [SerializeField] private Event.Event EnableFlexibleTextEvent;
        [SerializeField] private IntegerVariable CurrentBooms;
        public override void ListenEvent()
        {
            base.ListenEvent();
            FlexibleUpgrade flexibleUpgrade = upgrade as FlexibleUpgrade;
            _flexibleUpgradeVariable.Value = flexibleUpgrade.Data;
            EnableBoomButtonEvent.Raise();
            EnableFlexibleTextEvent.Raise();
            CurrentBooms.Value += 0;
        }

        public override void ReplayReset()
        {
            base.ReplayReset();
            _flexibleUpgradeVariable.Reset();
        }
    }
}