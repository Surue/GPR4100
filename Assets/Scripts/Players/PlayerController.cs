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

    List<Vector2> jumpPosition;
    
    // Start is called before the first frame update
    void Start() {
        body = GetComponent<Rigidbody2D>();

        if (body != null) {
            Debug.Log("Body founded!");
        } else {
            Debug.Log("No nody");
        }

        playerHealth = GetComponent<PlayerHealth>();
        
        jumpPosition = new List<Vector2>();
    }

    void FixedUpdate() {
        body.velocity = direction;

        if (body.velocity.y < 0) {
            body.velocity = new Vector2(body.velocity.x, body.velocity.y * jumpFallingModifier);
        }

        body.velocity = Vector2.ClampMagnitude(body.velocity, maxSpeed);
        
        jumpPosition.Add(transform.position);
    }

    void CheckJump() {
        timerStopJump -= Time.deltaTime;
        
        if (Input.GetAxis("Jump") > 0.1f && canJump) {
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

//        Collider2D coll = Physics2D.OverlapBox((Vector2) transform.position + Vector2.down * 0.5f, new Vector2(1f, 0.2f), 
//            0, LayerMask.NameToLayer("Player"));
//
//        if (timerStopJump <= 0) {
//            if (coll != null) {
//                Debug.Log(coll);
//                canJump = true;
//            } else {
//                canJump = false;
//            }
//        }
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

    void OnTriggerEnter2D(Collider2D other) {
        
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine((Vector2)transform.position, (Vector2)transform.position + Vector2.down * raycastJumpLength);
        
        Gizmos.DrawWireCube((Vector2) transform.position + Vector2.down * 0.5f, new Vector2(1f, 0.2f));
        
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(bulletSpawnPoint.position, 0.3f);

        if (jumpPosition != null) {
            for (int i = 0; i < jumpPosition.Count; i++) {
                Gizmos.DrawWireSphere(jumpPosition[i], 0.1f);
            }
        }
    }
}
