using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private AudioManager audioManager;
    public int type;
    // Start is called before the first frame update
    void Start()
    {
        //audioManager = AudioManager.instance;
        //if (audioManager == null)
        //{
        //    Debug.LogError("AudioManager not found!!!");
        //}
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.GetComponent<Player>() != null
            && collider.gameObject.tag == "Player")
        {
            collider.gameObject.GetComponent<Player>().PickUpItem(type);
            Destroy(gameObject);
            Debug.Log("destroyed");
            // Play sound
            audioManager.PlaySound("PickupSound");
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
