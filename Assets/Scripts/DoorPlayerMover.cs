using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPlayerMover : MonoBehaviour
{
    GameObject player;
    public int doorNumber;
    float timer = 0f;
    Vector3 startPosition;
    Vector3 endPosition;
    bool moveBetweenRooms = false;
    int newDoorNumber = -1;

    public RoomSpawner roomSpawner;
    RoomController roomController;
    DoorController doorController;
    CameraController cameraController;

    void Start()
    {
        doorController = GameObject.Find("DoorController").GetComponent<DoorController>();
        player = GameObject.FindGameObjectWithTag("Player");
        cameraController = Camera.main.GetComponent<CameraController>();
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
        if (moveBetweenRooms)
        {
            TransitionBetweenRoom(startPosition, endPosition);
        }
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
                    newDoorNumber = 2;
                    startPosition = playerPos;
                    endPosition = new(playerPos.x, playerPos.y + (roomController.roomSize.y / offset) + 1, playerPos.z);
                    //player.transform.position = new (playerPos.x, playerPos.y + (roomController.roomSize.y/offset) + 1, playerPos.z);
                    roomController.MoveToNewRoom(doorNumber);
                    TriggerTransitionBetweenRoom();
                    break;

                case 1: //Right door
                    newDoorNumber = 3;
                    startPosition = playerPos;
                    endPosition = new(playerPos.x + (roomController.roomSize.x / offset) + 1, playerPos.y, playerPos.z);
                    //player.transform.position = new(playerPos.x + (roomController.roomSize.x / offset) + 1, playerPos.y, playerPos.z);
                    roomController.MoveToNewRoom(doorNumber);
                    TriggerTransitionBetweenRoom();
                    break;

                case 2: //Down door
                    newDoorNumber = 0;
                    startPosition = playerPos;
                    endPosition = new(playerPos.x, playerPos.y + (-roomController.roomSize.y / offset) - 1, playerPos.z);
                    //player.transform.position = new(playerPos.x, playerPos.y + (-roomController.roomSize.y / offset) - 1, playerPos.z);
                    roomController.MoveToNewRoom(doorNumber);
                    TriggerTransitionBetweenRoom();
                    break;

                case 3: //Left door
                    newDoorNumber = 1;
                    startPosition = playerPos;
                    endPosition = new(playerPos.x + (-roomController.roomSize.x / offset) - 1, playerPos.y, playerPos.z);
                    //player.transform.position = new(playerPos.x + (-roomController.roomSize.x / offset) - 1, playerPos.y, playerPos.z);
                    roomController.MoveToNewRoom(doorNumber);
                    TriggerTransitionBetweenRoom();
                    break;
            }
        }
    }

    void TriggerTransitionBetweenRoom()
    {
        startPosition = player.transform.position;
        player.GetComponent<CircleCollider2D>().enabled = false;
        player.GetComponent<PlayerController>().allowInputs = false;
        foreach (SpriteRenderer sr in player.GetComponentsInChildren<SpriteRenderer>())
        {
            sr.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        }
        moveBetweenRooms = true;
    }

    void TransitionBetweenRoom(Vector3 startPosition, Vector3 endPosition)
    {
        player.transform.position = Vector3.Lerp(startPosition, endPosition, timer);
        timer += Time.deltaTime/ cameraController.transitionTime;

        if(timer > 1)
        {
            moveBetweenRooms = false;
            player.GetComponent<CircleCollider2D>().enabled = true;
            player.GetComponent<PlayerController>().allowInputs = true;
            foreach (SpriteRenderer sr in player.GetComponentsInChildren<SpriteRenderer>())
            {
                sr.maskInteraction = SpriteMaskInteraction.None;
            }
            if(doorController != null)
                doorController.ForceCloseDoor(newDoorNumber);
        }
    }

}
