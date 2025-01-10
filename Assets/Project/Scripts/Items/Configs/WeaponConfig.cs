using UnityEngine;

namespace WeaponItem
{
    [CreateAssetMenu(fileName = "WeaponItemData", menuName = "ScriptableObjects/WeaponItemData", order = 54)]
    public class WeaponConfig : ItemConfig
    {
        [SerializeField] private GameObject _weaponPrefab;
        public GameObject WeaponPrefab => _weaponPrefab;

        public override Item CreateItem(string id)
        {
            return new Weapon(id, this);
        }
    }
}