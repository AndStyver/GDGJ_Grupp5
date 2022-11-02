using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    GameObject[] doors;
    bool[] doorIsOpen;
    bool aDoorIsOpen = true;

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
        yield return new WaitForSeconds(time);
        CloseRandomDoor();
        StartCoroutine(closeDoorTimer(2f));
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
        GameObject door = doors[randInt];

        if (doorIsOpen[randInt])
        {
            CloseDoor(randInt);
            doorIsOpen[randInt] = false;
        }
        else
        {
            CloseRandomDoor();
        }
    }

    void CloseDoor(int position)
    {
        doors[position].GetComponent<Door>().closeDoor();
    }
}
