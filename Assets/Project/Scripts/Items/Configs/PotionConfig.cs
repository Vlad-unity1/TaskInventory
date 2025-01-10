using UnityEngine;

namespace PotionItem
{
    [CreateAssetMenu(fileName = "PotionItemData", menuName = "ScriptableObjects/PotionItemData", order = 53)]
    public class PotionConfig : ItemConfig
    {
        [field: SerializeField] public int HealthAmount { get; private set; }

        public override Item CreateItem(string id)
        {
            return new Potion(id, this);
        }
    }
}
