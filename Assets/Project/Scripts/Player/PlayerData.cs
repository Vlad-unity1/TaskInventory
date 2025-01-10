using UnityEngine;

namespace PlayerDataScript
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 59)]
    public class PlayerData : ScriptableObject
    {
        [Header("Player Stats")]
        public int health = 200;
        public int maxHealth = 200;

        [Header("Player Inventory")]
        public int inventorySize = 9;
    }
}