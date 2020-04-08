using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Paused : MonoBehaviour
{
    private bool isPaused = false;
    GameObject[] pauseMenu;
    GameObject[] pauseButton;
    GameObject[] Options;
    GameObject[] purchase;

    //Caled once when the game starts
    void Start()
    {
        //Sets the game to running
        Time.timeScale = 1;
        //Finds the Object Tags
        pauseMenu = GameObject.FindGameObjectsWithTag("ShowOnPause");
        pauseButton = GameObject.FindGameObjectsWithTag("HideOnPause");
        Options = GameObject.FindGameObjectsWithTag("Option");
        purchase = GameObject.FindGameObjectsWithTag("Purchase");
        //Hides the Menus
        hidePaused();
        hideOptions();
        HidePurchase();
    }
    //When the options button is pressed, show the options menu and keep the game paused
    public void Option()
    {
        Time.timeScale = 0;
        showOptions();
    }
    //Pauses the Game
    public void Pause()
    {
        Time.timeScale = 0;
        isPaused = true;
        showPaused();
    }
    //Unpauses the Game
     public void Unpause()
     {
        Time.timeScale = 1;
        isPaused = false;
        hidePaused();
        hideOptions();
    }

    public bool IsGamePaused()
    {
        return isPaused;
    }

    void Update()
    {
        
    }
    //Shows the Pause Menu
    public void showPaused()
    {
        foreach (GameObject g in pauseMenu)
        {
            g.SetActive(true);
        }
        foreach (GameObject g in pauseButton)
        {
            g.SetActive(false);
        }
    }
    //Hides the Pause Menu
    public void hidePaused()
    {
        foreach (GameObject g in pauseMenu)
        {
            g.SetActive(false);
        }
        foreach (GameObject g in pauseButton)
        {
            g.SetActive(true);
        }
    }
    //Shows the Options Menu
    public void showOptions()
    {
        foreach (GameObject g in Options)
        {
            g.SetActive(true);
        }
    }
    //Hides the Option Menu
    public void hideOptions()
    {
        foreach (GameObject g in Options)
        {
            g.SetActive(false);
        }
    }
    //Restarts the game scene
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        hideOptions();
    }
    //Exits the game to the main menu scene
    public void Exit()
    {
        SceneManager.LoadScene(sceneName:"Main menu");
        hideOptions();
    }
    //Asks if the player is sure about their purchase
    public void Purchase()
    {
        foreach (GameObject g in purchase)
        {
            g.SetActive(true);
        }
    }

    public void HidePurchase()
    {
        foreach (GameObject g in purchase)
        {
            g.SetActive(false);
        }
    }
}

