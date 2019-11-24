using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    
    //Movements
    Rigidbody2D body;
    
    Vector2 direction;
    
    [Header("Jump")]
    [SerializeField] float jumpVelocity = 6;
    [SerializeField] float raycastJumpLength = 1f;

    [SerializeField] float timeStopJump = 0.1f;
    float timerStopJump = 0;
    [SerializeField] float jumpFallingModifier = 1;
    bool canJump = true;
    
    [Header("Horizontal deplacement")]
    [SerializeField]
    float speed = 4;
    [SerializeField]
    float maxSpeed = 10;

    //Health
    PlayerHealth playerHealth;

    //Gun
    [Header("Gun")] 
    [SerializeField] GameObject prefabBullet;
    [SerializeField] Transform bulletSpawnPoint;
    [SerializeField] float bulletSpeed = 10;

    //Animation
    [Header("Animation")] 
    Animator animator_;

    SpriteRenderer spriteRenderer_;
    bool isLookingRight = true;
    
    //Inventory
    [SerializeField] int money;
    [SerializeField] TextMeshProUGUI textMoney;
    
    void Start() {
        body = GetComponent<Rigidbody2D>();
        spriteRenderer_ = GetComponentInChildren<SpriteRenderer>();
        playerHealth = GetComponent<PlayerHealth>();
        animator_ = GetComponentInChildren<Animator>();
    }

    void FixedUpdate() {
        body.velocity = direction;

        //If player is falling => Multiply falling speed
        if (body.velocity.y < 0) {
            body.velocity = new Vector2(body.velocity.x, body.velocity.y * jumpFallingModifier);
        }

        //Set velocity to body and clamp it to don't fall too fast
        body.velocity = Vector2.ClampMagnitude(body.velocity, maxSpeed);
    }

    void Update() {
        //Function to attack itself
        if (Input.GetKeyDown(KeyCode.Q)) {
            playerHealth.TakeDamage(1);
        }
        
        //Movement
        UpdateMovement();
        
        //Gun
        UpdateGun();

        //Animation
        UpdateAnimation();
    }

    void UpdateMovement() {
        //Horizontal Movement
        direction = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);

        //Jump
        CheckJump();
    }

    void UpdateAnimation() {
        animator_.SetFloat("speed", Mathf.Abs(body.velocity.x));

        if (body.velocity.x < 0.1f && isLookingRight) {
            spriteRenderer_.flipX = true;
            isLookingRight = false;
        } else if (body.velocity.x > 0.1f && !isLookingRight) {
            spriteRenderer_.flipX = false;
            isLookingRight = true;
        }

        animator_.SetBool("isFalling", Mathf.Abs(body.velocity.y) > 0.1f);
    }

    void UpdateGun() {
        if (!(Input.GetAxis("Fire1") > 0.1f)) return;
        
        GameObject bullet = Instantiate(prefabBullet, bulletSpawnPoint);
        bullet.transform.parent = null;
            
        bullet.GetComponent<Rigidbody2D>().velocity = Vector2.right * bulletSpeed;
    }
    
    void CheckJump() {
        //Reduce timer
        timerStopJump -= Time.deltaTime;
        
        //Check if grounded only if timer is over, if not over then it's useless to go further 
        if (timerStopJump > 0) {
            return;
        }
    
        //Make a raycast under the player
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, raycastJumpLength, 1 << LayerMask.NameToLayer("Ground"));
            canJump = hit.rigidbody != null;
        
        //Check if the input is pressed
        if (!(Input.GetAxis("Jump") > 0.1f) || !canJump) return;
        
        animator_.SetTrigger("jump");
        direction.y += jumpVelocity;
        canJump = false;
        timerStopJump = timeStopJump;
    }

    public void AddMoney(int value) {
        //Change money
        money += value;

        //Update money Text
        textMoney.text = money.ToString();
    }

    public void TakeDamage(int value) {
        playerHealth.TakeDamage(value);
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine((Vector2)transform.position, (Vector2)transform.position + Vector2.down * raycastJumpLength);
        
        Gizmos.DrawWireCube((Vector2) transform.position + Vector2.down * 0.5f, new Vector2(1f, 0.2f));
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(bulletSpawnPoint.position, 0.3f);
    }
}
