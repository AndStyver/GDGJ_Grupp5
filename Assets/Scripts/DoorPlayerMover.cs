using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPlayerMover : MonoBehaviour
{
    GameObject player;
    public int doorNumber;

    public RoomSpawner roomSpawner;

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
                roomSpawner.GenerateRoom();


                break;

            case 1:
                player.transform.position = new Vector3(-5.14f, -0.11f, 0);
                roomSpawner.GenerateRoom();
                break;

            case 2:
                player.transform.position = new Vector3(-0.01f, 3.49f, 0);
                roomSpawner.GenerateRoom();
                break;

            case 3:
                player.transform.position = new Vector3(5.46f, 0.14f, 0);
                roomSpawner.GenerateRoom();
                break;
        }
    }

}
