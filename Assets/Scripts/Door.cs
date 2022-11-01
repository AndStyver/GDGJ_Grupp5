using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public AudioSource openDoorSound;
    public AudioSource closeDoorSound;
    public GameObject hinge;

    Animator doorAnimator;

    void Start()
    {
        doorAnimator = gameObject.GetComponent<Animator>();
        
        //closeDoor();
        //openDoor();
    }

    void openDoor()
    {
        doorAnimator.SetTrigger("Open");

        if (openDoorSound != null)
        {
            openDoorSound.Play();
        }
    }

    void closeDoor()
    {
        doorAnimator.SetTrigger("Close");

        if (closeDoorSound != null)
        {
            closeDoorSound.Play();
        }
    }
}
