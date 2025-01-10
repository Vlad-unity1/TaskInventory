using Model;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using View;

namespace PlayerUIView
{
    public class PlayerUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _expText;
        [SerializeField] private TextMeshProUGUI _infoBookText;
        [SerializeField] private Slider _healthSlider;
        [SerializeField] private TextMeshProUGUI _inventoryWeightText;
        [SerializeField] private Button _takeDamageButton;
        private Player _playerModel;
        private PlayerView _playerView;

        public void Initialize(Player model, PlayerView playerView)
        {
            _playerModel = model;
            _playerView = playerView;
            UpdatePlayerUI();

            _takeDamageButton.onClick.AddListener(() => _playerView.TakeDamage(30));

            _playerModel.OnHealthChanged += UpdateHealthUI;
            _playerModel.Inventory.OnWeightChanged += UpdateInventoryUI;
            _playerModel.OnExpChanged += UpdateExpUI;
            _playerModel.OnBookRead += () => ShowInfoMessage("Книга прочитана!");
            _playerModel.OnBookWasRead += () => ShowInfoMessage("Книга уже была прочитана!");
        }

        private void OnDestroy()
        {
            _playerModel.OnHealthChanged -= UpdateHealthUI;
            _playerModel.Inventory.OnWeightChanged -= UpdateInventoryUI;
            _playerModel.OnExpChanged -= UpdateExpUI;
            _playerModel.OnBookRead -= () => ShowInfoMessage("Книга прочитана!");
            _playerModel.OnBookWasRead -= () => ShowInfoMessage("Книга уже была прочитана!");
        }

        private void UpdatePlayerUI()
        {
            UpdateHealthUI();
            UpdateExpUI();
            UpdateInventoryUI();
        }

        private void UpdateExpUI()
        {
            _expText.text = $"EXP: {_playerModel.Exp}";
        }

        private void UpdateInventoryUI()
        {
            _inventoryWeightText.text = $"Inventory Weight: {_playerModel.Inventory.CurrentWeight}/{_playerModel.Inventory.MaxWeight}";
        }

        public void UpdateHealthUI()
        {
            _healthSlider.value = (float)_playerModel.CurrentHP / _playerModel.MaxHP;
        }

        private void ShowInfoMessage(string message)
        {
            _infoBookText.text = message;
            StartCoroutine(HideInfoMessageAfterDelay(3f));
        }

        private IEnumerator HideInfoMessageAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            _infoBookText.text = string.Empty;
        }
    }
}