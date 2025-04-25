using Roughlike2048.Event;
using UnityEngine;

namespace Roughlike2048
{
    [CreateAssetMenu(menuName = "Upgrade Manager", fileName = "Upgrade Manager")]
    public class UpgradeManager : ScriptableObject
    {
        [SerializeField] public UpgradeGroup[] UpgradeGroups;

        public void RaiseEventForGroup(UpgradeGroup group)
        {
            
        }
    }
}