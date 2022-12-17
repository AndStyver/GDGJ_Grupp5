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
    GameObject startRoom;

    private int prevRoomNumber = -1;

    void Start()
    {
        startRoom = transform.GetChild(0).gameObject;
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
        for (int i = 0; i < 4; i++)
        {
            AddRoom((Vector2)transform.position + roomPositions[i], i);
        }
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

        //Clear pickups from previous room
        if(prevRoomNumber != -1)
        {
            rooms[prevRoomNumber].GetComponent<RoomSpawner>().ClearPickups();
            Debug.Log("Cleared pickups in " + prevRoomNumber);
        }

        prevRoomNumber = newDoorNumber;
        
        //Remove old rooms
        for (int i = 0; i < numOfRooms; i++)
        {
            if(i != doorNumber)
                RemoveRoom(i);
        }
        //Remove starting room
        if(startRoom != null)
        {
            GameObject.Find("UITutorial").SetActive(false);
            Destroy(startRoom, 0.4f + cameraController.transitionTime);
            startRoom.GetComponent<RoomSpawner>().ClearPickups();
            Debug.Log("Deleted starting room");
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
        //Close door behind you after camera moves
        StartCoroutine(CloseDoorBehindYou(newRoom, newDoorNumber, cameraController.transitionTime));
        //Activate new room
        ActivateRoom(rooms[newDoorNumber]);
    }

    IEnumerator CloseDoorBehindYou(GameObject newRoom,int doorNumber , float timer)
    {
        yield return new WaitForSeconds(timer);
        newRoom.GetComponentInChildren<DoorController>().ForceCloseDoor(doorNumber);
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
            rooms[doorNumber].GetComponentInChildren<DoorController>().Deactivate();
            Destroy(rooms[doorNumber], 0.4f + cameraController.transitionTime);
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
