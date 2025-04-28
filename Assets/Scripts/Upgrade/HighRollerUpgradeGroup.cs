using System.Linq;
using Roughlike2048.Event;
using UnityEngine;

namespace Roughlike2048
{
    [CreateAssetMenu(menuName = "Data/Upgradegroup/HighRollerUpgradeGroup", fileName = "HighRollerUpgradeGroup")]
    public class HighRollerUpgradeGroup : UpgradeGroup
    {
        [SerializeField] private TileStateVariables _tileStateVariables;
        [SerializeField] private TileState[] tileStates;
        public override void ListenEvent()
        {
            base.ListenEvent();
            HighRollerUpgrade highRollerUpgrade = (HighRollerUpgrade)upgrade;
            _tileStateVariables.Value = tileStates.Where(x=>x.number == highRollerUpgrade.startedNumber).FirstOrDefault();
        }

        public override void ReplayReset()
        {
            base.ReplayReset();
            _tileStateVariables.Reset();
        }
    }
}