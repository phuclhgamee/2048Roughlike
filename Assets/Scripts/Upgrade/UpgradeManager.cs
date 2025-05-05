using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Roughlike2048.Event;
using UnityEngine;

namespace Roughlike2048
{
    [CreateAssetMenu(menuName = "Upgrade Manager", fileName = "Upgrade Manager")]
    public class UpgradeManager : ScriptableObject
    {
        [SerializeField] public UpgradeGroup[] UpgradeGroups;
        
        public List<UpgradeGroup> currentUpgradeGroups;
        public List<UpgradeGroup> availableUpgradeGroups;

        public void Initialize()
        {
            currentUpgradeGroups = new List<UpgradeGroup>();
            availableUpgradeGroups = new List<UpgradeGroup>(UpgradeGroups);
        }
        

        public List<UpgradeGroup> GetRandomUpgradeGroups(int numberOfGroups)
        {
            return availableUpgradeGroups
                .OrderBy(_ => Random.value)
                .Take(numberOfGroups)
                .ToList();
        }

        public void UpgradeAvailableGroup()
        {
            foreach (UpgradeGroup group in UpgradeGroups)
            {
                if (group.IsGettingMaxLevel)
                {
                    availableUpgradeGroups.Remove(group);
                }
            }
        }
        public void SelectUpgrade(UpgradeGroup group)
        {
            
        }

        public void ReplayUpgradeReset()
        {
            Initialize();
            foreach (UpgradeGroup group in UpgradeGroups)
            {
                group.ReplayReset();
            }
        }
    }
}