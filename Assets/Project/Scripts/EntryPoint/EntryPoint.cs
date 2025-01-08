using Buttons;
using Model;
using PlayerUIView;
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
        [SerializeField] private PlayerData _playerData;
        [SerializeField] private PlayerUI _playerUI;

        private void Awake()
        {
            Player player = new(_playerData.maxHealth, _playerData.health, _playerData.inventorySize);

            _playerUI.Initialize(player, _playerView);
            _playerView.Initialize(player, _playerUI);
            _inventoryView.Initialize(player.Inventory, player);
            _inventoryButtons.Initialize(_inventoryView);
        }
    }
}