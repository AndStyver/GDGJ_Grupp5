using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    GameObject[] doors;
    bool[] doorIsOpen;
    bool aDoorIsOpen = true;
    [SerializeField] [Range(0.5f, 3)] float closeDoorTimer = 2f;

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

        StartCoroutine(CloseRandomEverySeconds(closeDoorTimer));
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
        if (currentRoom == roomCounter)
            StartCoroutine(CloseRandomEverySeconds(time));
    }

    IEnumerator CloseRandomEverySeconds(float time)
    {
        int currentRoom = roomCounter;
        while (currentRoom == roomCounter)
        {
            yield return new WaitForSeconds(time);
            if (currentRoom == roomCounter)
                CloseRandomDoor();
        }
    }

    public void CloseRandomDoor()
    {
        if (!aDoorIsOpen)
        {
            Debug.Log("No Open Door was found");

            GameOverController gameOver = GameObject.Find("GameController").GetComponent<GameOverController>();
            gameOver.EndGame(false);

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

    public void ResetDoors(int lastEnteredDoor)
    {
        doors[lastEnteredDoor].GetComponent<Door>().SkipGhostAnimation();
        roomCounter++;
        for (int i = 0; i < doors.Length; i++)
        {
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
}
