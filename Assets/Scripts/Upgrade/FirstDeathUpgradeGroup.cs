using Roughlike2048.Event;
using UnityEngine;

namespace Roughlike2048
{
    [CreateAssetMenu(menuName = "Data/Upgradegroup/FirstDeathUpgradeGroup", fileName = "FirstDeathUpgradeGroup")]
    public class FirstDeathUpgradeGroup : UpgradeGroup
    {
        [SerializeField] private FirstDeathVariable firstDeathVariable;

        public override void ListenEvent()
        {
            base.ListenEvent();
            FirstDeathUpgrade firstDeathUpgrade = (FirstDeathUpgrade)upgrade;
            firstDeathVariable.Value = firstDeathUpgrade.Data;
        }
        public override void ReplayReset()
        {
            base.ReplayReset();
            firstDeathVariable.Reset();
        }
    }
}