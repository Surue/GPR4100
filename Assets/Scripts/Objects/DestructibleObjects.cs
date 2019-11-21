using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class DestructibleObjects : MonoBehaviour {
    [SerializeField] int health = 5;

    int test = 10;

    public int Test {
        get => test;
        set => test = value;
    }

    void OnCollisionEnter2D(Collision2D other) {

        if (other.gameObject.GetComponent<Bullet>() != null) {
            Bullet bullet = other.gameObject.GetComponent<Bullet>();
            health -= bullet.GetDamage();

            if (health <= 0) {
                Destroy(gameObject);
            }
        }
    }
}
