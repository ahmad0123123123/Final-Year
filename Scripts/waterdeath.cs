using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DeadSilence;
public class waterdeath : MonoBehaviour
{
    Collider[] colliders;

    // Update is called once per frame
    void Update()
    {

        xsdg();
    }

    
    void xsdg()
    {
        
        colliders = Physics.OverlapSphere(transform.position, 5f);
        foreach (Collider collider in colliders)
        {
            collider.GetComponent<PlayerStats>().health = 0;

        }    
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="water")
        {
            xsdg();
        }
    }
}
