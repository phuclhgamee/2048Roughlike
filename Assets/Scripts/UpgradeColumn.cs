using Roughlike2048.Event;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Roughlike2048
{
    public class UpgradeColumn : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI upgradeName;
        [SerializeField] private TextMeshProUGUI upgradeDescription;
        [SerializeField] private Button button;
        [SerializeField] private TextMeshProUGUI levelText;
        
        public void Setup(UpgradeStatus upgradeStatus)
        {
            upgradeName.text = upgradeStatus.group.name;
            upgradeDescription.text = upgradeStatus.upgrade?.Description;
            levelText.text = upgradeStatus.level.ToString();
            button.onClick.AddListener(()=>ButtonOnClick(upgradeStatus));
        }

        public void ButtonOnClick(UpgradeStatus upgradeStatus)
        {
            //upgradeStatus.level = Mathf.Clamp(upgradeStatus.level+1, 0, upgradeStatus.group.Upgrades.Length);
            upgradeStatus.upgrade.RaisedEvent.Raise();
            Setup(upgradeStatus);
        }

    }
}