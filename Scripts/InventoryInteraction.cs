

using UnityEngine;
using UnityEngine.UI;

namespace DeadSilence
{
    public class InventoryInteraction : MonoBehaviour
    {
        public Item item;
        public Inventory inventory;
        public Text infoText;

        private void Start()
        {
            inventory = FindObjectOfType<Inventory>();
        }

        public void RemoveItem()
        {
            inventory.RemoveItem(item, false);
            this.gameObject.SetActive(false);
        }

        public void Useitem()
        {
            if (item.type == ItemType.consumable)
            {
                inventory.UseItem(item, false);
            }
            gameObject.SetActive(false);
        }
    }
}
