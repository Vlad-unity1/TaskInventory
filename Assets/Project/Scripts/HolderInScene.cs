using ItemScriptable;
using UnityEngine;

public class HolderInScene : MonoBehaviour
{
    [SerializeField] private ItemData[] _itemData;

    public ItemData GetItem()
    {
        int randomIndex = Random.Range(0, _itemData.Length);
        return _itemData[randomIndex];
    }
}
