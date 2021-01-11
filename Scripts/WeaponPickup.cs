

using UnityEngine;

namespace DeadSilence
{
    public class WeaponPickup : MonoBehaviour
    {
        private WeaponManager weaponManager;
        
        public int ammoInWeaponCount;
        public string weaponNameToEquip;

        private void Start()
        {
            weaponManager = FindObjectOfType<WeaponManager>();
        }

        public void Pickup()
        {
                    weaponManager.EquipWeapon(weaponNameToEquip, gameObject);
                    print("Pickup:" + weaponNameToEquip);
            
        }
    }
}
