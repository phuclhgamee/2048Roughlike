using System;
using UnityEngine;

namespace Roughlike2048
{
    [CreateAssetMenu(menuName = "Data/Upgrade/BoomUpgrade", fileName = "BoomUpgrade")]
    public class BoomUpgrade : Upgrade
    {
        public BoomUpgradeData Data;
    }
    [Serializable]
    public class BoomUpgradeData
    {
        public bool IsActivated;
        public int NumberOfProvidedBooms;
    }
}