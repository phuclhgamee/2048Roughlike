using System;
using UnityEngine;

namespace Roughlike2048.Event
{
    [CreateAssetMenu(fileName = "UpgradeStatusVariable", menuName = "Event/Variables/UpgradeStatusVariable")]
    public class UpgradeStatusVariable : BaseVariable<UpgradeStatus[]>
    {
        
    }

    [Serializable]
    public class UpgradeStatus
    {
        public UpgradeGroup group;
        public int level;

        public Upgrade upgrade
        {
            get
            {
                if (level == 0) return null;
                return group.Upgrades[level-1];
            }
        }
    }
}