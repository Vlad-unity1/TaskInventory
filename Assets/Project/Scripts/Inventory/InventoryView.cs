using InventorySystem;
using ItemInspector;
using System.Collections;
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

        private Inventory _inventory;

        public void Initialize(Inventory inventory)
        {
            _inventory = inventory;
            _inventoryPanel.SetActive(false);

            _inventory.OnItemAdded += ShowErrorMessage;
            _inventory.OnItemRemoved += ShowErrorMessage;
            _inventory.OnItemUsed += ShowErrorMessage;
        }

        public void ToggleInventory()
        {
            _inventoryPanel.SetActive(!_inventoryPanel.activeSelf);
        }

        public void TryToUse(int slotIndex)
        {
            _inventory.UseItem(_slots[slotIndex].ItemData);
        }

        public void TryAddItem(int slotIndex)
        {
            _inventory.TryAddItem(_slots[slotIndex].ItemData);
        }

        public void TryRemoveItem(int slotIndex)
        {
            _inventory.RemoveItem(_slots[slotIndex].ItemData);
        }

        private void ShowErrorMessage(string message)
        {
            StartCoroutine(ShowMessageCoroutine(message));
        }

        private IEnumerator ShowMessageCoroutine(string message)
        {
            _errorMessageText.text = message;
            _errorMessageText.gameObject.SetActive(true);
            yield return new WaitForSeconds(2f);
            _errorMessageText.gameObject.SetActive(false);
        }
    }
}