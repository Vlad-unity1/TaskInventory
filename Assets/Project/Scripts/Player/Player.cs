using Armor;
using Book;
using InventorySystem;
using MessageInfo;
using System;
using System.Collections.Generic;
using UnityEngine;
using Weapon;

namespace Model
{
    public class Player
    {
        public event Action OnHealthChanged;
        public event Action OnExpChanged;
        public event Action<string> OnBookReaded;
        public event Action<WeaponEffect> OnWeaponEquipped;
        public event Action<ArmorEffect> OnArmorEquipped;

        private int _currentHP;
        public int CurrentHP
        {
            get => _currentHP;
            private set
            {
                if (_currentHP != value)
                {
                    _currentHP = value;
                    OnHealthChanged?.Invoke();
                }
            }
        }
        public int MaxHP { get; private set; }
        public int Exp { get; private set; }
        public Inventory Inventory { get; private set; }
        public List<BookEffect> _readBooks { get; private set; }
        private WeaponEffect _equippedWeapon;
        private ArmorEffect _equippedArmor;


        public Player(int maxHP, float maxInventoryWeight)
        {
            MaxHP = maxHP;
            CurrentHP = MaxHP;
            Exp = 0;
            Inventory = new Inventory(maxInventoryWeight);
            _readBooks = new List<BookEffect>();
        }

        public void Heal(int amount)
        {
            CurrentHP = Mathf.Min(CurrentHP + amount, MaxHP);
        }

        public void TakeDamage(int damage)
        {
            CurrentHP = Mathf.Max(CurrentHP - damage, 0);
        }

        public void EquipWeapon(WeaponEffect weapon)
        {
            if (_equippedArmor != null)
            {
                Inventory.ReturnItem(_equippedArmor);
                _equippedArmor = null;
            }

            _equippedWeapon = weapon;
            OnWeaponEquipped?.Invoke(weapon);
        }

        public void EquipArmor(ArmorEffect armor)
        {
            if (_equippedWeapon != null)
            {
                Inventory.ReturnItem(_equippedWeapon);
                _equippedWeapon = null;
            }

            _equippedArmor = armor;
            OnArmorEquipped?.Invoke(armor);
        }

        public void Experience(int experience)
        {
            Exp += experience;
            OnExpChanged?.Invoke();
        }

        public void ReadBook(BookEffect book)
        {
            if (!_readBooks.Contains(book))
            {
                _readBooks.Add(book);
                Experience(book.EXP);
                OnBookReaded?.Invoke(Message.BOOK_USED);
            }
            else
            {
                OnBookReaded?.Invoke(Message.BOOK_ALREADY_READ);
            }
        }
    }
}
