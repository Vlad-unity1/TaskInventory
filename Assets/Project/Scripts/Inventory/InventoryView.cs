using Cell;
using InventorySystem;
using ItemArmor;
using ItemInspector;
using ItemsFactory;
using Model;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ViewInventory
{
    public class InventoryView : MonoBehaviour
    {
        public int SlotIndex { get; private set; }
        [SerializeField] private TextMeshProUGUI _errorMessageText;
        [SerializeField] private TextMeshProUGUI[] _currentStack;
        [SerializeField] private TextMeshProUGUI _potionMessage;
        [SerializeField] private TextMeshProUGUI _bookStatusText;
        [SerializeField] private Sprite _bookImage;
        [SerializeField] private Sprite _bookReadImage;
        [SerializeField] private Image[] _slotImages;
        [SerializeField] private List<InventoryCell> _cells;

        private ItemFactory _itemFactory;
        private Coroutine _hideErrorMessageCoroutine;
        private Inventory _inventory;
        private Player _player;
        private List<Slot> _slots;

        public void Initialize(Inventory inventory, Player player, ItemFactory itemFactory)
        {
            _inventory = inventory;
            _player = player;
            _itemFactory = itemFactory;
            _slots = _inventory.Slots;

            for (int i = 0; i < _slots.Count; i++)
            {
                _cells[i].SetSlot(_slots[i]);
            }

            _inventory.OnInventoryChanged += SyncInventoryUI;
        }

        public void TryToUse(int slotIndex)
        {
            var item = _slots[slotIndex].Item;
            SlotIndex = slotIndex;

            _inventory.UseItem(item, _player, SlotIndex);
            SyncInventoryUI(slotIndex);

            if (item is Armor || item is Weapon)
            {
                SetSlotImageTexture(slotIndex, item.Config.Image);
            }

            if (item is Potion potionEffect)
            {
                string potionMessage = $"Зелье восстановило {potionEffect.HealthAmount} здоровья!";
                _potionMessage.text = potionMessage;
            }

            if (item is Book book)
            {
                UpdateBookSprite(slotIndex, book);
            }

            ShowErrorMessage("Предмет успешно использован!");
        }

        public void TryAddRandomItem()
        {
            var randomItem = _itemFactory.CreateRandomItem();
            int addedSlotIndex = _inventory.TryAddItem(randomItem, 1);

            if (addedSlotIndex != -1)
            {
                _currentStack[addedSlotIndex].text = _slots[addedSlotIndex].Amount.ToString();
                SetSlotImageTexture(addedSlotIndex, randomItem.Config.Image);
                ShowErrorMessage("Предмет успешно добавлен!");
            }
            else
            {
                ShowErrorMessage("Не удалось добавить предмет: инвентарь заполнен!");
            }
        }

        public void TryRemoveItem(int slotIndex)
        {
            var item = _slots[slotIndex].Item;
            SlotIndex = slotIndex;

            _inventory.RemoveItem(item, 1, slotIndex);
            SyncInventoryUI(slotIndex);
            ShowErrorMessage("Предмет успешно удален!");
        }

        private void SyncInventoryUI(int slotIndex)
        {
            var slot = _slots[slotIndex];

            if (slot.IsEmpty)
            {
                SetSlotImageTexture(slotIndex, null);
                SetStackText(slotIndex, "0");
            }
            else
            {
                var itemImage = slot.Item.Config.Image;
                var itemAmount = slot.Amount.ToString();

                SetSlotImageTexture(slotIndex, itemImage);
                SetStackText(slotIndex, itemAmount);
            }
        }

        private void SetStackText(int slotIndex, string text)
        {
            _currentStack[slotIndex].text = text;
        }

        private void UpdateBookSprite(int slotIndex, Book book)
        {
            if (_player.ReadBookIDs.Contains(book.Id))
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