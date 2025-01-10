namespace ItemInspector
{
    public class Slot
    {
        public Item Item { get; private set; }
        public int Amount { get; private set; }
        public int StackCount { get; private set; }
        public bool IsEmpty => Item == null || Amount == 0;

        public void SetItem(Item item, int amount)
        {
            if (IsEmpty)
            {
                Item = item;
                Amount = amount;
            }
            else if (Item.Config == item.Config)
            {
                Amount += amount;
            }
        }

        private void ClearSlot()
        {
            Item = null;
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