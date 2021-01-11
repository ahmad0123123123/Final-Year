

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace DeadSilence
{
    public class NPCVision : MonoBehaviour
    {
        [Tooltip("How often vision will check if player is in sight")]
        public float visibilityCheckInterval = 0.1f;
        [Tooltip("NPC field of fiev range")]
        public float FOV = 100;
        [Tooltip("How far NPC can look")]
        public float detectionRange = 30;

        private Transform player;

        [HideInInspector]
        public bool isPlayerVisible;
        [HideInInspector]
        public Vector3 lastPlayerPosition;

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

            StartCoroutine(VisibilityCheck(visibilityCheckInterval));
        }

        //Simple coroutine that calling with interval step
        IEnumerator VisibilityCheck(float visibilityCheckInterval)
        {
            for (; ; )
            {
                Vector3 direction = player.transform.position - transform.position;
                float angle = Vector3.Angle(direction, transform.forward);

                RaycastHit hit;

                if (angle < FOV * 0.5f)
                {
                    if (Physics.Raycast(transform.position, direction, out hit, detectionRange))
                    {
                        if (hit.transform.tag == "Player")
                        {
                            Debug.DrawLine(transform.position, player.transform.position, Color.green);
                            isPlayerVisible = true;
                            lastPlayerPosition = player.transform.position;
                        }
                        else
                        {
                            isPlayerVisible = false;
                            Debug.DrawLine(transform.position, player.transform.position, Color.red);
                        }
                    }
                }
                else
                {
                    isPlayerVisible = false;
                    Debug.DrawLine(transform.position, player.transform.position, Color.red);
                }

                yield return new WaitForSeconds(visibilityCheckInterval);
            }
        }
    }
}
