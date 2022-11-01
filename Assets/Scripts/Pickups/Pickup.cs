using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] PickupController pickupController;

    private void Start()
    {
        pickupController = GameObject.Find("PickupController").GetComponent<PickupController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            pickupController.PickupPickedUp();
            Destroy(this.gameObject);
        }
    }
}
