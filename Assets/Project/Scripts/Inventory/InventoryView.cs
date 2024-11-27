using Armor;
using Book;
using InventorySystem;
using ItemInspector;
using MessageInfo;
using Potion;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Weapon;

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
        [SerializeField] private TextMeshProUGUI _potionMessage;
        [SerializeField] private TextMeshProUGUI _bookStatusText;
        [SerializeField] private Sprite _bookImage;
        [SerializeField] private List<Sprite> _usedSlots = new();
        [SerializeField] private Image _lastItemImage;
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
            DeleteSlotImageTexture(_slots[slotIndex]);
            LastItemUsed(item.Image);

            if (item is ArmorEffect || item is WeaponEffect)
            {
                SetSlotImageTexture(_slots[slotIndex], item.Image);
            }

            if (item is PotionEffect)
            {
                StartCoroutine(Message.ShowMessage(_potionMessage, Message.POTION_USED));
            }

            if (item is BookEffect)
            {
                UpdateBookStatus(true);
            }
        }

        public void TryAddItem(int slotIndex)
        {
            var item = _slots[slotIndex].ItemData;
            if (_inventory.TryAddItem(item))
            {
                _currentStack[slotIndex].text = _inventory.CurrentStack(item).ToString();
                SetSlotImageTexture(_slots[slotIndex], item.Image);
            }
        }

        public void TryRemoveItem(int slotIndex)
        {
            var item = _slots[slotIndex].ItemData;
            if (_inventory.RemoveItem(item))
            {
                _currentStack[slotIndex].text = _inventory.CurrentStack(item).ToString();
                DeleteSlotImageTexture(_slots[slotIndex]);
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

        public void UpdateBookStatus(bool isRead)
        {
            if (isRead)
            {
                _bookStatusText.text = "Used";
            }
        }

        private void SetSlotImageTexture(ItemHolder slot, Sprite texture)
        {
            if (slot.TryGetComponent<Image>(out var rawImage))
            {
                rawImage.sprite = texture;
            }
        }

        private void DeleteSlotImageTexture(ItemHolder slot)
        {
            if (slot.TryGetComponent<Image>(out var rawImage))
            {
                rawImage.sprite = null;
            }
        }

        private void LastItemUsed(Sprite texture)
        {
            _usedSlots.Add(texture);
            _lastItemImage.sprite = texture;
        }

        private void ShowErrorMessage(string message)
        {
            StartCoroutine(Message.ShowMessage(_errorMessageText, message));
        }

    }
}