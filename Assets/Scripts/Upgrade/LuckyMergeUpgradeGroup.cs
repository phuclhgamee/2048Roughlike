using Roughlike2048.Event;
using UnityEngine;

namespace Roughlike2048
{
    [CreateAssetMenu(menuName = "Data/Upgradegroup/LuckyMergeUpgradeGroup", fileName = "LuckyMergeUpgradeGroup")]
    public class LuckyMergeUpgradeGroup : UpgradeGroup
    {
        [SerializeField] private FloatVariable luckyMergeVariable;

        public override void ListenEvent()
        {
            base.ListenEvent();
            FourLuckyMergeUpgrade luckyMergeUpgrade = (FourLuckyMergeUpgrade)upgrade;
            luckyMergeVariable.Value = luckyMergeUpgrade.Probability;
        }

        public override void ReplayReset()
        {
            base.ReplayReset();
            luckyMergeVariable.Reset();
        }
    }
}