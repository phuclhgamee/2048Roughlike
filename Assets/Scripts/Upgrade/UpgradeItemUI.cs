using System.Collections;
using Roughlike2048.Event;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Roughlike2048
{
    public class UpgradeItemUI : MonoBehaviour
    {
        [SerializeField] private Image Icon;
        [SerializeField] private TextMeshProUGUI Description;
        [SerializeField] private TextMeshProUGUI Name;
        [SerializeField] private Button button;
        [SerializeField] private Transform starGroupPanel;
        [SerializeField] private GameObject activeStar;
        [SerializeField] private GameObject inactiveStar;
        [Space]
        [SerializeField] private Event.Event CloseUpgradeUIEvent;
        [SerializeField] private Event.Event OnChoosingUpgradeEvent;
        [SerializeField] private IntegerVariable NumberOfUpgradeSelected;
            

        public void Setup(UpgradeGroup group)
        {
            Icon.sprite = group.GetNextLevelIcon();
            Name.text = group.name;
            Description.text = group.GetNextLevelDescription();
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(()=>ButtonOnClick(group));
            ClearStarPanel();
            if (group.Upgrades.Length > 1)
            {
                for (int i = 0; i < group.Upgrades.Length; i++)
                {
                    if (i < group.LevelStatus.Value)
                    {
                        Instantiate(activeStar, starGroupPanel);
                    }
                    else
                    {
                        Instantiate(inactiveStar, starGroupPanel);
                    }
                }
            }
        }

        private void ClearStarPanel()
        {
            foreach (Transform child in starGroupPanel)
            {
                Destroy(child.gameObject);
            }
        }
        public void ButtonOnClick(UpgradeGroup group)
        {
            group.ListenEvent();
            CloseUpgradeUIEvent.Raise();
            OnChoosingUpgradeEvent.Raise();
        }

        
    }
}