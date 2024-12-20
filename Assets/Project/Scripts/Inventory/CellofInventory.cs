using ItemInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Cell
{
    public class CellofInventory : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _countText;
        private ItemHolder _slot;

        public void SetSlot(ItemHolder slot)
        {
            _slot = slot;
            UpdateSlotUI();
        }

        private void UpdateSlotUI()
        {
            if (_slot.GetItem() != null)
            {
                _icon.sprite = _slot.GetItem().Image;
                _countText.text = _slot.GetStackCount().ToString();
            }
            else
            {
                _icon.sprite = null;
                _countText.text = "0";
            }
        }
    }
}