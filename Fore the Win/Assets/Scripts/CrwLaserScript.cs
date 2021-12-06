using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrwLaserScript : MonoBehaviour
{
    public Rigidbody2D rigbod;
    public float speed;
    public float damage;
    public float lifetime;
    public float rotation;
    public bool canBreak = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Despawn());
        rigbod = GetComponent<Rigidbody2D>();
        //Debug.Log(rotation);
        transform.Rotate(0, 0, rotation);
        switch(rotation)
        {
            case 0:
                rigbod.AddRelativeForce(new Vector2(-60 * speed, 0));
                break;

            case 90:
                rigbod.AddRelativeForce(new Vector2(0, -60 * speed));
                break;

            case -90:
                rigbod.AddRelativeForce(new Vector2(0, 60 * speed));
                break;

            case 180:
                rigbod.AddRelativeForce(new Vector2(60 * speed, 0));
                break;
        }
        StartCoroutine(Breakable());
    }

    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }

    IEnumerator Breakable()
    {
        // the wall the Crwaller is on keeps breaking its laser
        // wait 1 second and allow the laser to break in contact with walls
        yield return new WaitForSeconds(1);
        canBreak = true;
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if(coll.collider.CompareTag("Player"))
        {
            Destroy(gameObject);
        } else if((coll.collider.CompareTag("WallX") || coll.collider.CompareTag("WallY")) && canBreak)
        {
            Destroy(gameObject);
        }
        /*
        /* checks what the bullet hit
        switch (other.gameObject.tag)
        {
            case "WallX":
            case "WallY":
                Destroy(gameObject);
                break;
            case "Player":
                other.gameObject.GetComponent<PlayerController>().TakeDamage(1);
                Destroy(gameObject);
                break;
        }*/
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            coll.gameObject.GetComponent<PlayerController>().TakeDamage(4);
            Destroy(gameObject);
        } 
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(new Vector3(0, 0, speed * Time.deltaTime), Space.Self);
    }
}
