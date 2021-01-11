
using UnityEngine;

namespace DeadSilence
{
    
    public class LadderTrigger : MonoBehaviour {

        private FPSController controller;
        /*
        private void Start()
        {
            controller = GameObject.FindGameObjectWithTag("Player").GetComponent<FPSController>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                controller.isClimbing = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                controller.isClimbing = false;
            }
        }*/
    }
}
