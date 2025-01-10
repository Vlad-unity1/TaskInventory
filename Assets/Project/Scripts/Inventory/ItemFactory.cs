using System;
using UnityEngine;

namespace ItemsFactory
{
    public class ItemFactory : MonoBehaviour
    {
        [SerializeField] private ItemConfig[] _availableConfigs;

        public Item CreateRandomItem()
        {
            ItemConfig randomConfig = _availableConfigs[UnityEngine.Random.Range(0, _availableConfigs.Length)];
            string uniqueId = Guid.NewGuid().ToString();
            return randomConfig.CreateItem(uniqueId); 
        }
    }
}
