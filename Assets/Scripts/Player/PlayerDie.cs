using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDie : MonoBehaviour
{

    public GameObject playerDeathPrefab;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Player>().isDead == true)
        {
            Instantiate(playerDeathPrefab, this.transform.position, Quaternion.identity);
        }

    }
}
