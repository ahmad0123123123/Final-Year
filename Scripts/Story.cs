using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Story : MonoBehaviour
{

    public GameObject story;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("asdasd");
        if (Input.GetKey(KeyCode.P))
        {
            story.SetActive(false);
            //DestroyObject(story);
            Time.timeScale = 1;
            story = GameObject.Find("Panel (1)");
            story = GameObject.Find("Panel (2)");
            story = GameObject.Find("Panel (3)");
            story = GameObject.Find("Panel (4)");
        }

    }

    public void openpanel()
    {
        
        DestroyObject(story);
    }

}
