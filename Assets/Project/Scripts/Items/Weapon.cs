using ItemScriptable;
using Model;
using UnityEngine;

namespace WeaponItem
{
    [CreateAssetMenu(fileName = "WeaponItemData", menuName = "ScriptableObjects/WeaponItemData", order = 54)]
    public class Weapon : ItemData
    {
        [SerializeField] private GameObject _weaponPrefab;
        public GameObject WeaponPrefab => _weaponPrefab;

        public override void UseItemEffect(Player player)
        {
            player.EquipWeapon(this);
        }
    }
}