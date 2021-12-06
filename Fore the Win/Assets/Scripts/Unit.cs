using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    // note gravity was y = -9.81; is now y = 0
    public float health = 5;
    public float maxHealth = 5; // override later
    public float speed = 5;
    // lab3 48:00
    public float playerDmg = 1;
    public EnemySpawnScript spawner;// = new EnemySpawnScript();
    public bool isHit = false;
    public float dmgDealt = 1;

    public HealthBar healthBar;
    public Sprite deathSprite;
    public Sprite standard;
    public Sprite standard2;

    private bool canHurt = true;

    // Start is called before the first frame update
    void Start()
    {
        //GameObject player = GameObject.FindGameObjectWithTag("Player");
        //Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());

        //canMove = true;
        //StartCoroutine(SwapSprites());

        //spawner = new EnemySpawnScript();
        //spawner = FindObjectOfType<EnemySpawnScript>();
        healthBar = new HealthBar();
        health = maxHealth;
        healthBar.setMaxHealth((int)maxHealth);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("Hit");
        if(collision.gameObject.CompareTag("Player") && canHurt)
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(dmgDealt);
        }
    }

    public IEnumerator SwapSprites()
    {
        while(health > 0)
        {
            yield return new WaitForSeconds(0.5f);
            this.gameObject.GetComponent<SpriteRenderer>().sprite = (health > 0) ? standard2 : deathSprite;
            yield return new WaitForSeconds(0.5f);
            this.gameObject.GetComponent<SpriteRenderer>().sprite = (health > 0) ? standard : deathSprite;
        }
    }

    virtual public void OnDeath()
    {
        Destroy(gameObject);
        spawner.DecrementLiving();
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.setHealth((int)health);
        //Debug.Log("I've been shot");
        if (health <= 0 && !isHit)
        {
            isHit = true;
            canHurt = false;
            Debug.Log(canHurt);
            this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            this.gameObject.GetComponent<SpriteRenderer>().sprite = deathSprite;
            Invoke("OnDeath", 1);
        }
    }

    public float GetDamage()
    {
        return 1;
    }
}
