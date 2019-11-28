using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class House : MonoBehaviour
{
    enum State {
        DOWN,
        UP,
        GO_UP,
        GO_DOWN
    }

    State state = State.DOWN;

    [SerializeField] Transform door;
    [SerializeField] float speedDoor;
    [SerializeField] float maxHeight;
    
    float startingHeight;

    AudioManager audioManager_;
    
    
    // Start is called before the first frame update
    void Start() {
        startingHeight = door.position.y;

        maxHeight += startingHeight;

        audioManager_ = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        audioManager_.SetAmbiantCutoff(1 - (door.position.y / maxHeight));
        
        switch (state) {
            case State.DOWN:
                break;
            case State.UP:
                break;
            case State.GO_UP: {
                Vector3 posDoor = door.position;
                door.position = new Vector3(posDoor.x, posDoor.y + Time.deltaTime * speedDoor, posDoor.z);
                
                if (door.position.y > maxHeight) {
                    state = State.UP;
                }
            }
                break;
            case State.GO_DOWN: {
                Vector3 posDoor = door.position;
                door.position = new Vector3(posDoor.x, posDoor.y - Time.deltaTime * speedDoor, posDoor.z);
                
                if (door.position.y < startingHeight) {
                    state = State.UP;
                }
            }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            state = State.GO_UP;
        }
    }
    
    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            state = State.GO_DOWN;
        }
    }

    void OnDrawGizmos() {
        if (startingHeight == 0) {
            Gizmos.DrawWireSphere(door.position, 1);
            Gizmos.DrawWireSphere(door.position + new Vector3(0, maxHeight, 0), 1);
        } else {
//            Gizmos.DrawWireSphere(transform.position, 1);
        }
    }
}
