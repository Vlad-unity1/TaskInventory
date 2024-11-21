using InventorySystem;
using ItemInspector;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private Button _openInventory;
    [SerializeField] private Button _useItemButton; 
    [SerializeField] private GameObject _inventoryPanel;
    [SerializeField] private ItemHolder _slot;

    private Inventory _inventory;

    public void Initialize(Inventory inventory)
    {
        _inventory = inventory;
        _inventoryPanel.SetActive(false);
        _useItemButton.onClick.AddListener(TryToUse);
    }

    public void ToggleInventory()
    {
        _inventoryPanel.SetActive(!_inventoryPanel.activeSelf);
    }

    public void TryToUse()
    {
        _inventory.UseItem(_slot.ItemData);
    }

    public void TryAddItem()
    {
        _inventory.TryAddItem(_slot.ItemData);
    }
}
