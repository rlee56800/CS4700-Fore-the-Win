using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrwallerController : Unit
{
    public float damage;
    private Rigidbody2D rigbod;
    private bool onWallX = false;
    private bool onWallY = false;

    public GameObject warningShot;
    public GameObject projectile;
    private bool stillWarning = true;
    private bool stillShooting = true;

    public float firerate;
    public float last_shot;


    // Start is called before the first frame update
    void Start()
    {
        rigbod = GetComponent<Rigidbody2D>();
        rigbod.AddRelativeForce(new Vector2(40 * speed, 0));

        StartCoroutine(CrwAttack());
        last_shot = Time.time;

        StartCoroutine(SwapSprites());
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
       if(coll.collider.CompareTag("WallX") && !onWallX)
        {
            // when first touching top/bottom wall
            onWallX = true; // currently touching WallX
            onWallY = false; // no longer touching WallY
            Schmoove();
            
            //Debug.Log("schmooving top or bottom");
        } else if (coll.collider.CompareTag("WallY") && !onWallY)
        {
            // when first touching left/right wall
            onWallY = true; // currently toyching WallY
            onWallX = false; // no longer touching WallX
            Schmoove();

            //Debug.Log("schmooving left or right");
        }
    }

    void Schmoove()
    {
        transform.Rotate(0, 0, 90);
        rigbod.AddRelativeForce(new Vector2(0, 40 * speed));
    }

    IEnumerator CrwAttack()
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(1, 8));
            //Debug.Log("stop");
            rigbod.velocity = Vector3.zero;
            yield return new WaitForSeconds(0.5f);
            stillWarning = true;
            stillShooting = true;
            Fire(1, 3, 5, transform.forward);

            /*StartCoroutine(PauseWarning(3));
            while(stillWarning)
            {
                FireWarning(5, 5, transform.position);
            }
            StartCoroutine(PauseShot(3));
            while(stillShooting)
            {

            }*/

            rigbod.AddRelativeForce(new Vector2(40 * speed, 0));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("Player"))
        {
            coll.gameObject.GetComponent<PlayerController>().TakeDamage(2);
            Destroy(gameObject);
        }
    }

    public void FireWarning(float lifetime, float speed, Vector3 direction)
    {
        //if (last_shot + firerate > Time.time) return;
        GameObject wproj = Instantiate(warningShot, transform.position + direction.normalized * 0.5f, Quaternion.LookRotation(direction));
        // Lab 3 42:00
        last_shot = Time.time;
    }

    public void Fire(float damage, float lifetime, float speed, Vector3 direction)
    {
        GameObject proj = Instantiate(projectile, transform.position + direction.normalized * 0.5f, Quaternion.LookRotation(direction));
        // Lab 3 42:00
        var laser = proj.GetComponent<CrwLaserScript>();
        laser.damage = damage;
        laser.lifetime = lifetime;
        laser.speed = speed;
        laser.rotation = transform.localEulerAngles.z - 90;
        last_shot = Time.time;
    }

    IEnumerator PauseWarning(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        stillWarning = false;
    }

    IEnumerator PauseShot(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        stillShooting = false;
    }
}
