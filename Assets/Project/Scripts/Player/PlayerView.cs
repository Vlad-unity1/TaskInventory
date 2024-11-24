using Armor;
using MessageInfo;
using Model;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Weapon;

namespace View
{
    public class PlayerView : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private TextMeshProUGUI _expText;
        [SerializeField] private TextMeshProUGUI _infoBookText;
        [SerializeField] private Slider _healthSlider;
        [SerializeField] private TextMeshProUGUI _inventoryWeightText;
        [SerializeField] private Button _takeDamageButton;

        [Header("Equipment Slots")]
        [SerializeField] private Transform _weaponSlot;
        [SerializeField] private Transform _armorSlot;

        private Player _playerModel;

        public void Initialize(Player model)
        {
            _playerModel = model;
            UpdatePlayerUI();

            _takeDamageButton.onClick.AddListener(() => TakeDamage(30));

            _playerModel.OnHealthChanged += UpdateHealthUI;
            _playerModel.Inventory.OnWeightChanged += UpdateInventoryUI;
            _playerModel.OnExpChanged += UpdateExpUI;
            _playerModel.OnWeaponEquipped += EquipWeapon;
            _playerModel.OnArmorEquipped += EquipArmor;
            _playerModel.OnBookReaded += ShowInfoMessage;
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

        private void EquipWeapon(WeaponEffect weapon)
        {
            Instantiate(weapon.WeaponPrefab, _weaponSlot.transform);
        }

        private void EquipArmor(ArmorEffect armor)
        {
            Instantiate(armor.ArmorPrefab, _weaponSlot.transform);
        }

        private void UpdatePlayerUI()
        {
            UpdateHealthUI();
            UpdateExpUI();
            UpdateInventoryUI();
        }

        private void ShowInfoMessage(string message)
        {
            StartCoroutine(Message.ShowMessage(_infoBookText, message));
        }
    }
}