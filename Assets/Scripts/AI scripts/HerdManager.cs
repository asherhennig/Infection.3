using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerdManager : MonoBehaviour
{
    public GameObject enemyPrefab;                       //whether easy, med, hard
    public int maxHerd = 20;                            //just an example quantity until we link the wave manager
    public GameObject[] allEnemy;                        // array of total instantiated fish
    public Vector3 limpLimits = new Vector3(5, 5, 5);    //radius value for the flocks distance range - incrcease numbers for a bigger range
    public Vector3 targetPos;                              //vec3 to modify x, y and z coordinates.

    //Inspector settings for manual changes
    [Header("Herd Settings")]      
    [Range(0.0f, 5.0f)]
    public float minSpeed;
    [Range(0.0f, 5.0f)]
    public float maxSpeed;
    [Range(1.0f, 10.0f)]
    public float neighbourDist;
    [Range(0.0f, 5.0f)]
    public float rotationSpeed;


     void Start()
    {
        allEnemy = new GameObject[maxHerd];      //create fish and an array to store
        for (int i = 0; i < maxHerd; i++)       //loop is creating a position to place the enemy
                                                  //That position is based on the flock manager + a random vector3 value
        {
            Vector3 pos = this.transform.position + new Vector3(Random.Range(-limpLimits.x, limpLimits.x),     
                                                                Random.Range(-limpLimits.y, limpLimits.y),
                                                                Random.Range(-limpLimits.z, limpLimits.z));
            allEnemy[i] = (GameObject)Instantiate(enemyPrefab, pos, Quaternion.identity);
            allEnemy[i].GetComponent<Herd>().herdsManager = this;           //now linking to Herd.cs for effective flocking
        }

        targetPos = this.transform.position;
    }

    private void Update()
    {
        if (Random.Range(0, 100) < 10)

            targetPos = this.transform.position + new Vector3(Random.Range(-limpLimits.x, limpLimits.x),
                                                                    Random.Range(-limpLimits.y, limpLimits.y),
                                                                    Random.Range(-limpLimits.z, limpLimits.z));
    }
}