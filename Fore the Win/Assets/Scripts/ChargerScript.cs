using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerScript : Unit
{
    public float damage;
    private Rigidbody2D rigbod;

    private bool wallY = false;
    private bool whichWall = true; // true = heading for top, false = heading to bottom
    private bool isAttacking = false;
    private bool isAvailable = false;
    private float movementSpd;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rigbod = GetComponent<Rigidbody2D>();
        //movementSpd = -40 * speed;
        movementSpd = -1 * speed;
        rigbod.AddRelativeForce(new Vector2((-40 * speed), 0)); // go to left wall
        isAvailable = false;

        //StartCoroutine(ChAttack());
        StartCoroutine(SwapSprites());
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (isAttacking)
        {
            transform.Rotate(0, 0, 180);
            isAttacking = false;
            rigbod.AddRelativeForce(new Vector2(0, 40 * speed));
            //StartCoroutine(Chill());
            whichWall = !whichWall;
        }

        /*if (coll.collider.CompareTag("WallY") && !wallY)
        {
            // when it touches left wall, stop going left and start going up
            wallY = true;
            rigbod.AddRelativeForce(new Vector2(0, 40 * speed));
            isAvailable = true;
        } else if (coll.collider.CompareTag("WallX") && whichWall)
        {
            // after touching top wall, go down
            whichWall = false;
            rigbod.AddRelativeForce(new Vector2(0, -40 * speed));
            isAvailable = true;
        } else if (coll.collider.CompareTag("WallX") && !whichWall)
        {
            // after touching bottom wall, go up
            whichWall = true;
            rigbod.AddRelativeForce(new Vector2(0, 40 * speed));
            isAvailable = true;
        }*/

        if (coll.collider.CompareTag("WallY") && !wallY)
        {
            // when it touches left wall, stop going left and start going up
            wallY = true;
            rigbod.AddRelativeForce(new Vector2(0, 40 * speed));
            isAvailable = true;
        }
        else if (coll.collider.CompareTag("WallX"))
        {
            //transform.Rotate(new Vector3(180, 0, 0));
            //rigbod.velocity = Vector3.zero;
            movementSpd *= -1;
            //rigbod.AddRelativeForce(new Vector2(0, movementSpd)); // TODO HERE HERE HERE
            isAvailable = true;
        }
    }

    private void FixedUpdate()
    {
        if(isAvailable) rigbod.velocity = new Vector2(0, movementSpd);
    }

    IEnumerator ChAttack()
    {
        //Debug.Log("Target sighted");
        isAttacking = true;
        isAvailable = false;
        rigbod.velocity = Vector3.zero;
        yield return new WaitForSeconds(0.5f);
        rigbod.AddRelativeForce(new Vector2(60 * speed, 0));
    }

    /*IEnumerator Chill()
    {
        yield return new WaitForSeconds(Random.Range(5, 10));
        isAvailable = true;
    }*/
    // Update is called once per frame



    void Update()
    {
        if(!isAttacking && isAvailable && (Mathf.Ceil(player.transform.position.y) == Mathf.Ceil(transform.position.y)))
        {
            //Debug.Log("Target sighted");
            StartCoroutine(ChAttack());
        }
    }

    /*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Hit");
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(2);
        }
    }
    */
}
