using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public int weaponDam;
    public GameObject bulletPrefab;
    public Transform firePosition;
    public Ammo ammo;
    public float fireSpeed = 0.75f;
    public float bulletSpeed = 10.0f;
    bool miniFire = false;
    private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("AudioManager not found!!!");
        }
    }

    // Update is called once per frame
    void Update()
    {
            fire();
    }

    public void fire()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!IsInvoking("fireBullet") && ammo.HasAmmo(tag))
            {
                InvokeRepeating("fireBullet", 0f, fireSpeed);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            CancelInvoke("fireBullet");
        }
    }

 

    public void fireBullet()
    {   // 1   
        Debug.Log(ammo.GetAmmo(tag));
        GameObject bullet = Instantiate(bulletPrefab) as GameObject;
        // 2   
        bullet.transform.position = firePosition.position;
        bullet.transform.rotation = firePosition.rotation;
        // 3   
        bullet.GetComponent<Rigidbody>().velocity =
            transform.forward * bulletSpeed;
        //4
        bullet.GetComponent<bullet>().damage = weaponDam;

        // Play audio when bullet is fired
        audioManager.PlaySound("LaserSound");
    }

}
