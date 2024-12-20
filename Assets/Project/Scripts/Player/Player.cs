using ArmorItem;
using BookItem;
using InventorySystem;
using System;
using System.Collections.Generic;
using UnityEngine;
using WeaponItem;

namespace Model
{
    public class Player
    {
        public event Action OnHealthChanged;
        public event Action OnExpChanged;
        public event Action<string> OnBookReaded;
        public event Action<Weapon> OnWeaponEquipped;
        public event Action<Armor> OnArmorEquipped;

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
        private Weapon _equippedWeapon;
        private Armor _equippedArmor;

        public Player(int maxHP, float maxInventoryWeight)
        {
            MaxHP = maxHP;
            CurrentHP = MaxHP;
            Exp = 0;
            Inventory = new Inventory(9, maxInventoryWeight);
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
            if (_equippedWeapon != null)
            {
                Inventory.ReturnItem(_equippedWeapon);
            }

            _equippedWeapon = weapon;
            OnWeaponEquipped?.Invoke(weapon);
        }

        public void EquipArmor(Armor armor)
        {
            if (_equippedArmor != null)
            {
                Inventory.ReturnItem(_equippedArmor);
            }

            _equippedArmor = armor;
            OnArmorEquipped?.Invoke(armor);
        }

        public void Experience(int experience)
        {
            Exp += experience;
            OnExpChanged?.Invoke();
        }

        public void ReadBook(Book book)
        {
            if (ReadBookIDs.Contains(book.RandomID))
            {
                OnBookReaded?.Invoke("Эта книга уже прочитана!");
                return;
            }

            ReadBookIDs.Add(book.RandomID);
            Experience(book.EXP);
            OnBookReaded?.Invoke($"Книга прочитана! Получено {book.EXP} опыта.");
        }
    }
}
