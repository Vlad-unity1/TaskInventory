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

        public static Player GlobalTarget { get; private set; }

        public static void SetGlobalTarget(Player player)
        {
            GlobalTarget = player;
        }

        public virtual void UseItemEffect() { }
    }
}