using ItemScriptable;
using Model;
using UnityEngine;

namespace Book
{
    [CreateAssetMenu(fileName = "BookItemData", menuName = "ScriptableObjects/BookItemData", order = 55)]
    public class BookEffect : ItemData
    {
        [field: SerializeField] public int EXP { get; private set; }

        public override void UseItemEffect(Player player)
        {
            if (!player.ReadBooks.Contains(this))
            {
                player.ReadBook(this);
            }
        }
    }
}