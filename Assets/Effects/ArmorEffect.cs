using EffectApply;
using ItemScriptable;
using Model;
using UnityEngine;

namespace Armor 
{
    [CreateAssetMenu(fileName = "ArmorItemData", menuName = "ScriptableObjects/ArmorItemData", order = 56)]
    public class ArmorEffect : ItemData, IEffect
    {
        public void Apply(PlayerModel player)
        {
            player.EquipArmor(this);
        }
    }
}