using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] PickupController pickupController;

    [SerializeField] Sprite[] candySprites;
    SpriteRenderer spriteRend;

    private void Start()
    {
        pickupController = GameObject.Find("PickupController").GetComponent<PickupController>();

        spriteRend = GetComponentInChildren<SpriteRenderer>();
        int spritePicker = Random.Range(0,candySprites.Length);
        spriteRend.sprite = candySprites[spritePicker];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           // pickupController.AddScore(1.5f);
            Destroy(this.gameObject);
        }
    }
}
