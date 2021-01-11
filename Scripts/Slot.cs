

using UnityEngine;

namespace DeadSilence
{

    public class Slot : MonoBehaviour {
        
        public Weapon storedWeapon;
        public GameObject storedDropObject;

        public bool IsFree()
        {
            if (!storedWeapon)
                return true;
            else
                return false;
        }
    }
}
