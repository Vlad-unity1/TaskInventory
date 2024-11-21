using ItemScriptable;

namespace ItemStats
{
    public class Item
    {
        public int MaxStack => ItemData.MaxStack;
        public float Weight => ItemData.Weight;
        public bool IsStackable => ItemData.IsStackable;
        public ItemData ItemData { get; private set; }

        public Item(ItemData itemData)
        {
            ItemData = itemData;
        }
    }
}
