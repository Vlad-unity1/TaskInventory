using ItemInspector;
using Model;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    public class Inventory
    {
        public event Action OnWeightChanged;
        public event Action<int> OnInventoryChanged;

        public float MaxWeight { get; private set; }
        public float CurrentWeight { get; private set; }
        public List<Slot> Slots { get; private set; } = new();

        public Inventory(int maxSlots, float maxWeight)
        {
            MaxWeight = maxWeight;
            Slots = new List<Slot>(maxSlots);

            for (int i = 0; i < maxSlots; i++)
            {
                Slots.Add(new Slot());
            }
        }

        public int TryAddItem(Item item, int amount)
        {
            if (CurrentWeight + item.Config.Weight > MaxWeight)
            {
                return -1;
            }

            for (int i = 0; i < Slots.Count; i++)
            {
                if (!Slots[i].IsEmpty && Slots[i].Item.Config == item.Config && item.Config.IsStackable)
                {
                    int currentAmount = Slots[i].Amount;
                    int maxStack = item.Config.MaxStack;
                    int newAmount = Mathf.Min(currentAmount, maxStack);

                    if (currentAmount < maxStack)
                    {
                        Slots[i].SetItem(item, newAmount);
                        IncreaseWeight(item.Config.Weight);
                        OnInventoryChanged?.Invoke(i);
                        return i;
                    }
                }

                if (Slots[i].IsEmpty)
                {
                    Slots[i].SetItem(item, amount);
                    IncreaseWeight(item.Config.Weight);
                    OnInventoryChanged?.Invoke(i);
                    return i;
                }
            }

            return -1;
        }

        public int RemoveItem(Item item, int amount, int slotIndex)
        {
            var slot = Slots[slotIndex];
            var currentItem = slot.Item;

            if (!slot.IsEmpty && currentItem == item)
            {
                slot.DecreaseAmount(amount);
                DecreaseWeight(item.Config.Weight);
                OnInventoryChanged?.Invoke(slotIndex);
                return slotIndex;
            }

            return -1;
        }

        public int UseItem(Item item, Player player, int slotIndex)
        {
            if (slotIndex >= 0 && slotIndex < Slots.Count)
            {
                var slot = Slots[slotIndex];
                var currentItem = slot.Item;

                if (!slot.IsEmpty && currentItem == item)
                {
                    if (item is Book)
                    {
                        item.UseItemEffect(player);
                    }
                    else
                    {
                        item.UseItemEffect(player);
                        slot.DecreaseAmount(1);
                        DecreaseWeight(item.Config.Weight);
                        OnInventoryChanged?.Invoke(slotIndex);
                        return slotIndex;
                    }
                }
            }

            return -1;
        }

        private void IncreaseWeight(float weight)
        {
            CurrentWeight += weight;
            OnWeightChanged?.Invoke();
        }

        private void DecreaseWeight(float weight)
        {
            CurrentWeight -= weight;
            OnWeightChanged?.Invoke();
        }
    }
}