using Roughlike2048.Event;
using UnityEngine;

namespace Roughlike2048
{
    [CreateAssetMenu(menuName = "Upgrade Manager", fileName = "Upgrade Manager")]
    public class UpgradeManager : ScriptableObject
    {
        [SerializeField] private UpgradeStatusVariable upgradeStatusVariable;

        public UpgradeStatus[] Get3RandomUpgrades()
        {
            return new[] { new UpgradeStatus(), new UpgradeStatus() ,new UpgradeStatus() };
        }
        
        
    }
}