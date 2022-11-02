using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] GameObject pickup;
    [SerializeField] GameObject[] furniture;

    [Header("Furniture")]
    [SerializeField] GameObject furnitureSpawnPointsObject;
    [SerializeField] List<Vector2> furnitureSpawnPoints;
    [SerializeField] List<GameObject> spawnedFurniture;

    [Header("Variables")]
    [SerializeField] int pickupsToSpawn;
    [SerializeField] int furnitureToSpawn;
    Vector3 offset = new(0, 0, 10);

    PickupController pickupController;

    // Start is called before the first frame update
    void Start()
    {
        pickupController = GameObject.Find("PickupController").GetComponent<PickupController>();

        Camera cam = Camera.main;

        SpawnPickups(cam);
        SpawnFurniture();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GenerateRoom();
        }
    }

    public void GenerateRoom()
    {
        SpawnPickups(Camera.main);
        SpawnFurniture();

        pickupController.ResetCombo();
    }

    private void SpawnPickups(Camera cam)
    {
        GameObject[] pickupsToRemove = GameObject.FindGameObjectsWithTag("Pickup");
        for (int i = 0; i < pickupsToRemove.Length; i++) { Destroy(pickupsToRemove[i]); }

        //spawn objects
        for (int i = 0; i < pickupsToSpawn; i++)
        {
            //generate points for pickup
            Vector2 pickupSpawnVector = cam.ScreenToWorldPoint(new(Random.Range(50, cam.pixelWidth - 50), Random.Range(50, cam.pixelHeight - 50))) + offset;

            Instantiate(pickup, pickupSpawnVector, Quaternion.identity);
        }
    }

    private void SpawnFurniture()
    {
        //remove old furniture
        GameObject[] furnitureToDestroy = GameObject.FindGameObjectsWithTag("Furniture");
        for (int i = 0; i < furnitureToDestroy.Length; i++) { Destroy(furnitureToDestroy[i]); }

        furnitureSpawnPoints.Clear();

        //add all potential spawnpoints
        for (int i = 0; i < furnitureSpawnPointsObject.transform.childCount + 1; i++)
        {
            furnitureSpawnPoints.Add(furnitureSpawnPointsObject.GetComponentsInChildren<Transform>()[i].position);
        }

        for (int i = 0; i < furnitureToSpawn; i++)
        {
            //pick a point for the furniture to spawn on
            int positionToSpawn = Random.Range(0, furnitureSpawnPoints.Count);

            //spawn the furniture at set point with a random rotation
            Instantiate(furniture[Random.Range(0, furniture.Length)], furnitureSpawnPoints[positionToSpawn],
                Quaternion.Euler(new(0, 0, Random.Range(0f, 360f))));

            //remove that point from the list to not spawn twice at same location
            furnitureSpawnPoints.RemoveAt(positionToSpawn);
        }
    }
}
