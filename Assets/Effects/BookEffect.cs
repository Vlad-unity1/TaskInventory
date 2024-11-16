using EffectApply;
using ItemScriptable;
using Model;
using System;
using UnityEngine;

namespace Book
{
    [CreateAssetMenu(fileName = "BookItemData", menuName = "ScriptableObjects/BookItemData", order = 55)]
    public class BookEffect : ItemData, IEffect
    {
        [SerializeField] private bool _isRead = false;
        [field: SerializeField] public int EXP { get; private set; }

        public void Apply(PlayerModel player)
        {
            if (!_isRead)
            {
                _isRead = true;
                player.Experience(EXP);
            }
        }
    }
}