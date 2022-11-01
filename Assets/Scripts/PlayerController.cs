 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Player Reference 
    Rigidbody2D rb2d;

    //Current movement
    Vector2 movement = new Vector2();

    //Player Movement speed
    float speed = 10;

    void Start()
    {
        //Find our Rigidbody2D
        rb2d = GetComponent<Rigidbody2D>();

        
    }

    void Update()
    {
        //get input from player
        float horInput = Input.GetAxisRaw("Horizontal");
        float verInput = Input.GetAxisRaw("Vertical");

        movement.x = horInput;
        movement.y = verInput;

        //Update our movement 
        rb2d.velocity = movement.normalized * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if ()
        //{

        //}
    }

}
