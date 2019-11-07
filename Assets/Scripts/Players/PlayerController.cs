using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [SerializeField] int money;
    
    // Start is called before the first frame update
    void Start() {
        body = GetComponent<Rigidbody2D>();

        if (body != null) {
            Debug.Log("Body founded!");
        } else {
            Debug.Log("No nody");
        }

        playerHealth = GetComponent<PlayerHealth>();
    }

    void FixedUpdate() {
        body.velocity = direction;

        if (body.velocity.y < 0) {
            body.velocity = new Vector2(body.velocity.x, body.velocity.y * jumpFallingModifier);
        }

        body.velocity = Vector2.ClampMagnitude(body.velocity, maxSpeed);
    }

    void Update() {
        direction = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);

        CheckJump();

        if (Input.GetAxis("Fire1") > 0.1f) {
            GameObject bullet = Instantiate(prefabBullet, bulletSpawnPoint);
            bullet.transform.parent = null;
            
            bullet.GetComponent<Rigidbody2D>().velocity = Vector2.right * 10; 
        }
    }
    
    void CheckJump() {
        timerStopJump -= Time.deltaTime;
        
        if (Input.GetAxis("Jump") > 0.1f && canJump) {
            Debug.Log("Jump");
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
