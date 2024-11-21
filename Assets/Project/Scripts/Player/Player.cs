using Armor;
using InventorySystem;
using System;
using UnityEngine;
using Weapon;

namespace Model
{
    public class Player
    {
        public event Action OnHealthChanged;

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

        public Player(int maxHP, float maxInventoryWeight)
        {
            MaxHP = maxHP;
            CurrentHP = MaxHP;
            Exp = 0;
            Inventory = new Inventory(maxInventoryWeight);
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
            Debug.Log($"WeaponEffect {weapon} equip.");
        }

        public void EquipArmor(ArmorEffect armor)
        {
            Debug.Log($"ArmorEffect {armor} equip.");
        }

        public void Experience(int experience)
        {
            Exp += experience;
        }
    }
}
