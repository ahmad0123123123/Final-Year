using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterlevel : MonoBehaviour
{
    public GameObject water_level;
    
    // Update is called once per frame
    void Update()
    {
        
        water_level.transform.position += new Vector3(0, 0.05f , 0) * Time.deltaTime;
        
    }
}
