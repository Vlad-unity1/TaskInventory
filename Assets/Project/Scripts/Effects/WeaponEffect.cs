using EffectApply;
using ItemScriptable;
using Model;
using UnityEngine;

namespace Weapon
{
    [CreateAssetMenu(fileName = "WeaponItemData", menuName = "ScriptableObjects/WeaponItemData", order = 54)]
    public class WeaponEffect : ItemData, IEffect
    {
        public void Apply(PlayerModel player)
        {
            player.EquipWeapon(this);
        }
    }
}