using Buttons;
using ItemInspector;
using ItemScriptable;
using Model;
using UnityEngine;
using View;
using ViewInventory;

namespace EntryPoinInProject
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private PlayerView _playerView;
        [SerializeField] private InventoryView _inventoryView;
        [SerializeField] private InventoryButtons _inventoryButtons;

        private void Awake()
        {
            Player player = new(200, 200);
            ItemHolder holder = new();
            ItemData itemData = ScriptableObject.CreateInstance<ItemData>();

            _playerView.Initialize(player);
            holder.Initialize(itemData);
            _inventoryView.Initialize(player.Inventory, player);
            _inventoryButtons.Initialize(_inventoryView);
        }
    }
}