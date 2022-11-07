using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupMagnet : MonoBehaviour
{
    [SerializeField][Range(0.5f, 5)] float magnetForce = 1f;
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Rigidbody2D rb2d = this.GetComponent<Rigidbody2D>();
            rb2d.AddForce((other.transform.position - this.transform.position) * magnetForce, ForceMode2D.Impulse);
        }
    }
}
