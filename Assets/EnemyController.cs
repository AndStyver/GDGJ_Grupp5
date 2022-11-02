using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //Enemy Reference 
    Rigidbody2D enemyRb2d;

  

  

    

    public Transform player;
    int MoveSpeed = 8;
    int MaxDist = 10000;
    float MinDist = 1f;


    void Start()
    {
        //Find our Rigidbody2D
        enemyRb2d = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        transform.LookAt(player);
        if (Vector3.Distance(enemyRb2d.transform.position, player.position) >= MinDist)
        {

            enemyRb2d.transform.position += transform.forward * MoveSpeed * Time.deltaTime;

            if (Vector3.Distance(transform.position, player.position) <= MaxDist)
            {
                
            }


        }
    }
}
