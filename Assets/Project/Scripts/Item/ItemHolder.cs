using ItemScriptable;
using UnityEngine;

namespace ItemInspector
{
    public class ItemHolder : MonoBehaviour
    {
        [SerializeField] private ItemData _itemData;
        public ItemData ItemData => _itemData;

        public void SetItem(ItemData itemData)
        {
            _itemData = itemData;
        }
    }
}