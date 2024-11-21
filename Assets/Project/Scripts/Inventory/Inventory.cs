using ItemScriptable;
using System;
using System.Collections.Generic;

namespace InventorySystem
{
    public class Inventory
    {
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

            _items.Add(item);
            CurrentWeight += item.Weight;
            return true;
        }

        public bool RemoveItem(ItemData item)
        {
            if (_items.Remove(item))
            {
                CurrentWeight -= item.Weight;
                return true;
            }
            throw new Exception("попытка удалить предмет которого нет в инвентаре");
        }

        public void UseItem(ItemData item)
        {
            if (_items.Contains(item))
            {
                item.UseItemEffect();
                RemoveItem(item);
            }
        }
    }
}