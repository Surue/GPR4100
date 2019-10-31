using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {
    Rigidbody2D body;

    Vector2 direction = new Vector2(1, 0);
    
    // Start is called before the first frame update
    void Start() {
        body = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        body.velocity = direction;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -10) {
            direction.x = 1;
        } else if(transform.position.x > 0) {
            direction.x = -1;
        }
    }
}
