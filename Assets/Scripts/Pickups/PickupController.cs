using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    [SerializeField] List<GameObject> totalPickups;
    int pickupsLeft;

    // Start is called before the first frame update
    void Start()
    {
        totalPickups.AddRange(GameObject.FindGameObjectsWithTag("Pickup"));

        pickupsLeft = totalPickups.Count;
    }

    public void PickupPickedUp()
    {
        pickupsLeft--;
        if(pickupsLeft <= 0)
        {

        }

        Debug.Log("Picked up item! Pickups left = " + pickupsLeft);
    }
}
