using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    bool isOpen = true;

    public AudioSource openDoorSound;
    public AudioSource closeDoorSound;
    public GameObject hinge;

    Animator doorAnimator;

    void Start()
    {
        doorAnimator = gameObject.GetComponent<Animator>();
    }

    void openDoor()
    {
        isOpen = true;
        doorAnimator.SetBool("isOpen", isOpen);
    }

    void closeDoor()
    {
        isOpen = false;
        doorAnimator.SetBool("isOpen", isOpen);
    }
}
