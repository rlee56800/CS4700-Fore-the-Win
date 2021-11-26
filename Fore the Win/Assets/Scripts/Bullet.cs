using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    // TODO add bullet effects? particle collider

    public Rigidbody2D rb;
    public GameObject impactEffect;

    void OnTriggerEnter2D(Collider2D other)
    {
        // checks what the bullet hit
        switch(other.gameObject.tag)
        {
            case "WallX":
                Destroy(gameObject);
                break;
            case "WallY":
                Destroy(gameObject);
                break;
            case "Enemy":
                other.gameObject.GetComponent<Unit>().TakeDamage(1); // this part damaghes te enemy -Rebecca
                Imapct();
                break;
        }
    }

    public void Imapct()
    {
        Instantiate(impactEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
