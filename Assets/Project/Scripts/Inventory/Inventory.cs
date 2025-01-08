using BookItem;
using ItemInspector;
using ItemScriptable;
using Model;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    public class Inventory
    {
        public event Action OnWeightChanged;
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

        public int TryAddItem(ItemData item, int amount)
        {
            for (int i = 0; i < Slots.Count; i++)
            {
                if (!Slots[i].IsEmpty && Slots[i].ItemData == item && item.IsStackable)
                {
                    int currentAmount = Slots[i].Amount;
                    int newAmount = Mathf.Min(currentAmount, item.MaxStack);

                    if (currentAmount < item.MaxStack)
                    {
                        Slots[i].SetItem(item, newAmount);
                        UpdateWeight(item.Weight, true);
                        return i;
                    }
                }

                if (Slots[i].IsEmpty)
                {
                    Slots[i].SetItem(item, amount);
                    UpdateWeight(item.Weight, true);
                    return i;
                }
            }

            return -1;
        }

        public int RemoveItem(ItemData item, int amount, int slotIndex)
        {
            var slot = Slots[slotIndex];
            var currentItem = slot.ItemData;

            if (!slot.IsEmpty && currentItem == item)
            {
                slot.DecreaseAmount(amount);

                UpdateWeight(item.Weight, false);
                return slotIndex;
            }

            return -1;
        }

        public int UseItem(ItemData item, Player player, int slotIndex)
        {
            if (slotIndex >= 0 && slotIndex < Slots.Count)
            {
                var slot = Slots[slotIndex];
                var currentItem = slot.ItemData;

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
                        UpdateWeight(item.Weight, false);
                        return slotIndex;
                    }
                }
            }

            return -1;
        }

        private void UpdateWeight(float weight, bool add)
        {
            if (add)
            {
                CurrentWeight += weight;
            }
            else
            {
                CurrentWeight -= weight;
            }

            OnWeightChanged?.Invoke();
        }
    }
}