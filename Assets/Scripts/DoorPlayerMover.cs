using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPlayerMover : MonoBehaviour
{
    GameObject player;
    public int doorNumber;

    public RoomSpawner roomSpawner;
    RoomController roomController;
    DoorController doorController;

    void Start()
    {
        doorController = GameObject.Find("DoorController").GetComponent<DoorController>();
        player = GameObject.FindGameObjectWithTag("Player");

        if (roomController == null)
        {
            roomController = GetComponentInParent<RoomController>();
        }

        if (roomSpawner == null)
        {
            roomSpawner = GetComponentInParent<RoomSpawner>();
        }
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Vector3 playerPos = player.transform.position;
            float offset = 4;
            switch (doorNumber)
            {
                case 0: //Top door
                    
                    player.transform.position = new (playerPos.x, playerPos.y + (roomController.roomSize.y/offset) + 1, playerPos.z);
                    roomController.MoveToNewRoom(doorNumber);
                    //roomSpawner.GenerateRoom();
                    //doorController.ResetDoors(2);

                    break;

                case 1: //Right door
                    player.transform.position = new(playerPos.x + (roomController.roomSize.x / offset) + 1, playerPos.y, playerPos.z);
                    roomController.MoveToNewRoom(doorNumber);
                    //roomSpawner.GenerateRoom();
                    //doorController.ResetDoors(3);
                    break;

                case 2: //Down door
                    player.transform.position = new(playerPos.x, playerPos.y + (-roomController.roomSize.y / offset) - 1, playerPos.z);
                    roomController.MoveToNewRoom(doorNumber);
                    //roomSpawner.GenerateRoom();
                    //doorController.ResetDoors(0);
                    break;

                case 3: //Left door
                    player.transform.position = new(playerPos.x + (-roomController.roomSize.x / offset) - 1, playerPos.y, playerPos.z);
                    roomController.MoveToNewRoom(doorNumber);
                    //roomSpawner.GenerateRoom();
                    //doorController.ResetDoors(1);
                    break;
            }
        }
    }

}
