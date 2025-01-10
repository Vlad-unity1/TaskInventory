using Model;
using UnityEngine;
using WeaponItem;

public class Weapon : Item
{
    public GameObject WeaponPrefab { get; private set; }

    public Weapon(string id, ItemConfig config) : base(id, config)
    {
        if (config is WeaponConfig weaponConfig)
        {
            WeaponPrefab = weaponConfig.WeaponPrefab;
        }
    }

    public override void UseItemEffect(Player player)
    {
        player.EquipWeapon(this);
    }
}
