using System;
using UnityEngine;

namespace Roughlike2048
{
    [CreateAssetMenu(menuName = "Data/Upgrade/FirstDeathUpgrade", fileName = "FirstDeathUpgrade")]
    public class FirstDeathUpgrade : Upgrade
    {
        public FirstDeathUpgradeData Data;
    }
    [Serializable]
    public class FirstDeathUpgradeData
    {
        public bool IsActivated;

        public FirstDeathUpgradeData(bool isActivated)
        {
            IsActivated = isActivated;
        }
    }
}