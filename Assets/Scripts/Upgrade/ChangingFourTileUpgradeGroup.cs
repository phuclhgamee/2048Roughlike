using System;
using Roughlike2048.Event;
using UnityEngine;

namespace Roughlike2048
{
    [CreateAssetMenu(menuName = "Data/Upgradegroup/ChangingFourTileUpgradeGroup", fileName = "ChangingFourTileUpgradeGroup")]
    public class ChangingFourTileUpgradeGroup : UpgradeGroup
    {
        [SerializeField] private ChangingFourTileVariable _changingFourTileVariable;

        public override void ListenEvent()
        {
            base.ListenEvent();
            ChangingFourTileUpgrade changingFourTileUpgrade = (ChangingFourTileUpgrade)upgrade;
            _changingFourTileVariable.Value = changingFourTileUpgrade.Data;
        }

        public override void ReplayReset()
        {
            base.ReplayReset();
            _changingFourTileVariable.Reset();
        }
    }
}