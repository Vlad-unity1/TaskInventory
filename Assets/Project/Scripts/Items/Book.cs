using ItemScriptable;
using Model;
using System;
using UnityEngine;

namespace BookItem
{
    [CreateAssetMenu(fileName = "BookItemData", menuName = "ScriptableObjects/BookItemData", order = 55)]
    public class Book : ItemData
    {
        [field: SerializeField] public int EXP { get; private set; }
        public string RandomID { get; private set; } = string.Empty;

        public override void UseItemEffect(Player player)
        {
            player.ReadBook(this);
        }

        public void GenerateRandomID()
        {
            RandomID = Guid.NewGuid().ToString();
        }
    }
}