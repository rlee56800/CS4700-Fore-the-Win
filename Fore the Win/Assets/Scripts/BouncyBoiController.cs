using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyBoiController : Unit
{
    public float damage;
    private Rigidbody2D rigbod;

    // Start is called before the first frame update
    void Start()
    {
        rigbod = GetComponent<Rigidbody2D>();
        int direction = (Random.value > 0.5) ? 1 : -1;
        rigbod.AddForce(new Vector2(40 * speed * direction, -30 * speed)); // 200, -150

        StartCoroutine(SwapSprites());
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Hit");
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(2);
        }
    }
}
