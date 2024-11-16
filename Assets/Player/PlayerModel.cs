using Armor;
using InventorySystem;
using UnityEngine;
using Weapon;

namespace Model
{
    public class PlayerModel
    {
        public int CurrentHP { get; private set; }
        public int MaxHP { get; private set; }
        public int Exp { get; private set; }
        public Inventory Inventory { get; private set; }

        public PlayerModel(int maxHP, float maxInventoryWeight)
        {
            MaxHP = maxHP;
            CurrentHP = MaxHP;
            Exp = 0;
            Inventory = new Inventory(maxInventoryWeight);
        }

        public void Heal(int amount)
        {
            CurrentHP += amount;
            if (CurrentHP > MaxHP) CurrentHP = MaxHP;
        }

        public void TakeDamage(int damage)
        {
            CurrentHP -= damage;
            if (CurrentHP < 0) CurrentHP = 0;
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
