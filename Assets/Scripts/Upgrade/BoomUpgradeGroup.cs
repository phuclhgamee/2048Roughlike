using Roughlike2048.Event;
using UnityEngine;

namespace Roughlike2048
{
    [CreateAssetMenu(menuName = "Data/Upgradegroup/BoomUpgradeGroup", fileName = "BoomUpgradeGroup")]
    public class BoomUpgradeGroup : UpgradeGroup
    {
        [SerializeField] private BoomUpgradeVariable boomUpgradeVariable;
        [SerializeField] private IntegerVariable CurrentBoooms;
        [SerializeField] private Event.Event EnableBoomButtonEvent;
        public override void ListenEvent()
        {
            base.ListenEvent();
            BoomUpgrade boomUpgrade = (BoomUpgrade)upgrade;
            boomUpgradeVariable.Value = boomUpgrade.Data;
            CurrentBoooms.Value += boomUpgradeVariable.Value.NumberOfProvidedBooms;
            EnableBoomButtonEvent.Raise();
        }

        public override void ReplayReset()
        {
            base.ReplayReset();
            boomUpgradeVariable.Reset();
        }
    }
}