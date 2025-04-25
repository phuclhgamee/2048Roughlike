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
        
        public void Setup(UpgradeGroup group)
        {
            upgradeName.text = group.name;
            upgradeDescription.text = group.GetNextLevelDescription();
            levelText.text = group.LevelStatus.ToString();
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(()=>ButtonOnClick(group));
        }
        
        public void ButtonOnClick(UpgradeGroup group)
        {
            group.ListenEvent();
            Setup(group);
        }

    }
}