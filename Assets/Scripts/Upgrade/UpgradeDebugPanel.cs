using Roughlike2048.Event;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Roughlike2048
{
    public class UpgradeDebugPanel : MonoBehaviour
    {
        [SerializeField] private UpgradeManager upgradeManager;
        [SerializeField] private UpgradeColumn upgradeColumnPrefab;
        
        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            foreach (UpgradeGroup upgrade in upgradeManager.UpgradeGroups)
            {
                GameObject column = Instantiate(upgradeColumnPrefab.gameObject, transform);
                UpgradeColumn columnComponent= column.GetComponent<UpgradeColumn>();
                if (columnComponent)
                {
                    columnComponent.Setup(upgrade);
                }
            }
        }
    }
}