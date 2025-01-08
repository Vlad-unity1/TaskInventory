using ItemScriptable;

namespace ItemInspector
{
    public class Slot
    {
        public ItemData ItemData { get; private set; }
        public int Amount { get; private set; }
        public int StackCount { get; private set; }
        public bool IsEmpty => ItemData == null || Amount == 0;

        public void SetItem(ItemData itemData, int amount)
        {
            if (IsEmpty)
            {
                ItemData = itemData;
                Amount = amount;
            }
            else if (ItemData == itemData)
            {
                Amount += amount;
            }
        }

        private void ClearSlot()
        {
            ItemData = null;
            Amount = 0;
        }

        public void DecreaseAmount(int value)
        {
            Amount -= value;

            if (Amount <= 0)
            {
                ClearSlot();
            }
        }
    }
}