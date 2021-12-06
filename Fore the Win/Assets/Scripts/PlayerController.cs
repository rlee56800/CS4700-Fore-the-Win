using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public Rigidbody2D rb;
    public BoxCollider2D playerHitbox;          
    public Camera sceneCamera;
    public GunController gun;  
    public float moveSpeed;   
    public float playerHealth = 20; 
    public float maxHealth = 20;  

    private Vector2 moveDirection;
    private Vector2 mousePosition;

    // TODO monobehavior cant be set with new?
    public HealthBar healthBar = new HealthBar(); 

    public Sprite standard;
    public Sprite standard2;

    public Color flashColor;
    public Color regularColor;
    public float flashDelay;
    public float invincibilityDuration;
    public SpriteRenderer mySprite;
    public bool isInvincible;
    

    void Start()
    {
        //DontDestroyOnLoad (this.gameObject);

        playerHealth = maxHealth;
        healthBar.setMaxHealth((int)maxHealth);
        isInvincible = false;
        //Time.timeScale = 1;

        StartCoroutine(SwapSprites());
    }

    // called once per frame
    void Update()
    {
        ProcessInputs();
    }

    // called every fixed framerate frame
    void FixedUpdate()
    {
        Move();
    }

    // takes in user input
    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;
        mousePosition = sceneCamera.ScreenToWorldPoint(Input.mousePosition);
 
        if(Input.GetMouseButtonDown(0))
        {
            gun.Fire();
        }
    }

    // moves player based on inputs
    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x*moveSpeed, moveDirection.y*moveSpeed);
        Vector2 aimDirection = mousePosition - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = aimAngle;
    }

    // death and damage
    virtual public void OnDeath()
    {
        Destroy(gameObject);
        SceneManager.LoadScene(8);
    }
    public void TakeDamage(float damage)
    {
        if (isInvincible) return;

        playerHealth -= damage;
        healthBar.setHealth((int)playerHealth);
        if (playerHealth <= 0)
        {
            OnDeath();
        }
        StartCoroutine(IFrame());
    }

    public IEnumerator IFrame()
    {
        isInvincible = true;

        for (float i=0; i<invincibilityDuration; i+=flashDelay)
        {
            mySprite.color = flashColor;
            yield return new WaitForSeconds(flashDelay);
            mySprite.color = regularColor;
            yield return new WaitForSeconds(flashDelay);
        }

        isInvincible = false;
    }

    public IEnumerator SwapSprites()
    {
        while(playerHealth > 0)
        {
            yield return new WaitForSeconds(0.3f);
            this.gameObject.GetComponent<SpriteRenderer>().sprite = standard2;
            yield return new WaitForSeconds(0.3f);
            this.gameObject.GetComponent<SpriteRenderer>().sprite = standard;
        }
    }
}
