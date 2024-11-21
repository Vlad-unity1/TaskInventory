using InventorySystem;
using ItemInspector;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private Button _openInventory;
    [SerializeField] private GameObject _inventoryPanel;
    [SerializeField] private ItemHolder[] _slots;
    [SerializeField] private Button[] _useButtons;
    [SerializeField] private Button[] _addButtons;
    [SerializeField] private Button[] _removeButtons;

    private Inventory _inventory;

    public void Initialize(Inventory inventory)
    {
        _inventory = inventory;
        _inventoryPanel.SetActive(false);

        for (int i = 0; i < _slots.Length; i++)
        {
            int slotIndex = i;

            _useButtons[i].onClick.AddListener(() => TryToUse(slotIndex));
            _addButtons[i].onClick.AddListener(() => TryAddItem(slotIndex));
            _removeButtons[i].onClick.AddListener(() => TryRemoveItem(slotIndex));
        }
    }

    public void ToggleInventory()
    {
        _inventoryPanel.SetActive(!_inventoryPanel.activeSelf);
    }

    public void TryToUse(int slotIndex)
    {
        _inventory.UseItem(_slots[slotIndex].ItemData);
    }

    public void TryAddItem(int slotIndex)
    {
        _inventory.TryAddItem(_slots[slotIndex].ItemData);
    }

    public void TryRemoveItem(int slotIndex)
    {
        _inventory.RemoveItem(_slots[slotIndex].ItemData);
    }
}