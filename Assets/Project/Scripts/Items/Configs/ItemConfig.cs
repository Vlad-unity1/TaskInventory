using UnityEngine;

public abstract class ItemConfig : ScriptableObject
{
    public Sprite Image;
    public int MaxStack;
    public int Weight;
    public bool IsStackable;

    public abstract Item CreateItem(string id);
}
