using ArmorItem;
using BookItem;
using ItemInspector;
using ItemScriptable;
using JetBrains.Annotations;
using Model;
using System;
using System.Collections.Generic;
using UnityEngine;
using WeaponItem;

namespace InventorySystem
{
    public class Inventory
    {
        public event Action OnWeightChanged;
        public float MaxWeight { get; private set; }
        public float CurrentWeight { get; private set; }
        public List<ItemHolder> Slots { get; private set; } = new();

        public Inventory(int maxSlots, float maxWeight)
        {
            MaxWeight = maxWeight;
            Slots = new List<ItemHolder>(maxSlots);

            for (int i = 0; i < maxSlots; i++)
            {
                Slots.Add(new ItemHolder());
            }
        }

        public int TryAddItem(ItemData item, int amount)
        {
            for (int i = 0; i < Slots.Count; i++)
            {
                if (!Slots[i].IsEmpty && Slots[i].GetItem() == item && item.IsStackable)
                {
                    int currentAmount = Slots[i].GetAmount();
                    int newAmount = Mathf.Min(currentAmount, item.MaxStack);

                    if (currentAmount < item.MaxStack)
                    {
                        Slots[i].SetItem(item, newAmount);
                        UpdateWeight(item.Weight, true);
                        return i;
                    }
                }
            }

            for (int i = 0; i < Slots.Count; i++)
            {
                if (Slots[i].IsEmpty)
                {
                    Slots[i].SetItem(item, amount);
                    UpdateWeight(item.Weight, true);
                    return i;
                }
            }

            return -1;
        }

        public int RemoveItem(ItemData item, int amount, int slotindex)
        {
            var slot = Slots[slotindex];
            var currentItem = slot.GetItem();

            if (!slot.IsEmpty && currentItem == item)
            {
                slot.DecreaseAmount(amount);

                UpdateWeight(item.Weight, false);
                return slotindex;
            }

            return -1;
            throw new Exception("Попытка удалить предмет, которого нет в инвентаре");
        }

        public int UseItem(ItemData item, Player player, int slotindex)
        {
            if (slotindex >= 0 && slotindex < Slots.Count)
            {
                var slot = Slots[slotindex];
                var currentItem = slot.GetItem();

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
                        return slotindex;
                    }
                }
            }

            return -1;
        }

        public void ReturnItem(ItemData item)
        {
            for (int i = 0; i < Slots.Count; i++)
            {
                if (Slots[i].IsEmpty)
                {
                    Slots[i].SetItem(item, 1);
                    UpdateWeight(item.Weight, true);
                    return;
                }
            }

            throw new Exception("Инвентарь переполнен, невозможно вернуть предмет.");
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