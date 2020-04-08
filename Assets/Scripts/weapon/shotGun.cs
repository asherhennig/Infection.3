using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotGun : Gun
{
    private AudioManager audioManager;
    public int pelletsPerShot;
    public float spread;
    public bool isPurchased;
    List<Quaternion> pellets;

    void Awake()
    {
        pellets = new List<Quaternion>(pelletsPerShot);
        for (int i = 0; i < pelletsPerShot; i++)
        {
            pellets.Add(Quaternion.Euler(Vector3.zero)); 
        }

        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("AudioManager not found!!!");
        }
    }

    void fireBullet()
    {
        ammo.ConsumeAmmo(tag);
        for(int i = 0; i < pelletsPerShot; i++)
        {
            pellets[i] = Random.rotation;
            GameObject pellet = Instantiate(bulletPrefab, firePosition.position, firePosition.rotation);
            pellet.transform.rotation = new Quaternion(pellet.transform.rotation.x, pellet.transform.rotation.y, Quaternion.RotateTowards(pellet.transform.rotation, pellets[i], spread).z, pellet.transform.rotation.w);
            pellet.GetComponent<Rigidbody>().AddForce(pellet.transform.forward * bulletSpeed);
            bulletPrefab.GetComponent<bullet>().damage = weaponDam;
            // Play sound
            audioManager.PlaySound("ShotgunSound");
        }
    }

    void upgradePelletNum()
    {
        pelletsPerShot++;
        spread += 30;
    }
    void upgradeDam()
    {
        weaponDam++;
    }
}


