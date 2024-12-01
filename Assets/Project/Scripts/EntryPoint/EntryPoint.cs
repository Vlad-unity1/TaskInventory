using MessageInfo;
using Model;
using UnityEngine;
using View;
using ViewInventory;

namespace EntryPoinInProject
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private PlayerView _playerView;
        [SerializeField] private InventoryView _inventoryController;

        private void Awake()
        {
            Player player = new(200, 200);
            Message message = new();
            _playerView.Initialize(player, message);
            _inventoryController.Initialize(player.Inventory, player, message);
        }
    }
}