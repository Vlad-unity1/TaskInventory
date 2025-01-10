using InventorySystem;
using ItemArmor;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    public class Player
    {
        public event Action OnHealthChanged;
        public event Action OnExpChanged;
        public event Action OnBookRead;
        public event Action OnBookWasRead;
        public event Action OnWeaponEquipped;
        public event Action OnArmorEquipped;

        public int CurrentHP
        {
            get => _currentHp;
            private set
            {
                if (_currentHp != value)
                {
                    _currentHp = value;
                    OnHealthChanged?.Invoke();
                }
            }
        }
        public int MaxHP { get; private set; }
        public int Exp { get; private set; }
        private int _currentHp;

        public Inventory Inventory { get; private set; }
        public List<string> ReadBookIDs { get; private set; } = new List<string>();
        public Weapon EquippedWeapon { get; private set; }
        public Armor EquippedArmor { get; private set; }

        public Player(int maxHP, float maxInventoryWeight, int inventorySize)
        {
            MaxHP = maxHP;
            CurrentHP = MaxHP;
            Exp = 0;
            Inventory = new Inventory(inventorySize, maxInventoryWeight);
        }

        public void Heal(int amount)
        {
            CurrentHP = Mathf.Min(CurrentHP + amount, MaxHP);
        }

        public void TakeDamage(int damage)
        {
            CurrentHP = Mathf.Max(CurrentHP - damage, 0);
        }

        public void EquipWeapon(Weapon weapon)
        {
            if (EquippedWeapon != null)
            {
                Inventory.TryAddItem(EquippedWeapon, 1);
            }

            EquippedWeapon = weapon;
            OnWeaponEquipped?.Invoke();
        }

        public void EquipArmor(Armor armor)
        {
            if (EquippedArmor != null)
            {
                Inventory.TryAddItem(EquippedArmor, 1);
                EquippedArmor = null;
            }

            EquippedArmor = armor;
            OnArmorEquipped?.Invoke();
        }

        public void Experience(int experience)
        {
            Exp += experience;
            OnExpChanged?.Invoke();
        }

        public void ReadBook(Book book)
        {
            if (ReadBookIDs.Contains(book.Id))
            {
                OnBookWasRead?.Invoke();
                return;
            }

            ReadBookIDs.Add(book.Id);
            Experience(book.EXP);
            OnBookRead?.Invoke();
        }
    }
}
