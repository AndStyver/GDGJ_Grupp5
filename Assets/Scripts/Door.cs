using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public AudioSource openDoorSound;
    public AudioSource closeDoorSound;
    public GameObject hinge;
    public bool isOpen = true;

    Animator doorAnimator;

    void Start()
    {
        doorAnimator = gameObject.GetComponent<Animator>();
        
        //closeDoor();
        //openDoor();
    }

    public void openDoor()
    {
        doorAnimator.SetTrigger("Open");
        isOpen = true;
        if (openDoorSound != null)
        {
            openDoorSound.Play();
        }
    }

    public void closeDoor()
    {
        doorAnimator.SetTrigger("Close");
        isOpen = false;
        if (closeDoorSound != null)
        {
            closeDoorSound.Play();
        }
    }
}
