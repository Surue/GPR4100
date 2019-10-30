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
        body.velocity = direction * speed;
    }
    
    // Update is called once per frame
    void Update() {
        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (Input.GetAxis("Fire1") > 0.1f) {
            playerHealth.AttackSelf(10);
        }
    }
}
