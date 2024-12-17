using Armor;
using Book;
using InventorySystem;
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
        public Inventory Inventory { get; private set; }
        public List<BookEffect> ReadBooks { get; private set; }
        private int _currentHp;
        private WeaponEffect _equippedWeapon;
        private ArmorEffect _equippedArmor;

        public Player(int maxHP, float maxInventoryWeight)
        {
            MaxHP = maxHP;
            CurrentHP = MaxHP;
            Exp = 0;
            Inventory = new Inventory(9, maxInventoryWeight);
            ReadBooks = new List<BookEffect>();
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
            if (!ReadBooks.Contains(book))
            {
                ReadBooks.Add(book);
                Experience(book.EXP);
                OnBookReaded?.Invoke("Книга прочитана, опыт получен");
            }
            else
            {
                OnBookReaded?.Invoke("Эта книга была прочитана");
            }
        }
    }
}
