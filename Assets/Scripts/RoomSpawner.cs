using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField] GameObject pickup;
    //The spawn area fr pickups
    [SerializeField] GameObject pickupSpawnArea;
    //Holds all pickups as children
    [SerializeField] GameObject pickupHolder;
    //All possible room layouts
    [SerializeField] GameObject[] rooms;
    //the array of all possible furniture to spawn, more than one meaning it can spawn twice in the same room
    [SerializeField] GameObject[] furniture; 
    //the list that the game chooses from, dont add stuff in it!
    [SerializeField] List<GameObject> furnitureToPickFrom;

    [Header("Furniture")]
    [SerializeField] GameObject furnitureSpawnPointsObject;
    [SerializeField] GameObject furnitureHolder;
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

        //disable spawn area indicator
        pickupSpawnArea.GetComponent<SpriteRenderer>().enabled = false;

        //SpawnPickups(cam);
        NewSpawnPickups();
        SpawnFurniture();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            GenerateRoom();
        }
    }

    public void GenerateRoom()
    {
        //SpawnPickups(Camera.main);
        NewSpawnPickups();
        SpawnFurniture();

        if(pickupController != null)
            pickupController.ResetCombo();

        ChangeRoomGraphics();
        if(gameController != null)
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

    private void NewSpawnPickups()
    {
        ClearPickups();

        for (int i = 0; i < pickupsToSpawn; i++)
        {
            //generate points for pickup
            Vector3 roomSize = pickupSpawnArea.transform.localScale;
            //Debug.Log(roomSize);

            Vector2 pickupSpawnVector = transform.position + 
                new Vector3(Random.Range(-roomSize.x / 2, roomSize.x / 2), Random.Range(-roomSize.y / 2, roomSize.y / 2));

            //Spawn pickup under pickupHolder
            GameObject newPickup = Instantiate(pickup, pickupSpawnVector, Quaternion.identity, pickupHolder.transform);
        }
    }


    public void ClearPickups()
    {
        //Destroy all leftover pickups
        for (int i = 0; i < pickupHolder.transform.childCount; i++)
        {
            Destroy(pickupHolder.transform.GetChild(i).gameObject);
        }
    }

    private void SpawnFurniture()
    {
        //remove old furniture
        //GameObject[] furnitureToDestroy = GameObject.FindGameObjectsWithTag("Furniture");
        //for (int i = 0; i < furnitureToDestroy.Length; i++) { Destroy(furnitureToDestroy[i]); }

        //remove old furniture
        for (int i = 0; i < furnitureHolder.transform.childCount; i++)
        {
            Destroy(furnitureHolder.transform.GetChild(i).gameObject);
        }

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
            //Instantiate(furnitureToPickFrom[furnitureToSpawn], furnitureSpawnPoints[positionToSpawn],Quaternion.Euler(new(0, 0, Random.Range(0f, 360f))));
            Instantiate(furnitureToPickFrom[furnitureToSpawn], furnitureSpawnPoints[positionToSpawn],
                Quaternion.Euler(new(0, 0, Random.Range(0f, 360f))), furnitureHolder.transform);

            //remove that point from the list to not spawn twice at same location
            furnitureSpawnPoints.RemoveAt(positionToSpawn);
            furnitureToPickFrom.RemoveAt(furnitureToSpawn);
        }
    }


    private void ChangeRoomGraphics()
    {
        SpriteRenderer newRoom = rooms[Random.Range(0, rooms.Length)].GetComponent<SpriteRenderer>();
        newRoom.flipX = (0 ==Random.Range(0, 2));
        GetComponent<SpriteRenderer>().sprite = newRoom.sprite;

    }
}
