using Roughlike2048.Event;
using UnityEngine;

namespace Roughlike2048
{
    [CreateAssetMenu(menuName = "Data/Upgradegroup/SuperEightGroup", fileName = "SuperEightGroup")]
    public class SuperEightUpgradeGroup : UpgradeGroup
    {
        [SerializeField] private FloatVariable superEightVariables;

        public override void ListenEvent()
        {
            base.ListenEvent();
            SuperEightUpgrade superEightUpgrade = (SuperEightUpgrade)upgrade;
            superEightVariables.Value = superEightUpgrade.Probability;
        }
    }
}