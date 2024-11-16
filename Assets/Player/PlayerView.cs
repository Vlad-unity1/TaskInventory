using Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class PlayerView : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private TextMeshProUGUI _expText;
        [SerializeField] private Slider _healthSlider;
        [SerializeField] private TextMeshProUGUI _inventoryWeightText;

        [Header("Player Stats")]
        [SerializeField] private int _maxHP;
        [SerializeField] private float _maxInventoryWeight;

        private PlayerModel playerModel;

        public void Initialize(PlayerModel model)
        {
            playerModel = new PlayerModel(_maxHP, _maxInventoryWeight);
            playerModel = model;
            UpdatePlayerUI();
        }

        private void UpdateHealthUI()
        {
            _healthSlider.value = (float)playerModel.CurrentHP / playerModel.MaxHP;
        }

        private void UpdateExpUI()
        {
            _expText.text = $"EXP: {playerModel.Exp}";
        }

        private void UpdateInventoryUI()
        {
            _inventoryWeightText.text = $"Inventory Weight: {playerModel.Inventory.CurrentWeight}/{playerModel.Inventory.MaxWeight}";
        }

        private void UpdatePlayerUI()
        {
            UpdateHealthUI();
            UpdateExpUI();
            UpdateInventoryUI();
        }
    }
}