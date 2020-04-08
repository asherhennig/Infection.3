using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public GameObject Player;
  
    //variables to save
    int health;
    int shotgunAmmo;
    int grenadeAmmo;
    int score;
    int money;
    int maxHealth;

    void Start()
    {
        //find the game objects with the tags so you can access the variables in the ammo and player script
        Player = GameObject.FindGameObjectWithTag("Player");
       
        //sets variables in the save script to be the variables in other scripts wanting to save
         health = Player.GetComponent<Player>().curHealth;
        int shotgunAmmo = Player.GetComponent<Ammo>().shotgunAmmo;
        int grenadeAmmo = Player.GetComponent<Ammo>().grenadeAmmo;
         score = GetComponent<GameManager>().score;
         money = GetComponent<GameManager>().bubblegum;
         maxHealth = Player.GetComponent<Player>().maxHealth;
    }

    public void gameLoad()
    {
        //load previous player prefs
        PlayerPrefs.GetInt("health");
        //PlayerPrefs.GetInt("shot gun ammo");
        PlayerPrefs.GetInt("score");
        PlayerPrefs.GetInt("bubblegum");
        PlayerPrefs.GetInt("max health");
        Debug.Log(health);

    }

    public void gameSave()
    {
        //set player prefs for current state of the game
        PlayerPrefs.SetInt("health", health);
        PlayerPrefs.SetInt("shot gun ammo", shotgunAmmo);
        PlayerPrefs.SetInt("gernade", grenadeAmmo);
        PlayerPrefs.SetInt("score", score);
        PlayerPrefs.SetInt("bubblegum", money);
        PlayerPrefs.SetInt("max health", maxHealth);
        Debug.Log(health);
        Debug.Log(money);
    }
}


