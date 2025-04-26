using System.Collections.Generic;
using System.Linq;
using Roughlike2048.Event;
using UnityEngine;

namespace Roughlike2048
{
    [CreateAssetMenu(menuName = "Upgrade Manager", fileName = "Upgrade Manager")]
    public class UpgradeManager : ScriptableObject
    {
        [SerializeField] public UpgradeGroup[] UpgradeGroups;
        
        [HideInInspector] public List<UpgradeGroup> currentUpgradeGroups;
        [HideInInspector] public List<UpgradeGroup> availableUpgradeGroups;

        public void Initialize()
        {
            currentUpgradeGroups = new List<UpgradeGroup>();
            availableUpgradeGroups = new List<UpgradeGroup>(UpgradeGroups);
        }
        
        public void RaiseEventForGroup(UpgradeGroup group)
        {
            
        }

        public List<UpgradeGroup> GetRandomUpgradeGroups(int numberOfGroups)
        {
            return availableUpgradeGroups
                .OrderBy(_ => Random.value)
                .Take(numberOfGroups)
                .ToList();
        }

        public void SelectSkill(UpgradeGroup group)
        {
            
        }
    }
}