

using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// We grab ammo from inventory and pass data to the Weapon.cs where we get needed ammo by index
/// </summary>

namespace DeadSilence
{
    public class AmmoCheck : MonoBehaviour
    {
        public string neededAmmoName;
        public Inventory inventory;

        public List<Item> ammoItems;

        private void Start()
        {
            inventory = FindObjectOfType<Inventory>();
        }

        private void Update()
        {
            foreach (var item in inventory.characterItems)
            {
                if (item.type == ItemType.ammo && !ammoItems.Contains(item))
                {
                    ammoItems.Add(item);
                }
            }
        }

        private int CountAmmo()
        {
            return ammoItems.Sum(_ammo => _ammo.ammo);
        }
    }
}
