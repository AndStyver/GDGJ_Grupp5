using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPlayerMover : MonoBehaviour
{
    GameObject player;
    public int doorNumber;
    void Start()
    {
       player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (doorNumber)
        {
            case 0:
                player.transform.position = new Vector3(0.02f, -3.11f, 0);
               
                break;

            case 1:

                break;

            case 2:

                break;

            case 3:

                break;
        }
    }

}
