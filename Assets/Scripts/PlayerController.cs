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
    [SerializeField][Range(1, 10)]float speed = 10;

    Animator playerAnimator;
    public bool allowInputs = true;

    void Start()
    {
        //Find our Rigidbody2D 
        rb2d = GetComponent<Rigidbody2D>(); 

        playerAnimator = gameObject.GetComponent<Animator>();

        StartCoroutine(Blink(2f));
        
    }

    void Update()
    {
        if (allowInputs)
        {
            //get input from player
            float horInput = Input.GetAxisRaw("Horizontal");
            float verInput = Input.GetAxisRaw("Vertical");

            bool rightAnim = horInput > 0;
            bool leftAnim = horInput < 0;
            bool upAnim = verInput > 0;
            bool downAnim = verInput < 0;
            playerAnimator.SetBool("Right", rightAnim);
            playerAnimator.SetBool("Left", leftAnim);
            playerAnimator.SetBool("Up", upAnim);
            playerAnimator.SetBool("Down", downAnim);

            movement.x = horInput;
            movement.y = verInput;

            //Update our movement 
            rb2d.velocity = movement.normalized * speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if ()
        //{

        //}
    }

    IEnumerator Blink(float time)
    {
        while (true)
        {
            yield return new WaitForSeconds(time);
            playerAnimator.SetTrigger("Blink");
        }
    }

}
