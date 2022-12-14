using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] PickupController pickupController;

    [SerializeField] Sprite[] candySprites;
    SpriteRenderer spriteRend;
    public AudioSource pickupSound;

    private void Start()
    {
        pickupController = GameObject.Find("GameController").GetComponent<PickupController>();

        spriteRend = GetComponentInChildren<SpriteRenderer>();
        int spritePicker = Random.Range(0, candySprites.Length);
        spriteRend.sprite = candySprites[spritePicker];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (pickupController == null) { Start(); }
            else
                pickupController.AddScore(1.5f);

            if (pickupSound != null)
            {
                pickupSound.pitch = pickupSound.pitch + Random.Range(-0.2f, 0.2f);
                pickupSound.Play();
            }
            //Disable render and collider after pickup to allow sound to play
            gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
            gameObject.GetComponentInChildren<CircleCollider2D>().enabled = false;
            //Destroy the parent gameobject
            Destroy(gameObject.transform.parent.gameObject, 1);
        }
    }
}
