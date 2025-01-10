using ArmorItem;
using Model;
using UnityEngine;

namespace ItemArmor
{
    public class Armor : Item
    {
        public GameObject ArmorPrefab { get; private set; }

        public Armor(string id, ItemConfig config) : base(id, config)
        {
            if (config is ArmorConfig armorConfig)
            {
                ArmorPrefab = armorConfig.ArmorPrefab;
            }
        }

        public override void UseItemEffect(Player player)
        {
            player.EquipArmor(this);
        }
    }
}
