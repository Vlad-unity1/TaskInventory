using ItemScriptable;
using Model;
using UnityEngine;

namespace ArmorItem
{
    [CreateAssetMenu(fileName = "ArmorItemData", menuName = "ScriptableObjects/ArmorItemData", order = 56)]
    public class Armor : ItemData
    {
        [SerializeField] private GameObject _armorPrefab;
        public GameObject ArmorPrefab => _armorPrefab;

        public override void UseItemEffect(Player player)
        {
            player.EquipArmor(this);
        }
    }
}