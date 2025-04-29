using System;
using UnityEngine;

namespace Roughlike2048
{
    [CreateAssetMenu(menuName = "Data/Upgrade/BiggerUpgrade", fileName = "BiggerUpgrade")]
    public class BiggerUpgrade : Upgrade
    {
        public BiggerUpgradeData Data;
    }
    [Serializable]
    public class BiggerUpgradeData
    {
        public float PercentageValue;
    }
}