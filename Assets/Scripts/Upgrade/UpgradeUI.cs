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
        [SerializeField] private Transform horizontalLayout;
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
                GameObject column = Instantiate(upgradeItemUIPrefab.gameObject, horizontalLayout);
                UpgradeItemUI itemComponent= column.GetComponent<UpgradeItemUI>();
                if (itemComponent)
                {
                    itemComponent.Setup(upgrade);
                }
            }
        }

        private void ClearAllChildren()
        {
            foreach (Transform child in horizontalLayout)
            {
                Destroy(child.gameObject);
            }
        }
    }
}