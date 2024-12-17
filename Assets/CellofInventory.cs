using ItemInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Cell
{
    public class CellofInventory : MonoBehaviour
    {
        public Image Icon;
        public TextMeshProUGUI CountText;
        private ItemHolder _slot;

        public void SetSlot(ItemHolder slot)
        {
            _slot = slot;
            UpdateSlotUI();
        }

        public void UpdateSlotUI()
        {
            if (_slot.GetItem() != null)
            {
                Icon.sprite = _slot.GetItem().Image;
                CountText.text = _slot.GetStackCount().ToString();
            }
            else
            {
                Icon.sprite = null;
                CountText.text = "0";
            }
        }
    }
}