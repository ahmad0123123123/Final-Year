

using UnityEngine;

namespace DeadSilence
{
    public class GetCollisionTag : MonoBehaviour
    {
        public string contactTag;

        private void Update()
        {
            RaycastHit hit;

            if(Physics.Raycast(transform.position, -transform.up, out hit, 1.5f))
            {
                contactTag = hit.collider.tag;
            }
        }
    }
}
