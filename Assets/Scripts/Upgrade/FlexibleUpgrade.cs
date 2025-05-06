using System;
using UnityEngine;

namespace Roughlike2048
{
    [CreateAssetMenu(menuName = "Data/Upgrade/FlexibleUpgrade", fileName = "FlexibleUpgrade")]
    public class FlexibleUpgrade : Upgrade
    {
        public FlexibleUpgradeData Data;
    }
    
    [Serializable]
    public class FlexibleUpgradeData
    {
        public int Steps;
        public int BoomAwards;
        public bool IsActivated;
    }
    
}