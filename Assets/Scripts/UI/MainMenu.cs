using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        Time.timeScale = 1;
    }

    public void PlayButton()
    {
        SceneManager.LoadScene("Lab");
    }
    
    public void quit()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
