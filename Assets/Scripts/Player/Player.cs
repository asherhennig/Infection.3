using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //initialized current health and max health and other public vars
    public int curHealth = 5;
    public int maxHealth;
    public float speed = 10;
    public float rotSpeed = 1.0f;
    public float timeBetweenHits = 0;
    public LayerMask layerMask;
    public GameObject miniGun;
    public GameObject PlayerHitPrefab;
    public int currency;

    enemyBase enemy;

    public Animator heroAnim;
    Animator head;


    //so get componet can access the minigun
    public bool isDead = false;
    public HealthBar healthBar;

    //private init
    private CharacterController characterController;
    private Vector3 currentLookTarget = Vector3.zero;
    private bool isHit = false;
    private float timeSinceHit = 0;
    private GunEquipper gunEquipper;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        gunEquipper = GetComponent<GunEquipper>();
        healthBar.setMaxHealth(maxHealth);

        heroAnim = GetComponent<Animator>();
        head = enemy.GetComponent<Animator>();

        //currency = GetComponent<GameManager>().bubblegum;
    }

    //added takeDamage function
    public void takeDamage()
    {
        int healthDamage = 1;
        curHealth -= healthDamage;
        healthBar.setHealth(curHealth);
        Debug.Log("you've been hurt, health is: " + curHealth + " out of: " + maxHealth);
        if(curHealth <= 0)
        {
            isDead = true;
        }
    }

    //added max health up function
    public void maxUp()
    {
        curHealth = maxHealth;
        Debug.Log("health is: " + curHealth);
    }

    // added health pick up and it caps at max health
    public void pickUpHealth()
    {
        //pick up only gives one health
        curHealth += 1;
        if (curHealth > maxHealth)
        {
            //wont go over max health
            curHealth = maxHealth;
            Debug.Log("You're at max health!");
        }
        else
        {
            Debug.Log("Health Up! " + curHealth);
        }
    }

    public void pickUp1Curr()
    {
        currency += 1; 
    }

    public void picUp5Curr()
    {
        currency += 5;
    }

    public void pickUpMiniGun()
    {
        StartCoroutine("fireMiniGun");
    }
    //checks which pickup we got to know its effect
    public void PickUpItem(int pickupItem)
    {
        
        switch (pickupItem)
        {
            //uses constant class to define the variables and set it to case 1, 2 .....ect.

            //heals 1 health point
            case Constants.healthPickUp1:
                pickUpHealth();
                break;
            
            //heals full
            case Constants.HealthPickUpFull:
                maxUp();
                break;

            //add one bubble gum to inventory
            case Constants.bubbleGum1:
                pickUp1Curr();
                break;

            //add 5 bubble gum to inventory
            case Constants.bubbleGum5:
                picUp5Curr();
                break;

            //pick up the mini gun and start shooting
            case Constants.miniGunPickUp:
                pickUpMiniGun();
                break;

            default:
                //in case of bad pick up
                Debug.LogError("Bad pickup type passed" + pickupItem);
                break;
        }
    }

    

    // Update is called once per frame
    void Update()
    {
        //moves the player using the character controller
        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"),
                                            0, Input.GetAxis("Vertical"));
        characterController.SimpleMove(moveDirection * speed);

        if (moveDirection == Vector3.zero)
        {
            heroAnim.SetBool("IsMoving", false);        //Set Animator to not moving if character vector = 0
        }
        else
        {
            heroAnim.SetBool("IsMoving", true);
        }

        //gives the player some I frames after being hit, we can adjust how long
        if (isHit)
        {
            timeSinceHit += Time.deltaTime;
            if(timeSinceHit>timeBetweenHits)
            {
                isHit = false;
                timeSinceHit = 0;
            }
        }


        //if youre dead you die... lol
        if (isDead)
        {
            Die();
        }
    }
    void FixedUpdate()
    {
        ////Player direction controls
        //Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal"),
        //                                    0, Input.GetAxis("Vertical"));




        //else
        //{
        //    head.AddForce(transform.right * 150, ForceMode.Acceleration);            //head bobble functionality

        //    bodyAnimator.SetBool("IsMoving", true);         //Set Animator to moving if character vector != 0
        //}


        //this makes the palyer look at the cursor when its on screen
        //creating hit and ray variables
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //fires ray from camera and returns hit
        if (Physics.Raycast(ray, out hit, 1000, layerMask, QueryTriggerInteraction.Ignore))
        {

            if (hit.point != currentLookTarget)
            {
                //Debug.Log("player not rotating");
            }
        }

        //sets where we want to turn to
        Vector3 targetPositon = new Vector3(hit.point.x, transform.position.y, hit.point.z);
        //gives smooth roation
        Quaternion rotation = Quaternion.LookRotation(targetPositon - transform.position);
        //turns to cursor 
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotSpeed);
    }
    //detects if we get hit by an enemy
    private void OnTriggerEnter(Collider other)
    {
        //checks if its and enemy by seeing if it has the FollowFood script
        enemyBase enemy = other.gameObject.GetComponent<enemyBase>();

        if (enemy != null)
        {
            //checks if were not already hit
            if(!isHit)
            {
                Instantiate(PlayerHitPrefab, this.transform.position, Quaternion.identity);
                takeDamage();
            }
        }
    }

    private IEnumerator fireMiniGun()
    {
        
            //200 is the num of bulets fired when powered up
            for (int i = -0; i < 200; i++)
                
            {
               //minigun is checking if a minigun GO is there
               miniGun = GameObject.FindGameObjectWithTag("miniGun"); 



                //gets the fire bulet function from the mini gun in gun script and calls it
                miniGun.GetComponent<Gun>().fireBullet(); 

               //call againg in half a second
              yield return new WaitForSeconds(1 / 2);
            }
                

            //deactivate the mini gun and reactivate pistol
            gunEquipper.deactiveMiniGun();

        
    }
    //this is where eventually well do everything that happens when the player dies here
    public void Die()
    {
        Debug.Log("GameOver");
        Destroy(gameObject);
    }
}
