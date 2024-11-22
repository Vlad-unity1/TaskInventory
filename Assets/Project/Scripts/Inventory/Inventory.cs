using ItemScriptable;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.VersionControl;

namespace InventorySystem
{
    public class Inventory
    {
        public event Action OnWeightChanged;
        public event Action<string> OnItemAdded;
        public event Action<string> OnItemRemoved;
        public event Action<string> OnItemUsed;

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

        public void UseItem(ItemData item)
        {
            if (_items.Contains(item))
            {
                item.UseItemEffect();
                OnItemUsed?.Invoke(Message.ITEM_USED);
                OnWeightChanged?.Invoke();
                RemoveItem(item);
            }
            else
            {
                OnItemUsed?.Invoke(Message.ITEM_USE_FAILED);
            }
        }
    }
}