using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    
    private void Start()
    {
        //PlayerPrefs.SetInt("Level", 0);
        
        
    }
    private void Update()
    {
        


        
    }

    public void Resume()
    {
        //PlayerPrefs.SetInt("Level", 0);
        if (PlayerPrefs.GetInt("Level") == 0)
            SceneManager.LoadScene(1);
        else
            SceneManager.LoadScene(PlayerPrefs.GetInt("Level"));
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //Debug.Log(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
    public void NewGame()
    {
        PlayerPrefs.SetInt("Level", 1);
        SceneManager.LoadScene(PlayerPrefs.GetInt("Level"));
    }
}
