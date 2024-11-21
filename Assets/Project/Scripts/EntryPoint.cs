using ItemScriptable;
using Model;
using UnityEngine;
using View;


public class EntryPoint : MonoBehaviour
{
    [SerializeField] private PlayerView _playerView;
    [SerializeField] private InventoryController _inventoryController;

    private void Awake()
    {
        Player player = new(200, 200);
        _playerView.Initialize(player);
        _inventoryController.Initialize(player.Inventory);
        ItemData.SetGlobalTarget(player);
    }
}
