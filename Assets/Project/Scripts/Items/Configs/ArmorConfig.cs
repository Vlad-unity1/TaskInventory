using ItemArmor;
using UnityEngine;

namespace ArmorItem
{
    [CreateAssetMenu(fileName = "ArmorItemData", menuName = "ScriptableObjects/ArmorItemData", order = 56)]
    public class ArmorConfig : ItemConfig
    {
        [SerializeField] private GameObject _armorPrefab;
        public GameObject ArmorPrefab => _armorPrefab;

        public override Item CreateItem(string id)
        {
            return new Armor(id, this);
        }
    }
}