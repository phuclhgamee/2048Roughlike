using Roughlike2048.Event;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Roughlike2048
{
    public class UpgradeDebugPanel : MonoBehaviour
    {
        [SerializeField] private UpgradeManager _upgradeManager;
        [SerializeField] private UpgradeStatusVariable upgradeStatusVariable;
        [SerializeField] private UpgradeColumn upgradeColumnPrefab;
        

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            foreach (UpgradeStatus upgrade in upgradeStatusVariable.Value)
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