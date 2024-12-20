using ItemScriptable;
using Model;
using UnityEngine;

namespace PotionItem
{
    [CreateAssetMenu(fileName = "PotionItemData", menuName = "ScriptableObjects/PotionItemData", order = 53)]
    public class Potion : ItemData
    {
        [field: SerializeField] public int HealthAmount { get; private set; }

        public override void UseItemEffect(Player player)
        {
            player.Heal(HealthAmount);
        }
    }
}
