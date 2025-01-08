using BookItem;
using ItemScriptable;
using UnityEngine;

namespace ItemsHolder
{
    public class SlotView : MonoBehaviour
    {
        [SerializeField] private ItemData[] _itemData;

        public ItemData GetItem()
        {
            int randomIndex = Random.Range(0, _itemData.Length);

            if (_itemData[randomIndex] is Book book)
            {
                var clonedBook = Instantiate(book);
                clonedBook.GenerateRandomID();
                return clonedBook;
            }

            return _itemData[randomIndex];
        }
    }
}
