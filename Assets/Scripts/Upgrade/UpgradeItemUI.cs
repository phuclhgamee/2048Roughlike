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
        }
        
        public void ButtonOnClick(UpgradeGroup group)
        {
            group.ListenEvent();
            CloseUpgradeUIEvent.Raise();
            OnChoosingUpgradeEvent.Raise();
            
        }

        
    }
}