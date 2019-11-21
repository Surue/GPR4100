using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AngryBird : MonoBehaviour {
    Vector3 target_;

    [SerializeField] float speed_;
    
    Rigidbody2D body_;
    
    enum State {
        IDLE,
        CHASE_PLAYER,
        RETURN_NEST
    }

    State state = State.IDLE;

    Vector3 originalPos_;
    
    // Start is called before the first frame update
    void Start() {
        body_ = GetComponent<Rigidbody2D>();

        originalPos_ = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state) {
            case State.IDLE:
                break;
            case State.CHASE_PLAYER:
                body_.velocity = (target_ - transform.position).normalized * speed_;

                if (Vector3.Distance(transform.position, target_) < 0.5f) {
                    state = State.RETURN_NEST;
                }
                break;
            case State.RETURN_NEST:
                body_.velocity = (originalPos_ - transform.position).normalized * speed_;

                if (Vector3.Distance(transform.position, originalPos_) < 0.1f) {
                    state = State.IDLE;
                }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }   
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.GetComponent<PlayerController>()) {
            target_ = other.transform.position;
            state = State.CHASE_PLAYER;
        }
    }
}
