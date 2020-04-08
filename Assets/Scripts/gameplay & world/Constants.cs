using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants
{
    //Weapon Types
    public const string Pistol = "Pistol";
    public const string Shotgun = "Shotgun";
    public const string Grenade = "Grenade";
    public const string lureGrenade = "lureGrenade";
    public const string miniGun = "miniGun";

    // Pick-ups
    public const int healthPickUp1 = 1;
    public const int HealthPickUpFull = 2;
    public const int bubbleGum1 = 3;
    public const int bubbleGum5 = 4;
    public const int miniGunPickUp = 5;

    //Enemy Types
    public const string Spider = "Spider";

    public static readonly int[] AllPickUps = new int[5]
    {
        healthPickUp1,
        HealthPickUpFull,
        bubbleGum1,
        bubbleGum5,
        miniGunPickUp
    };

}
