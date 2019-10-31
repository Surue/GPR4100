using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Rigidbody2D body;
    
    Vector2 direction;
    
    [SerializeField]
    private float speed = 4;

    PlayerHealth playerHealth;

    [SerializeField] bool isTop = false;

    bool canJump = true;
    
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
    }
    
    // Update is called once per frame
    void Update() {
        if (isTop) {
            direction = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);

            if (Input.GetAxis("Jump") > 0.1f && canJump) {
                Debug.Log("ici");
                direction.y += 10;

                canJump = false;
            }
            
        } else {
            direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * speed;
        }
    }

    void OnTriggerStay2D(Collider2D other) {
        canJump = true;
    }
}
