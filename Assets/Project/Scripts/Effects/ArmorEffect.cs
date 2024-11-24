using ItemScriptable;
using UnityEngine;

namespace Armor
{
    [CreateAssetMenu(fileName = "ArmorItemData", menuName = "ScriptableObjects/ArmorItemData", order = 56)]
    public class ArmorEffect : ItemData
    {
        [SerializeField] private GameObject _armorPrefab;

        public GameObject ArmorPrefab => _armorPrefab;

        public override void UseItemEffect()
        {
            GlobalTarget.EquipArmor(this);
        }
    }
}