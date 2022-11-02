using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public AudioSource openDoorSound;
    public AudioSource closeDoorSound;
    public bool isOpen = true;

    Animator doorAnimator;

    void Start()
    {
        doorAnimator = gameObject.GetComponent<Animator>();
    }

    public void SkipGhostAnimation()
    {
        if(doorAnimator.GetCurrentAnimatorStateInfo(0).IsName("DoorGhost"))
            doorAnimator.SetTrigger("SkipGhost");
    }

    public void openDoor()
    {
        if (isOpen)
            return;

        doorAnimator.SetTrigger("Open");
        isOpen = true;
        if (openDoorSound != null)
        {
            openDoorSound.Play(0);
        }
    }

    public void closeDoor()
    {
        if (!isOpen)
            return;
        doorAnimator.SetTrigger("Close");
        isOpen = false;
        if (closeDoorSound != null)
        {
            closeDoorSound.Play(44100);
        }
    }
}
