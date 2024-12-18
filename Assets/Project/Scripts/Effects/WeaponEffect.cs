﻿using ItemScriptable;
using Model;
using UnityEngine;

namespace Weapon
{
    [CreateAssetMenu(fileName = "WeaponItemData", menuName = "ScriptableObjects/WeaponItemData", order = 54)]
    public class WeaponEffect : ItemData
    {
        [SerializeField] private GameObject _weaponPrefab;
        public GameObject WeaponPrefab => _weaponPrefab;

        public override void UseItemEffect(Player player)
        {
            player.EquipWeapon(this);
        }
    }
}