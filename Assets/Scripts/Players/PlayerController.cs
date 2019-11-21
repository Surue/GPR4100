using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    private Rigidbody2D body;
    
    Vector2 direction;
    
    [SerializeField]
    private float speed = 4;
    [SerializeField]
    private float maxSpeed = 10;

    PlayerHealth playerHealth;

   
    bool canJump = true;
    [Header("Jump")]
    [SerializeField] float jumpVelocity = 6;
    [SerializeField] float raycastJumpLength = 1f;

    [SerializeField] float timeStopJump = 0.1f;
    float timerStopJump = 0;
    [SerializeField] float jumpFallingModifier = 1;

    [Header("Gun")] 
    [SerializeField] GameObject prefabBullet;
    [SerializeField] Transform bulletSpawnPoint;

    [Header("Animation")] 
    Animator animator_;

    SpriteRenderer spriteRenderer_;
    bool isLookingRight = true;
    
    [SerializeField] int money;
    [SerializeField] TextMeshProUGUI textMoney;
    
    
    
    // Start is called before the first frame update
    void Start() {
        body = GetComponent<Rigidbody2D>();
        spriteRenderer_ = GetComponentInChildren<SpriteRenderer>();
        
        if (body != null) {
            Debug.Log("Body founded!");
        } else {
            Debug.Log("No nody");
        }

        playerHealth = GetComponent<PlayerHealth>();
        animator_ = GetComponentInChildren<Animator>();
    }

    void FixedUpdate() {
        body.velocity = direction;

        if (body.velocity.y < 0) {
            body.velocity = new Vector2(body.velocity.x, body.velocity.y * jumpFallingModifier);
        }

        body.velocity = Vector2.ClampMagnitude(body.velocity, maxSpeed);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Q)) {
            playerHealth.AttackSelf(1);
        }
        
        direction = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);

        CheckJump();

        if (Input.GetAxis("Fire1") > 0.1f) {
            GameObject bullet = Instantiate(prefabBullet, bulletSpawnPoint);
            bullet.transform.parent = null;
            
            bullet.GetComponent<Rigidbody2D>().velocity = Vector2.right * 10; 
        }
        
        animator_.SetFloat("speed", Mathf.Abs(body.velocity.x));

        if (body.velocity.x < 0.1f && isLookingRight) {
            spriteRenderer_.flipX = true;
            isLookingRight = false;
        } else if (body.velocity.x > 0.1f && !isLookingRight) {
            spriteRenderer_.flipX = false;
            isLookingRight = true;
        }

        if (Mathf.Abs(body.velocity.y) > 0.1f) {
            animator_.SetBool("isFalling", true);
        } else {
            animator_.SetBool("isFalling", false);
        }
    }
    
    void CheckJump() {
        timerStopJump -= Time.deltaTime;
        
        if (Input.GetAxis("Jump") > 0.1f && canJump) {
            Debug.Log("Jump");
            animator_.SetTrigger("jump");
            
            direction.y += jumpVelocity;

            canJump = false;

            timerStopJump = timeStopJump;
        }
        
        //Check if grounded
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, raycastJumpLength, LayerMask.NameToLayer("Player"));

        if (timerStopJump <= 0) {
            if (hit.rigidbody != null) {
                canJump = true;
            } else {
                canJump = false;
            }
        }
    }

    public void AddMoney(int value) {
        money += value;

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
