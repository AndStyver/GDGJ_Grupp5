using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public GameObject room;
    public Vector2 roomSize;
    GameObject[] rooms = new GameObject[4];
    Vector2[] roomPositions;
    CameraController cameraController;

    void Start()
    {
        cameraController = Camera.main.GetComponent<CameraController>();
        roomSize = room.GetComponent<SpriteRenderer>().size;
        // Top, Right, Down, Left
        Vector2[] temp = { 
            new Vector2(0, roomSize.y),
            new Vector2(roomSize.x, 0), 
            new Vector2(0, -roomSize.y), 
            new Vector2(-roomSize.x, 0) };
        roomPositions = temp;
        StartRooms();
    }

    public void StartRooms()
    {
        for (int i = 0; i < 3; i++)
        {
            AddRoom((Vector2)transform.position + roomPositions[i], i);
        }
        AddRoom(transform.position, 3);
    }

    public void MoveToNewRoom(int doorNumber)
    {
        Debug.Log("MoveToNewRoom: entered door " + doorNumber);
        int newDoorNumber = -1;
        switch (doorNumber)
        {
            case 0: //Top door
                newDoorNumber = 2;
                break;

            case 1: //Right door
                newDoorNumber = 3;
                break;

            case 2: //Down door
                newDoorNumber = 0;
                break;

            case 3: //Left door
                newDoorNumber = 1;
                break;
        }

        int numOfRooms = 4;

        //Remove old rooms
        for (int i = 0; i < numOfRooms; i++)
        {
            if(i != doorNumber)
                RemoveRoom(i);
        }
        //Save new room
        GameObject newRoom = rooms[doorNumber];
        //Clear old rooms
        rooms = new GameObject[numOfRooms];
        //Set current room as the room to closed door
        rooms[newDoorNumber] = newRoom;

        //Generate new rooms
        for (int i = 0; i < numOfRooms; i++)
        {
            if(i != newDoorNumber)
            {
                AddRoom((Vector2)rooms[newDoorNumber].transform.position + roomPositions[i], i);
            }
            else
            {
                Debug.Log("Couldn't add room at " + i + ", " + rooms[i]);
            }
        }
        //Move camera to new room
        Vector3 newCameraPos = rooms[newDoorNumber].transform.position;
        newCameraPos.z = -10;
        cameraController.moveToNextRoom(newCameraPos);
        //Close door behind you
        newRoom.GetComponentInChildren<DoorController>().CloseDoor(newDoorNumber);
        newRoom.GetComponentInChildren<DoorController>().ForceCloseDoor(newDoorNumber);
        //Activate new room
        ActivateRoom(rooms[newDoorNumber]);
    }

    void AddRoom(Vector2 position, int doorNumber)
    {
        GameObject newRoom = Instantiate(room, position, Quaternion.identity, transform);
        rooms[doorNumber] = newRoom;
        Debug.Log("Added room at doorNumber " + doorNumber + " at pos: " + position);
    }

    void RemoveRoom(int doorNumber)
    {
        if(rooms[doorNumber] != null)
        {
            Destroy(rooms[doorNumber], 1 + cameraController.transitionTime);
            Debug.Log("Deleted room at door number " + doorNumber);
        }
        else
        {
            Debug.Log("Could not find room at door number " + doorNumber);
        }
        //rooms[doorNumber] = null;
    }

    void ActivateRoom(GameObject room)
    {
        room.GetComponent<RoomSpawner>().GenerateRoom();
        room.GetComponentInChildren<DoorController>().Activate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
