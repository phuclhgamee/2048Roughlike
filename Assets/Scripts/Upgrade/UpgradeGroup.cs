using UnityEngine;

namespace Roughlike2048
{
    [CreateAssetMenu(menuName = "Data/Upgradegroup", fileName = "Upgradegroup")]
    public class UpgradeGroup : ScriptableObject
    {
        public Upgrade[] Upgrades;
    }
}