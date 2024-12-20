using ArmorItem;
using Model;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WeaponItem;

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
        private GameObject _equippedWeaponInstance;
        private GameObject _equippedArmorInstance;

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
        private void OnDestroy()
        {
            _playerModel.OnHealthChanged -= UpdateHealthUI;
            _playerModel.Inventory.OnWeightChanged -= UpdateInventoryUI;
            _playerModel.OnExpChanged -= UpdateExpUI;
            _playerModel.OnWeaponEquipped -= EquipWeapon;
            _playerModel.OnArmorEquipped -= EquipArmor;
            _playerModel.OnBookReaded -= ShowInfoMessage;
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

        private void EquipWeapon(WeaponItem.Weapon weapon)
        {
            if (_equippedWeaponInstance != null)
            {
                Destroy(_equippedWeaponInstance);
            }

            _equippedWeaponInstance = Instantiate(weapon.WeaponPrefab, _weaponSlot.transform);
        }

        private void EquipArmor(ArmorItem.Armor armor)
        {
            if (_equippedArmorInstance != null)
            {
                Destroy(_equippedArmorInstance);
            }

            _equippedArmorInstance = Instantiate(armor.ArmorPrefab, _armorSlot.transform);
        }

        private void UpdatePlayerUI()
        {
            UpdateHealthUI();
            UpdateExpUI();
            UpdateInventoryUI();
        }

        private void ShowInfoMessage(string message)
        {
            _infoBookText.text = message;
            StartCoroutine(HideInfoMessageAfterDelay(3f));
        }

        private IEnumerator HideInfoMessageAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            _infoBookText.text = "";
        }
    }
}