using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    GameObject[] doors;
    bool[] doorIsOpen;
    bool aDoorIsOpen = true;
    [SerializeField] [Range(0.5f, 10)] float closeDoorTimer = 2f;

    private int roomCounter = 0;
    private bool isActive = false;

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

        //StartCoroutine(CloseRandomEverySeconds(closeDoorTimer));
    }

    void CloseFirstDoor(int position)
    {
        CloseDoor(position);
        StartCoroutine(CloseRandomEverySeconds(closeDoorTimer));
    }

    IEnumerator CloseDoorAfterSeconds(float time, int position)
    {
        int currentRoom = roomCounter;
        CloseDoor(position);
        yield return new WaitForSeconds(1);
        if (currentRoom == roomCounter && isActive)
            StartCoroutine(CloseRandomEverySeconds(time));
    }

    IEnumerator CloseRandomEverySeconds(float time)
    {
        int currentRoom = roomCounter;
        while (currentRoom == roomCounter)
        {
            yield return new WaitForSeconds(time);
            if (currentRoom == roomCounter && isActive)
                CloseRandomDoor();
        }
    }

    public void CloseRandomDoor()
    {
        if (!aDoorIsOpen)
        {
            Debug.Log("No Open Door was found");
            roomCounter++; //Cheap fix to break CloseRandomEverySeconds
            GameOverController gameOver = GameObject.Find("GameController").GetComponent<GameOverController>();
            gameOver.SpawnGhosts();

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

    public void CloseDoor(int position)
    {
        doors[position].GetComponent<Door>().closeDoor();
        doorIsOpen[position] = false;
    }

    public void ForceCloseDoor(int position)
    {
        CloseDoor(position);
        doors[position].GetComponent<Door>().SkipGhostAnimation();
    }

    public void ResetDoors(int lastEnteredDoor)
    {
        roomCounter++;
        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].GetComponent<Door>().SkipGhostAnimation();
            OpenDoor(i);
        }
        CloseFirstDoor(lastEnteredDoor);
        //StartCoroutine(CloseDoorAfterSeconds(2f, lastEnteredDoor));
    }

    void OpenDoor(int position)
    {
        aDoorIsOpen = true;
        doors[position].GetComponent<Door>().openDoor();
        doorIsOpen[position] = true;
    }

    public void Activate()
    {
        isActive = true;
        StartCoroutine(CloseRandomEverySeconds(closeDoorTimer));
    }

    public void Deactivate()
    {
        isActive = false;
    }

}