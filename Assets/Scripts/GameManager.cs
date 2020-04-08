using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager singleton;
    //game objects that will be needed in the script
    public GameObject player;
    private Player player1;
    private Ammo ammo;
    private GunEquipper gunEquipper;
    public GameObject[] itemSpawnPoints;
    public GameObject[] enemySpawnPoints;
    public GameObject enemy;
    public GameObject[] pickUpPrefab;
    GameObject[] buyShotgun;
    GameObject[] buyShells;
    GameObject[] buyNade;
    GameObject[] buyHealth;
    GameObject[] buyMax;
    GameObject[] buyBrain;
    GameObject[] purchase;
    public ScoreCounter gameUI;
    public int score;
    public int bubblegum;
    private int price;
    private bool canPurchase = false;
    public bool isGameOver = false;

    //public vars so we can modify them as we need
    public int maxEnemiesOnScreen;
    public int enemiesPerSpawn;
    public int restTimer;
    public float minSpawnTime;
    public float maxSpawnTime;
    public float pickUpMaxSpawnTime = 20.0f;
    public int wave=0;

    //private data for keeping track of enemies on screen and time
    // between spawns of enemies and pick-ups
    private int enemiesOnScreen = 0;
    //these are for the spawning of pickups
    private bool spawnedPickUp = false;
    private float actualPickUpTime = 0;
    private float currentPickUpTime = 0;
    //number for a random pick up in array
    int pickUpNum;
    //these are for enemy spawns
    public int MaxPerWave = 5;
    private int curSpawnedWave = 0;
    GameObject pickUp;
    GameObject currency;
    //this lets us know if a wave is active
    public bool activeWave = true;
    //Difficulty
    public int curDifficulty = 1;
    public float difficultyMod = 1.0f;
    private int itemID;

    void Awake()
    {
        player1 = GameObject.FindObjectOfType<Player>();
        gunEquipper = GetComponent<GunEquipper>();
        ammo = GetComponent<Ammo>();
    }
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SaveSystem>().gameLoad();
        singleton = this;
        actualPickUpTime = Random.Range(pickUpMaxSpawnTime - 3.0f, pickUpMaxSpawnTime);
        actualPickUpTime = Mathf.Abs(actualPickUpTime);
        Time.timeScale = 1;
        restTimer = 0;
        StartCoroutine("updatedRestTimer");
        buyShotgun = GameObject.FindGameObjectsWithTag("BuyShotgun");
        buyShells = GameObject.FindGameObjectsWithTag("BuyShells");
        buyNade = GameObject.FindGameObjectsWithTag("BuyNade");
        buyHealth = GameObject.FindGameObjectsWithTag("BuyHealth");
        buyMax = GameObject.FindGameObjectsWithTag("BuyMax");
        buyBrain = GameObject.FindGameObjectsWithTag("BuyBrain");
        purchase = GameObject.FindGameObjectsWithTag("Purchase");
        HidePurchase();
    }

    // Update is called once per frame
    void Update()
    {
        if (wave == 2)
        {
            GetComponent<SaveSystem>().gameSave();  
        }
        StartCoroutine("updatedRestTimer");
        //updating pick up spawn time
        currentPickUpTime += Time.deltaTime;
        //checks if the current spawn time is more than the upgrade spawn time and that one isnt spawned
        if (pickUpPrefab.Length > 0)
        {
            if (currentPickUpTime > actualPickUpTime && !spawnedPickUp)
            {
                pickUpNum = Random.Range(0, 2);
                //generates a random number based on the number of spawn points we have and
                //assigns one to be the spawn, finally it spawns a pickup
                int randnum = Random.Range(0, itemSpawnPoints.Length - 1);
                GameObject spawnLocation = itemSpawnPoints[randnum];
                pickUp = Instantiate(pickUpPrefab[pickUpNum]) as GameObject;
                pickUp.transform.position = spawnLocation.transform.position;
                spawnedPickUp = true;
                actualPickUpTime = Random.Range(pickUpMaxSpawnTime - 3.0f, pickUpMaxSpawnTime);
                actualPickUpTime = Mathf.Abs(actualPickUpTime);
                Debug.Log("Spawned");
            }
            //checks if the pick up has been picked up
            if (pickUp == null && spawnedPickUp == true)
            {
                currentPickUpTime = 0;
                spawnedPickUp = false;
                Debug.Log("deactive");
            }
        }
        if (activeWave)
        {
            //checks if its time to spawn
            if (curSpawnedWave < MaxPerWave)
            {
               // Debug.Log("hi");
                if (enemiesPerSpawn > 0 && enemiesOnScreen < MaxPerWave)
                {
                    List<int> previousSpawnLocations = new List<int>();
                    if (enemiesPerSpawn > enemySpawnPoints.Length)
                    {
                        enemiesPerSpawn = enemySpawnPoints.Length - 1;
                    }

                    enemiesPerSpawn = (enemiesPerSpawn > MaxPerWave) ? enemiesPerSpawn - MaxPerWave : enemiesPerSpawn;
                    for (int i = 0; i < enemiesPerSpawn; i++)
                    {
                        if (curSpawnedWave < MaxPerWave + wave && enemiesOnScreen < maxEnemiesOnScreen)
                        {
                            Debug.Log("here");
                            enemiesOnScreen += 1;
                            int spawnPoint = -1;
                            while (spawnPoint == -1)
                            {
                                int randNum = Random.Range(0, enemySpawnPoints.Length - 1);
                                if (!previousSpawnLocations.Contains(randNum))
                                {
                                    previousSpawnLocations.Add(randNum);
                                    spawnPoint = randNum;
                                }
                            }
                            GameObject spawnLocation = enemySpawnPoints[spawnPoint];
                            GameObject newEnemy = Instantiate(enemy) as GameObject;
                            curSpawnedWave++;
                            Debug.Log("Enemy Spawned");
                            newEnemy.transform.position = spawnLocation.transform.position;
                            enemyBase enemyScript = newEnemy.GetComponent<enemyBase>();
                            newEnemy.GetComponent<enemyBase>().setDiff(difficultyMod);
                            if (player != null)
                            {
                                enemyScript.target = player.transform;
                                Vector3 targetRotation = new Vector3(player.transform.position.x,
                                       newEnemy.transform.position.y, player.transform.position.z);
                                newEnemy.transform.LookAt(targetRotation);
                            }
                            enemyScript.onDestroy.AddListener(enemyDestroyed);
                        }
                    }
                }

            }
            else if (curSpawnedWave == MaxPerWave && enemiesOnScreen == 0)
            {
                activeWave = false;
                restTimer = 10;
                curSpawnedWave = 0;
                Debug.Log("rest period");
                wave++;
                MaxPerWave++;
                maxEnemiesOnScreen++;
                score += 500;
             
            }
        }
    }

    private IEnumerator updatedRestTimer()
    {
        if(!activeWave)
        {
            Debug.Log("hello?");
            yield return new WaitForSeconds(10);
                activeWave = true;
            
        }
    }

    public void enemyDestroyed()
    {
        //int gumChance = Random.Range(0, 10);
        enemiesOnScreen -= 1;
        //give gum and score on kill(testing score and bubblegum counters)
        bubblegum += 5;
        //gameUI.SetMoneyText(bubblegum);
        score += 100;
        //gameUI.SetScoreText(score);
        
        //currency = Instantiate(bubbleGum[gumChance]) as GameObject;
        Debug.Log("enemy destroyed");
    }
    public void Prices()
    {
        foreach (GameObject g in buyShells)
        {
            price = 1000;
            itemID = 1;
            Debug.Log("Testing1");
        }
    }

    public void Prices1()
    {
        foreach (GameObject g in buyShells)
        {
            price = 200;
            itemID = 2;
            Debug.Log("Testing1");
        }
    }

    public void Prices2()
    {
        foreach (GameObject g in buyNade)
        {
            price = 3000;
            itemID = 3;
            Debug.Log("Testing2");
        }
    }
    public void Prices3()
    {
        foreach (GameObject g in buyHealth)
        {
            price = 1500;
            itemID = 4;
            Debug.Log("Testing3");
        }
       
    }

    public void Prices4()
    {
        foreach (GameObject g in buyMax)
        {
            price = 5000;
            itemID = 5;
            Debug.Log("Testing4");
        }
    }
    public void Prices5()
    {
        foreach (GameObject g in buyBrain)
        {
            price = 3500;
            itemID = 6;
            Debug.Log("Testing5");
        }
    }
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
        Debug.Log("purchase:" + purchase.Length);
    }

    public void Buyable()
    {

        if (bubblegum >= price)
        {
            canPurchase = true;
            bubblegum = bubblegum - price;
            gameUI.SetMoneyText(bubblegum);
            Debug.Log("Item Purchased");
            if (itemID == 1)
            {
                gunEquipper.shotgun.SetActive(true);
            }
            else if (itemID == 2)
            {
                ammo.shotgunAmmo = ammo.shotgunAmmo + 5;
            }
            else if (itemID == 3)
            {
                gunEquipper.fragGrenade.SetActive(true);
            }
            else if (itemID == 4)
            {
                player1.curHealth ++;
            }
            else if (itemID == 5)
            {
                player1.curHealth = player1.maxHealth;
            }
            else if (itemID == 6)
            {
                gunEquipper.lureGrenade.SetActive(true);
            }
        }
    }
 
    public float setDifficulty(int difficulty)
    {
        if(difficulty == 1)
        {
            difficultyMod = 0.5f;
        }
        else if (difficulty == 2)
        {
            difficultyMod = 1.0f;
        }
        else if (difficulty == 3)
        {
            difficultyMod = 2.0f;
        }
        return difficultyMod;
    }
}
