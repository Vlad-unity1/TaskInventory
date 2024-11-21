using ItemScriptable;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InventorySystem
{
    public class Inventory
    {
        public event Action OnWeightChanged;

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
                return false;

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
                    }
                }
            }
            else
            {
                _items.Add(item);
                CurrentWeight += item.Weight;
                OnWeightChanged?.Invoke();
            }

            return true;
        }

        public bool RemoveItem(ItemData item)
        {
            if (_items.Remove(item))
            {
                CurrentWeight -= item.Weight;
                OnWeightChanged?.Invoke();
                return true;
            }
            throw new Exception("попытка удалить предмет которого нет в инвентаре");
        }

        public void UseItem(ItemData item)
        {
            if (_items.Contains(item))
            {
                item.UseItemEffect();
                OnWeightChanged?.Invoke();
                RemoveItem(item);
            }
        }
    }
}