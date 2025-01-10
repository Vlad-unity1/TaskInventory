using UnityEngine;

namespace BookItem
{
    [CreateAssetMenu(fileName = "BookItemData", menuName = "ScriptableObjects/BookItemData", order = 55)]
    public class BookConfig : ItemConfig
    {
        [field: SerializeField] public int EXP { get; private set; }

        public override Item CreateItem(string id)
        {
            return new Book(id, this);
        }
    }
}