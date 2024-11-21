﻿using ItemScriptable;
using Model;
using UnityEngine;

namespace Weapon
{
    [CreateAssetMenu(fileName = "WeaponItemData", menuName = "ScriptableObjects/WeaponItemData", order = 54)]
    public class WeaponEffect : ItemData
    {
        public override void UseItemEffect()
        {
            GlobalTarget.EquipWeapon(this);
        }
    }
}