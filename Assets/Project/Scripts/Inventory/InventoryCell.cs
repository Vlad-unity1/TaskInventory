using ItemInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Cell
{
    public class InventoryCell : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _countText;
        private Slot _slot;

        public void SetSlot(Slot slot)
        {
            _slot = slot;
            UpdateSlotUI();
        }

        private void UpdateSlotUI()
        {
            if (_slot.Item != null)
            {
                _icon.sprite = _slot.Item.Config.Image;
                _countText.text = _slot.StackCount.ToString();
            }
            else
            {
                _icon.sprite = null;
                _countText.text = "0";
            }
        }
    }
}