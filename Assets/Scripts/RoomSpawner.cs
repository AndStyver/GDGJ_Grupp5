using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] GameObject pickup;
    [SerializeField] GameObject furniture;

    [Header("Variables")]
    [SerializeField] int pickupsToSpawn;
    [SerializeField] int furnitureToSpawn;
    Vector3 offset = new(0, 0, 10);

    // Start is called before the first frame update
    void Start()
    {
        Camera cam = Camera.main;

        for (int i = 0; i < pickupsToSpawn; i++)
        {
            Instantiate(pickup, cam.ScreenToWorldPoint(new(Random.Range(0, cam.pixelWidth), Random.Range(0, cam.pixelHeight))) + offset
                , Quaternion.identity);
        }
        for (int i = 0; i < furnitureToSpawn; i++)
        {
            Instantiate(furniture, cam.ScreenToWorldPoint(new(Random.Range(0, cam.pixelWidth), Random.Range(0, cam.pixelHeight))) + offset
               , Quaternion.identity);
        }
    }
}
