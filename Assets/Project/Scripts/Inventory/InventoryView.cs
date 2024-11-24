using InventorySystem;
using ItemInspector;
using MessageInfo;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ViewInventory
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField] private Button _openInventory;
        [SerializeField] private GameObject _inventoryPanel;
        [SerializeField] private ItemHolder[] _slots;
        [SerializeField] private Button[] _useButtons;
        [SerializeField] private Button[] _addButtons;
        [SerializeField] private Button[] _removeButtons;
        [SerializeField] private TextMeshProUGUI _errorMessageText;
        [SerializeField] private TextMeshProUGUI[] _currentStack;

        private Inventory _inventory;

        public void Initialize(Inventory inventory)
        {
            _inventory = inventory;
            _inventoryPanel.SetActive(false);

            _inventory.OnItemAdded += ShowErrorMessage;
            _inventory.OnItemRemoved += ShowErrorMessage;
            _inventory.OnItemUsed += ShowErrorMessage;
            _inventory.OnReturnItem += UpdateStackCounts;
            UpdateStackCounts();
        }

        public void ToggleInventory()
        {
            _inventoryPanel.SetActive(!_inventoryPanel.activeSelf);
        }

        public void TryToUse(int slotIndex)
        {
            var item = _slots[slotIndex].ItemData;
            _inventory.UseItem(item);
            _currentStack[slotIndex].text = _inventory.CurrentStack(item).ToString();
        }

        public void TryAddItem(int slotIndex)
        {
            var item = _slots[slotIndex].ItemData;
            if (_inventory.TryAddItem(item))
            {
                _currentStack[slotIndex].text = _inventory.CurrentStack(item).ToString();
            }
        }

        public void TryRemoveItem(int slotIndex)
        {
            var item = _slots[slotIndex].ItemData;
            if (_inventory.RemoveItem(item))
            {
                _currentStack[slotIndex].text = _inventory.CurrentStack(item).ToString();
            }
        }

        private void UpdateStackCounts()
        {
            for (int i = 0; i < _slots.Length; i++)
            {
                var item = _slots[i].ItemData;
                if (item != null)
                {
                    _currentStack[i].text = _inventory.CurrentStack(item).ToString();
                }
                else
                {
                    _currentStack[i].text = "0";
                }
            }
        }

        private void ShowErrorMessage(string message)
        {
            StartCoroutine(Message.ShowMessage(_errorMessageText, message));
        }

    }
}