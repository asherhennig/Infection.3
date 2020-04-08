using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunEquipper : MonoBehaviour
{

    public static string activeWeaponType;

    public GameObject pistol;
    public GameObject shotgun;
    public GameObject fragGrenade;
    public GameObject lureGrenade;
    public GameObject miniGun;

    Animator heroAnim;

    Ammo Ammo;

    //GameObject gameObject;
    GameObject activeGun;

    // Start is called before the first frame update
    void Start()
    {
        heroAnim = GetComponent<Animator>();
        activeWeaponType = Constants.Pistol;
        activeGun = pistol;
        heroAnim.SetBool("SetActive_pistol", true);
    }

    private void loadWeapons(GameObject weapon)
    {
        pistol.SetActive(false);
        shotgun.SetActive(false);
        fragGrenade.SetActive(false);
        lureGrenade.SetActive(false);
        miniGun.SetActive(false);

        weapon.SetActive(true);
        activeGun = weapon;
    }
    //ACTIVATES MINI GUN AND STARTS THE CORUTINE TO FIRE
    public void activeMiniGun()
    {
        
        //load  mini gun
        loadWeapons(miniGun);
        //set active gun to mini gun
        activeWeaponType = Constants.miniGun;

        heroAnim.SetBool("SetActive_miniGun", true);
        heroAnim.SetBool("SetActive_shotgun", false);
        heroAnim.SetBool("SetActive_pistol", false);
    }

    public void deactiveMiniGun()
    {
        loadWeapons(pistol);
        activeWeaponType = Constants.Pistol;

        heroAnim.SetBool("SetActive_pistol", true);
    }

    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            loadWeapons(pistol);
            activeWeaponType = Constants.Pistol;

            heroAnim.SetBool("SetActive_pistol", true);
            heroAnim.SetBool("SetActive_shotgun", false);
        }
        else if (Input.GetKeyDown("2") && shotgun.GetComponent<shotGun>().isPurchased == true)
        {
            loadWeapons(shotgun);
            activeWeaponType = Constants.Shotgun;

            heroAnim.SetBool("SetActive_shotgun", true);
            heroAnim.SetBool("SetActive_pistol", false);
        }
        else if (Input.GetKeyDown("3"))
        {
            loadWeapons(fragGrenade);
            activeWeaponType = Constants.Grenade;
        }
        else if (Input.GetKeyDown("4"))
        {
            loadWeapons(lureGrenade);
            activeWeaponType = Constants.lureGrenade;
        }
    }
}
