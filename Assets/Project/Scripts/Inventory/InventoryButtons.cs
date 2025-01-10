using System;
using UnityEngine;
using UnityEngine.UI;
using ViewInventory;

namespace Buttons
{
    public class InventoryButtons : MonoBehaviour
    {
        [SerializeField] private GameObject _inventoryPanel;
        [SerializeField] private Button[] _useButtons;
        [SerializeField] private Button _addButton;
        [SerializeField] private Button[] _removeButtons;
        [SerializeField] private Button _openInventory;
        private InventoryView _viewInventory;

        public void Initialize(InventoryView viewInventory)
        {
            _viewInventory = viewInventory;
            _inventoryPanel.SetActive(false);

            AssignButtonListeners(_useButtons, slotIndex => _viewInventory.TryToUse(slotIndex));
            AssignButtonListeners(_removeButtons, slotIndex => _viewInventory.TryRemoveItem(slotIndex));
            _addButton.onClick.AddListener(() => _viewInventory.TryAddRandomItem());
            _openInventory.onClick.AddListener(ToggleInventory);
        }

        private void ToggleInventory()
        {
            _inventoryPanel.SetActive(!_inventoryPanel.activeSelf);
        }

        private void AssignButtonListeners(Button[] buttons, Action<int> action)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                int slotIndex = i;
                buttons[i].onClick.AddListener(() => action(slotIndex));
            }
        }
    }
}