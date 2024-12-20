using ArmorItem;
using ItemScriptable;
using WeaponItem;

namespace ItemInspector
{
    public class ItemHolder
    {
        private ItemData _itemData;
        private int _amount;
        private readonly int _stackCount;
        public bool IsEmpty => _itemData == null || _amount == 0;

        public void Initialize(ItemData itemData)
        {
            _itemData = itemData;
        }

        public void SetItem(ItemData itemData, int amount)
        {
            if (IsEmpty)
            {
                _itemData = itemData;
                _amount = amount;
            }
            else if (_itemData == itemData)
            {
                _amount += amount;
            }
        }

        private void ClearSlot()
        {
            _itemData = null;
            _amount = 0;
        }

        public ItemData GetItem() => _itemData;

        public int GetAmount() => _amount;

        public int GetStackCount() => _stackCount;

        public void DecreaseAmount(int value)
        {
            _amount -= value;

            if (_amount <= 0)
            {
                ClearSlot();
            }
        }
    }
}