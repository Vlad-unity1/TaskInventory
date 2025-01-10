using Model;
using PlayerUIView;
using UnityEngine;

namespace View
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private Transform _weaponSlot;
        [SerializeField] private Transform _armorSlot;

        private PlayerUI _playerUI;
        private Player _playerModel;
        private GameObject _equippedWeaponInstance;
        private GameObject _equippedArmorInstance;

        public void Initialize(Player model, PlayerUI playerUI)
        {
            _playerModel = model;
            _playerUI = playerUI;

            _playerModel.OnWeaponEquipped += EquipWeapon;
            _playerModel.OnArmorEquipped += EquipArmor;
        }

        private void OnDestroy()
        {
            _playerModel.OnWeaponEquipped -= EquipWeapon;
            _playerModel.OnArmorEquipped -= EquipArmor;
        }

        public void TakeDamage(int damage)
        {
            _playerModel.TakeDamage(damage);
            _playerUI.UpdateHealthUI();
        }

        private void EquipWeapon()
        {
            if (_equippedWeaponInstance != null)
            {
                Destroy(_equippedWeaponInstance);
            }

            _equippedWeaponInstance = Instantiate(_playerModel.EquippedWeapon.WeaponPrefab, _weaponSlot.transform);
        }

        private void EquipArmor()
        {
            if (_equippedArmorInstance != null)
            {
                Destroy(_equippedArmorInstance);
            }

            _equippedArmorInstance = Instantiate(_playerModel.EquippedArmor.ArmorPrefab, _armorSlot.transform);
        }
    }
}