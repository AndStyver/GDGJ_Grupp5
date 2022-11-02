using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    GameObject[] doors;
    bool[] doorIsOpen;
    bool aDoorIsOpen = true;

    private int roomCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        int numOfDoors = gameObject.transform.childCount;
        doors = new GameObject[numOfDoors];
        doorIsOpen = new bool[numOfDoors];

        for (int i = 0; i < numOfDoors; i++)
        {
            doors[i] = gameObject.transform.GetChild(i).gameObject;
            doorIsOpen[i] = doors[i].GetComponent<Door>().isOpen;
        }

        Debug.Log(doors);
        StartCoroutine(closeDoorTimer(2f));
    }

    void Update()
    {
        
    }

    IEnumerator closeDoorTimer(float time)
    {
        int currentRoom = roomCounter;
        while (currentRoom == roomCounter)
        {
            yield return new WaitForSeconds(time);
            if(currentRoom == roomCounter)
                CloseRandomDoor();
        }
    }

    public void CloseRandomDoor()
    {
        if (!aDoorIsOpen)
        {
            Debug.Log("No Open Door was found");
            return;
        }
        
        aDoorIsOpen = false;

        foreach (bool open in doorIsOpen)
        {
            if (open.Equals(true))
            {
                aDoorIsOpen = true;
            }
        }

        int randInt = Random.Range(0, 4);

        if (doorIsOpen[randInt])
        {
            CloseDoor(randInt);
        }
        else
        {
            CloseRandomDoor();
        }
    }

    void CloseDoor(int position)
    {
        doors[position].GetComponent<Door>().closeDoor();
        doorIsOpen[position] = false;
    }

    public void ResetDoors()
    {
        roomCounter++;
        for (int i = 0; i < doors.Length; i++)
        {
            OpenDoor(i);
        }
        StartCoroutine(closeDoorTimer(2f));
    }

    void OpenDoor(int position)
    {
        aDoorIsOpen = true;
        doors[position].GetComponent<Door>().openDoor();
        doorIsOpen[position] = true;
    }
}
