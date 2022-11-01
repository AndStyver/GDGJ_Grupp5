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

    [Header("Variables")]
    [SerializeField] int pickupsToSpawn;
    [SerializeField] int furnitureToSpawn;
    Vector3 offset = new(0, 0, 10);

    // Start is called before the first frame update
    void Start()
    {
        Camera cam = Camera.main;

        //spawn objects
        for (int i = 0; i < pickupsToSpawn; i++)
        {
            //generate points for pickup
            Vector2 pickupSpawnVector = cam.ScreenToWorldPoint(new(Random.Range(50, cam.pixelWidth - 50), Random.Range(50, cam.pixelHeight - 50))) + offset;

            Instantiate(pickup, pickupSpawnVector, Quaternion.identity);
        }

        SpawnFurniture();
    }

    private void SpawnFurniture()
    {
        for (int i = 0; i < furnitureSpawnPointsObject.transform.childCount + 1; i++)
        {
            furnitureSpawnPoints.Add(furnitureSpawnPointsObject.GetComponentsInChildren<Transform>()[i].position);
        }

        for (int i = 0; i < furnitureToSpawn; i++)
        {
            //generate points for furnitures
            //Vector2 furnitureSpawnVector = cam.ScreenToWorldPoint(new(Random.Range(50, cam.pixelWidth - 50), Random.Range(50, cam.pixelHeight - 50))) + offset;
            int positionToSpawn = Random.Range(0, furnitureSpawnPoints.Count);

            Instantiate(furniture[Random.Range(0, furniture.Length)], furnitureSpawnPoints[positionToSpawn], Quaternion.Euler(new(0, 0, Random.Range(0f, 360f))));
            furnitureSpawnPoints.RemoveAt(positionToSpawn);
        }
    }
}
