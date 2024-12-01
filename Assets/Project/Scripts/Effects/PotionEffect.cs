using ItemScriptable;
using Model;
using UnityEngine;

namespace Potion
{
    [CreateAssetMenu(fileName = "PotionItemData", menuName = "ScriptableObjects/PotionItemData", order = 53)]
    public class PotionEffect : ItemData
    {
        [field: SerializeField] public int HealthAmount { get; private set; }

        public override void UseItemEffect(Player player)
        {
            player.Heal(HealthAmount);
        }
    }
}
