using Model;
using UnityEngine;

namespace ItemScriptable
{
    public class ItemData : ScriptableObject
    {
        public int MaxStack;
        public float Weight;
        public bool IsStackable;
        public Sprite Image;
        public string Name;

        public virtual void UseItemEffect(Player player) { }
    }
}