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
        [SerializeField] private Button _takeDamageButton;

        private Player _playerModel;

        public void Initialize(Player model)
        {
            _playerModel = model;
            UpdatePlayerUI();
            _takeDamageButton.onClick.AddListener(() => TakeDamage(30));
            _playerModel.OnHealthChanged += UpdateHealthUI;
        }

        private void UpdateHealthUI()
        {
            _healthSlider.value = (float)_playerModel.CurrentHP / _playerModel.MaxHP;
        }

        private void UpdateExpUI()
        {
            _expText.text = $"EXP: {_playerModel.Exp}";
        }

        private void UpdateInventoryUI()
        {
            _inventoryWeightText.text = $"Inventory Weight: {_playerModel.Inventory.CurrentWeight}/{_playerModel.Inventory.MaxWeight}";
        }

        public void TakeDamage(int damage)
        {
            _playerModel.TakeDamage(damage);
            UpdateHealthUI();
        }

        private void UpdatePlayerUI()
        {
            UpdateHealthUI();
            UpdateExpUI();
            UpdateInventoryUI();
        }
    }
}