using System;
using Roughlike2048.Event;
using UnityEngine;

namespace Roughlike2048
{
    public class UpgradeGroup : ScriptableObject
    {
        public string name;
        public Upgrade[] Upgrades;
        public UpgradeType Type;
        public IntegerVariable LevelStatus;
        
        public Upgrade upgrade
        {
            get
            {
                if (LevelStatus.Value == 0) return null;
                return Upgrades[LevelStatus.Value-1];
            }
        }

        public string GetNextLevelDescription()
        {
            try
            {
                return Upgrades[LevelStatus.Value].Description;
            }
            catch (Exception e)
            {
                return "";
            }
            
        }

        public virtual void ListenEvent()
        {
            LevelStatus.Value = Mathf.Clamp(LevelStatus.Value + 1, 0, Upgrades.Length);
        }
    }
    public enum UpgradeType
    {
        Normal,
        Rare
    }
}