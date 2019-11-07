using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingTrap : MonoBehaviour {
    [SerializeField] int damageValue = 10;

    Rigidbody2D body;
    
    // Start is called before the first frame update
    void Start() {
        body = GetComponent<Rigidbody2D>();

        body.bodyType = RigidbodyType2D.Static;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            body.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            LevelManager.player.TakeDamage(damageValue);
        }

        if (other.gameObject.layer != LayerMask.NameToLayer("Ceiling")) {
            Destroy(gameObject);
        }
    }
}
