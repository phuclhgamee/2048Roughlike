using System;
using System.Collections;
using System.Collections.Generic;
using Roughlike2048.Event;
using UnityEngine;

namespace Roughlike2048
{
    public class UpgradeSelectUI : MonoBehaviour
    {
        [SerializeField] private UpgradeManager upgradeManager;
        [SerializeField] private UpgradeItemUI upgradeItemUIPrefab;
        
        private void Awake()
        {
            upgradeManager.Initialize();
        }

        private void OnEnable()
        {
            SetupUpgrade();
        }
        private void SetupUpgrade()
        {
            ClearAllChildren();
            var randomAvailableUpgrades = upgradeManager.GetRandomUpgradeGroups(Const.NumberOfRandomSkill);
            foreach (UpgradeGroup upgrade in randomAvailableUpgrades)
            {
                GameObject column = Instantiate(upgradeItemUIPrefab.gameObject, transform);
                UpgradeItemUI itemComponent= column.GetComponent<UpgradeItemUI>();
                if (itemComponent)
                {
                    itemComponent.Setup(upgrade);
                }
            }
        }

        private void ClearAllChildren()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}