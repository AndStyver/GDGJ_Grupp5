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

    Collider2D doorCollider;

    void Start()
    {
        doorAnimator = gameObject.GetComponent<Animator>();
        doorCollider = GetComponentInChildren<Collider2D>();
        Debug.Log(doorCollider.name);

        if (isOpen) { doorCollider.isTrigger = true; }
    }

    public void SkipGhostAnimation()
    {
        if (doorAnimator.GetCurrentAnimatorStateInfo(0).IsName("DoorGhost"))
            doorAnimator.SetTrigger("SkipGhost");
        closeDoorSound.Stop();
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
        doorCollider.isTrigger = true;
    }

    public void closeDoor()
    {
        if (!isOpen)
            return;

        doorAnimator.SetTrigger("Close");
        isOpen = false;
        if (closeDoorSound != null)
        {
            closeDoorSound.PlayDelayed(1);
        }
    }

    public void EnableDoorCollider()
    {
        doorCollider.isTrigger = false;
    }
}
