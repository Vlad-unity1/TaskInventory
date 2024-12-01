using ItemTypeEnum;
using Model;
using UnityEngine;

namespace ItemScriptable
{
    public class ItemData : ScriptableObject
    {
        public ItemType Type;
        public int MaxStack;
        public float Weight;
        public bool IsStackable;
        public Sprite Image;

        public virtual void UseItemEffect(Player player) { }
    }
}