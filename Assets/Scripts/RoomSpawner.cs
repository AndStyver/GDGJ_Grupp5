using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] GameObject pickup;
    //the array of all possible furniture to spawn, more than one meaning it can spawn twice in the same room
    [SerializeField] GameObject[] furniture; 
    //the list that the game chooses from, dont add stuff in it!
    [SerializeField] List<GameObject> furnitureToPickFrom;

    [Header("Furniture")]
    [SerializeField] GameObject furnitureSpawnPointsObject;
    [SerializeField] List<Vector2> furnitureSpawnPoints;
    [SerializeField] List<GameObject> spawnedFurniture;

    [Header("Variables")]
    [SerializeField] int pickupsToSpawn;
    [SerializeField] int furnitureToSpawn;
    Vector3 offset = new(0, 0, 10);
    [SerializeField] float spawnOffsetFromWall;
    [SerializeField] float spawnOffsetFromWallY;

    PickupController pickupController;
    GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        pickupController = GameObject.Find("GameController").GetComponent<PickupController>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();

        Camera cam = Camera.main;

        SpawnPickups(cam);
        SpawnFurniture();
    }

    private void Update()
    {
        //if (Input.GetMouseButtonDown(1))
        //{
        //    GenerateRoom();
        //}
    }

    public void GenerateRoom()
    {
        SpawnPickups(Camera.main);
        SpawnFurniture();

        pickupController.ResetCombo();

        gameController.NewRoom();
    }

    private void SpawnPickups(Camera cam)
    {
        GameObject[] pickupsToRemove = GameObject.FindGameObjectsWithTag("Pickup");
        for (int i = 0; i < pickupsToRemove.Length; i++) { Destroy(pickupsToRemove[i]); }

        //spawn objects
        for (int i = 0; i < pickupsToSpawn; i++)
        {
            //generate points for pickup
            Vector2 pickupSpawnVector = cam.ScreenToWorldPoint(new(Random.Range(spawnOffsetFromWall, cam.pixelWidth - spawnOffsetFromWall),
                Random.Range(spawnOffsetFromWallY, cam.pixelHeight - spawnOffsetFromWallY))) + offset;

            Instantiate(pickup, pickupSpawnVector, Quaternion.identity);
        }
    }

    private void SpawnFurniture()
    {
        //remove old furniture
        GameObject[] furnitureToDestroy = GameObject.FindGameObjectsWithTag("Furniture");
        for (int i = 0; i < furnitureToDestroy.Length; i++) { Destroy(furnitureToDestroy[i]); }

        furnitureSpawnPoints.Clear();
        furnitureToPickFrom.Clear();

        //add all potential spawnpoints
        for (int i = 0; i < furnitureSpawnPointsObject.transform.childCount + 1; i++)
        {
            furnitureSpawnPoints.Add(furnitureSpawnPointsObject.GetComponentsInChildren<Transform>()[i].position);
        }

        for (int i = 0; i < furniture.Length; i++)
        {
            furnitureToPickFrom.Add(furniture[i]);
        }

        for (int i = 0; i < furnitureToSpawn; i++)
        {
            //pick a point for the furniture to spawn on
            int positionToSpawn = Random.Range(0, furnitureSpawnPoints.Count);
            int furnitureToSpawn = Random.Range(0, furnitureToPickFrom.Count);

            //spawn the furniture at set point with a random rotation
            Instantiate(furnitureToPickFrom[furnitureToSpawn], furnitureSpawnPoints[positionToSpawn],
                Quaternion.Euler(new(0, 0, Random.Range(0f, 360f))));

            //remove that point from the list to not spawn twice at same location
            furnitureSpawnPoints.RemoveAt(positionToSpawn);
            furnitureToPickFrom.RemoveAt(furnitureToSpawn);
        }
    }
}
