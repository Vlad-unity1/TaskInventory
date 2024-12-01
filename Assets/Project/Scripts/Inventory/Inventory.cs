using Armor;
using ItemScriptable;
using MessageInfo;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Weapon;

namespace InventorySystem
{
    public class Inventory
    {
        public event Action OnWeightChanged;
        public event Action<string> OnItemAdded;
        public event Action<string> OnItemRemoved;
        public event Action<string> OnItemUsed;
        public event Action OnReturnItem;

        public float MaxWeight { get; private set; }
        public float CurrentWeight { get; private set; }
        private readonly List<ItemData> _items = new();

        public Inventory(float maxWeight)
        {
            MaxWeight = maxWeight;
        }

        public bool TryAddItem(ItemData item)
        {
            if (CurrentWeight + item.Weight > MaxWeight)
            {
                OnItemAdded?.Invoke(Message.ITEM_ADD_FAILED);
                return false;
            }

            if (_items.Contains(item))
            {
                if (item.IsStackable)
                {
                    int currentStack = _items.Count(i => i == item);
                    if (currentStack < item.MaxStack)
                    {
                        _items.Add(item);
                        CurrentWeight += item.Weight;
                        OnWeightChanged?.Invoke();
                        OnItemAdded?.Invoke(Message.ITEM_ADDED);
                    }
                }
            }
            else
            {
                _items.Add(item);
                CurrentWeight += item.Weight;
                OnWeightChanged?.Invoke();
                OnItemAdded?.Invoke(Message.ITEM_ADDED);
            }

            return true;
        }

        public bool RemoveItem(ItemData item)
        {
            if (_items.Remove(item))
            {
                CurrentWeight -= item.Weight;
                OnWeightChanged?.Invoke();
                OnItemRemoved?.Invoke(Message.ITEM_REMOVED);
                return true;
            }

            throw new Exception("попытка удалить предмет которого нет в инвентаре");
        }

        public void UseItem(ItemData item, Player player)
        {
            if (_items.Contains(item))
            {
                item.UseItemEffect(player);
                OnItemUsed?.Invoke(Message.ITEM_USED);
                OnWeightChanged?.Invoke();
                RemoveItem(item);
            }
            else
            {
                OnItemUsed?.Invoke(Message.ITEM_USE_FAILED);
            }
        }

        public void ReturnItem(ItemData item)
        {
            if (item == null) return;

            if (item is WeaponEffect weapon)
            {
                if (!_items.Contains(weapon))
                {
                    _items.Add(weapon);
                    CurrentWeight += weapon.Weight;
                    OnReturnItem?.Invoke();
                }
            }
            else if (item is ArmorEffect armor)
            {
                if (!_items.Contains(armor))
                {
                    _items.Add(armor);
                    CurrentWeight += armor.Weight;
                    OnReturnItem?.Invoke();
                }
            }
        }

        public int CurrentStack(ItemData item)
        {
            int current = _items.Count(i => i == item);
            return current;
        }

        public bool ContainsItem(ItemData item)
        {
            return _items.Contains(item); 
        }
    }
}