using EffectApply;
using ItemScriptable;

namespace ItemStats
{
    public class Item
    {
        public int MaxStack => ItemType.MaxStack;
        public float Weight => ItemType.Weight;
        public bool IsStackable => ItemType.IsStackable;
        public IEffect Effect { get; private set; }
        public ItemData ItemType { get; private set; }

        public Item(ItemData itemType, IEffect effect)
        {
            ItemType = itemType;
            Effect = effect;
        }
    }
}
