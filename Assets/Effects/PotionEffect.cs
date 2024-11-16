using EffectApply;
using ItemScriptable;
using Model;
using UnityEngine;

namespace Potion
{
    [CreateAssetMenu(fileName = "PotionItemData", menuName = "ScriptableObjects/PotionItemData", order = 53)]
    public class PotionEffect : ItemData, IEffect
    {
        [field: SerializeField] public int HealthAmount { get; private set; }

        public void Apply(PlayerModel player)
        {
            player.Heal(HealthAmount);
        }
    }
}
