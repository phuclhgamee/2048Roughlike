using System;
using UnityEngine;

namespace Roughlike2048
{
    [CreateAssetMenu(menuName = "Data/Upgrade/Merge2048", fileName = "Merge2048")]
    public class Merge2048Upgrade : Upgrade
    {
        public Merge2048UpgradeData Data;
    }

    [Serializable]
    public class Merge2048UpgradeData
    {
        public int MergeValue;
    }
}