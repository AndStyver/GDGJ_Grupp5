using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //Enemy Reference 
    Rigidbody2D enemyRb2d;

    GameOverController gameOverController;

    Transform player;
    SpriteRenderer enemyFlip;
    [SerializeField] float MoveSpeed = 20f; //Move speed of ghosts
    int MaxDist = 10; //Max distance that ghost can aggro onto player
    float MinDist = 0.1f; //Min distance that ghost will get to player 


    void Start()
    {
        //Find our Rigidbody2D
        enemyRb2d = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();
        gameOverController = GameObject.Find("GameController").GetComponent<GameOverController>();
        enemyFlip = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {

        if (Vector3.Distance(enemyRb2d.transform.position, player.position) >= MinDist)
        {
            Vector3 direction = (player.position - enemyRb2d.transform.position).normalized;
            enemyRb2d.transform.position += direction * MoveSpeed * Time.deltaTime;

            if (direction.x >= 0)
            {
                enemyFlip.flipX = true;
            }
            else
            {
                enemyFlip.flipX = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameOverController.EndGame(false);
        }
    }

}
