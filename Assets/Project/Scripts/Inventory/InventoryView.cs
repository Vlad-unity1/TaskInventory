using Armor;
using Book;
using Cell;
using InventorySystem;
using ItemInspector;
using Model;
using Potion;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Weapon;

namespace ViewInventory
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _errorMessageText;
        [SerializeField] private TextMeshProUGUI[] _currentStack;
        [SerializeField] private TextMeshProUGUI _potionMessage;
        [SerializeField] private TextMeshProUGUI _bookStatusText;
        [SerializeField] private Sprite _bookImage;
        [SerializeField] private Sprite _bookReadImage;
        [SerializeField] private Image[] _slotImages;
        [SerializeField] private List<CellofInventory> _cells;
        [SerializeField] private HolderInScene _itemData;

        public int SlotIndex { get; private set; }
        private Coroutine _hideErrorMessageCoroutine;

        private Inventory _inventory;
        private Player _player;
        private List<ItemHolder> _slots;

        public void Initialize(Inventory inventory, Player player)
        {
            _inventory = inventory;
            _player = player;
            _slots = _inventory.Slots;

            for (int i = 0; i < _slots.Count; i++)
            {
                _cells[i].SetSlot(_slots[i]);
            }
        }

        public void TryToUse(int slotIndex)
        {
            var item = _slots[slotIndex].GetItem();
            SlotIndex = slotIndex;
            _inventory.UseItem(item, _player, SlotIndex);
            SyncInventoryUI();

            if (item is ArmorEffect || item is WeaponEffect)
            {
                SetSlotImageTexture(slotIndex, item.Image);
            }

            if (item is PotionEffect potionEffect)
            {
                string potionMessage = $"Зелье восстановило {potionEffect.HealthAmount} здоровья!";
                _potionMessage.text = potionMessage;
            }

            if (item is BookEffect book)
            {
                UpdateBookSprite(slotIndex, book);
            }

            ShowErrorMessage("Предмет успешно использован!");
        }

        public void TryAddItem()
        {
            var item = _itemData.GetItem();
            int addedSlotIndex = _inventory.TryAddItem(item, 1);

            if (addedSlotIndex != -1)
            {
                _currentStack[addedSlotIndex].text = _slots[addedSlotIndex].GetAmount().ToString();
                SetSlotImageTexture(addedSlotIndex, item.Image);
                ShowErrorMessage("Предмет успешно добавлен!");
            }
        }

        public void TryRemoveItem(int slotIndex)
        {
            var item = _slots[slotIndex].GetItem();
            SlotIndex = slotIndex;
            _inventory.RemoveItem(item, 1, slotIndex);
            SyncInventoryUI();
            ShowErrorMessage("Предмет успешно удален!");
        }

        private void SyncInventoryUI()
        {
            var slot = _slots[SlotIndex];
            var amount = slot.GetAmount();

            SetStackText(SlotIndex, amount.ToString());

            if (amount <= 0)
            {
                SetSlotImageTexture(SlotIndex, null);
            }
        }

        private void SetStackText(int slotIndex, string text)
        {
            _currentStack[slotIndex].text = text;
        }

        public void UpdateBookSprite(int slotIndex, BookEffect book)
        {
            if (_player.ReadBooks.Contains(book))
            {
                _slotImages[slotIndex].sprite = _bookReadImage;
            }
            else
            {
                _slotImages[slotIndex].sprite = _bookImage;
            }
        }

        private void SetSlotImageTexture(int slotIndex, Sprite texture)
        {
            if (_slotImages[slotIndex] != null)
            {
                _slotImages[slotIndex].sprite = texture;
            }
        }

        private IEnumerator HideErrorMessageAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            _errorMessageText.text = "";
        }

        private void ShowErrorMessage(string message)
        {
            if (_hideErrorMessageCoroutine != null)
            {
                StopCoroutine(_hideErrorMessageCoroutine);
            }

            _errorMessageText.text = message;
            _hideErrorMessageCoroutine = StartCoroutine(HideErrorMessageAfterDelay(2f));
        }
    }
}