using ItemScriptable;
using UnityEngine;

namespace Book
{
    [CreateAssetMenu(fileName = "BookItemData", menuName = "ScriptableObjects/BookItemData", order = 55)]
    public class BookEffect : ItemData
    {
        [SerializeField] private bool _isRead = false;
        [field: SerializeField] public int EXP { get; private set; }

        public override void UseItemEffect()
        {
            if (!_isRead)
            {
                _isRead = true;
                GlobalTarget.Experience(EXP);
            }
        }
    }
}