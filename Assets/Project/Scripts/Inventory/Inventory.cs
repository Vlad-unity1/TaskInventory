using ItemStats;
using Model;
using System.Collections.Generic;

namespace InventorySystem
{
    public class Inventory
    {
        public float MaxWeight { get; private set; }
        public float CurrentWeight { get; private set; }
        private readonly List<Item> _items = new();

        public Inventory(float maxWeight)
        {
            MaxWeight = maxWeight;
        }

        public bool AddItem(Item item)
        {
            if (CurrentWeight + item.Weight > MaxWeight)
                return false;

            _items.Add(item);
            CurrentWeight += item.Weight;
            return true;
        }

        public bool RemoveItem(Item item)
        {
            if (_items.Remove(item))
            {
                CurrentWeight -= item.Weight;
                return true;
            }
            return false;
        }

        public void UseItem(Item item, PlayerModel player)
        {
            if (_items.Contains(item))
            {
                item.Effect.Apply(player);
                RemoveItem(item);
            }
        }
    }
}