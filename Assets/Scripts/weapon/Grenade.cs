using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public GameObject grenade;
    public Transform tossPos;
    public Vector3 throwPos;
    public float throwSpeed = 0.1f;
    public float TOF = 1000.0f;
    //bool tossed = false;
    public LayerMask LayerMask;
   // Vector3 direction;
   // GameObject tossedGrenade;
    public Ammo ammo;
    float gravity = 60f;
    public MeshRenderer inHand;
    //


    // Start is called before the first frame update
    void Start()
    {
    }

    IEnumerator myCoroutine()
    {
       // Vector3 direction = new Vector3();

        ammo.ConsumeAmmo(tag);
        //spawns grenade we want thrown
        GameObject tossedGrenade = Instantiate(grenade) as GameObject;
        //spawns it at set throw point
        tossedGrenade.transform.position = tossPos.position;
        tossedGrenade.transform.rotation = tossPos.rotation;
        //check where the player clicked
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            //set were the grenade should go and send it there
            throwPos = hit.point;

           // direction = (throwPos - tossPos.position).normalized;

            //  Debug.Log(direction);
           // tossedGrenade.GetComponent<Rigidbody>().MovePosition(transform.position + direction * TOF * Time.deltaTime);
            //tossedGrenade.GetComponent<Rigidbody>().AddForce(Vector3.MoveTowards(transform.position, throwPos, TOF * Time.deltaTime));
        }

        tossedGrenade.transform.position = tossPos.position;
        float arcAmount = 2f;
        float heightOfShot = 6f;
        Vector3 newVel = new Vector3();
        // Find the direction vector without the y-component
        Vector3 direction = new Vector3(throwPos.x, 0f, throwPos.z) - new Vector3(tossPos.position.x, 0f, tossPos.position.z);
        // Find the distance between the two points (without the y-component)
        float range = direction.magnitude;

        // Find unit direction of motion without the y component
        Vector3 unitDirection = direction.normalized;
        // Find the max height

        float maxYPos = tossPos.position.y + heightOfShot;

        // if it has, switch the height to match a 45 degree launch angle
        if (range / 2f > maxYPos)
            maxYPos = range / arcAmount;

        // find the initial velocity in y direction
        newVel.y = Mathf.Sqrt(-2.0f * -gravity * (maxYPos - tossPos.position.y));
        // find the total time by adding up the parts of the trajectory
        // time to reach the max
        float timeToMax = Mathf.Sqrt(-2.0f * (maxYPos - tossPos.position.y) / -gravity);
        // time to return to y-targe
        float timeToTargetY = Mathf.Sqrt(-2.0f * (maxYPos - throwPos.y) / -gravity);
        // add them up to find the total flight time
        float totalFlightTime = timeToMax + timeToTargetY;
        // find the magnitude of the initial velocity in the xz direction
        float horizontalVelocityMagnitude = range / totalFlightTime;
        // use the unit direction to find the x and z components of initial velocity
        newVel.x = horizontalVelocityMagnitude * unitDirection.x;
        newVel.z = horizontalVelocityMagnitude * unitDirection.z;

        float elapse_time = 0;
        while (elapse_time < totalFlightTime)
        {
            tossedGrenade.transform.Translate(newVel.x * Time.deltaTime, (newVel.y - (gravity * elapse_time)) * Time.deltaTime, newVel.z * Time.deltaTime);
            elapse_time += Time.deltaTime;
            yield return null;
        }
        /*  Vector3 direction = new Vector3();

          ammo.ConsumeAmmo(tag);
          //spawns grenade we want thrown
          GameObject tossedGrenade = Instantiate(grenade) as GameObject;
          //spawns it at set throw point
          tossedGrenade.transform.position = tossPos.position;
          tossedGrenade.transform.rotation = tossPos.rotation;
          //check where the player clicked
          RaycastHit hit;
          Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
          if (Physics.Raycast(ray, out hit))
          {
              //set were the grenade should go and send it there
              throwPos = hit.point;

              direction = (throwPos - tossPos.position).normalized;

              //  Debug.Log(direction);
              tossedGrenade.GetComponent<Rigidbody>().MovePosition(transform.position + direction * TOF * Time.deltaTime);
              //tossedGrenade.GetComponent<Rigidbody>().AddForce(Vector3.MoveTowards(transform.position, throwPos, TOF * Time.deltaTime));
          }

          //updating loop
          while(tossedGrenade != null)
          {
              Debug.Log(direction);
              Debug.Log(direction * TOF);
              //  Debug.Log(Time.deltaTime);
              // Debug.Log(TOF);
              tossedGrenade.GetComponent<Rigidbody>().MovePosition(tossedGrenade.transform.position + direction * TOF * Time.deltaTime);
              yield return new WaitForFixedUpdate();
          }*/
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        if(ammo.HasAmmo(tag))
        {
            inHand.enabled = true;
            throwGrenade();
        }
        else
        {
            inHand.enabled = false;
        }
    }
    void throwGrenade()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!IsInvoking("toss"))
            {
                Invoke("toss", 0f);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            CancelInvoke("toss");
        }
    }

    //throws grenade
    void toss()
    {
        /*ammo.ConsumeAmmo(tag);
        //spawns grenade we want thrown
        tossedGrenade = Instantiate(grenade) as GameObject;
        //spawns it at set throw point
        tossedGrenade.transform.position = tossPos.position;
        tossedGrenade.transform.rotation = tossPos.rotation;
        //check where the player clicked
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit))
        {
            //set were the grenade should go and send it there
            throwPos = hit.point;

            direction = (throwPos - tossPos.position).normalized;

          //  Debug.Log(direction);
            tossedGrenade.GetComponent<Rigidbody>().MovePosition(transform.position + direction * TOF * Time.deltaTime);

            tossed = true;
            //tossedGrenade.GetComponent<Rigidbody>().AddForce(Vector3.MoveTowards(transform.position, throwPos, TOF * Time.deltaTime));
        }*/
        StartCoroutine(myCoroutine());
    }
}
