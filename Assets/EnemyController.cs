using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //Enemy Reference 
    Rigidbody2D enemyRb2d;

    //Distance between enemy and player
    float distance;

    //Current movement
    Vector2 movement = new Vector2();

    //Player Movement speed
    float speed = 10;

    public PlayerController playerController;


    void Start()
    {
        //Find our Rigidbody2D
        enemyRb2d = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        distance = Vector3.Distance(playerController.transform.position, enemyRb2d.transform.position);
        Debug.Log("");
    }
}
