using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour {

    [SerializeField] float topHeight_;
    [SerializeField] float bottomHeight_;

    Vector2 posBottom_;
    Vector2 posTop_;


    const float timeGoUp = 0.5f;
    float timerGoUp = 0;

    const float timeWaiting = 1f;
    float timerWaiting = 0;
    
    const float timeGoDown = 2.5f;
    float timerGoDown = 0;

    enum State {
        IDLE,
        GO_UP,
        UP_WAITING,
        GO_DOWN
    }

    State state = State.IDLE;
    
    void Start() {
        posBottom_ = transform.position + Vector3.down * bottomHeight_;
        posTop_ = transform.position + Vector3.up * topHeight_;

        transform.position = posBottom_;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state) {
            case State.IDLE:
                break;
            case State.GO_UP: {
                timerGoUp += Time.deltaTime;
                transform.position = Vector3.Lerp(posBottom_, posTop_, timerGoUp / timeGoUp);

                if (timerGoUp >= timeGoUp) {
                    state = State.UP_WAITING;
                    timerGoUp = 0;
                }
            }
                break;
            case State.UP_WAITING: {
                timerWaiting += Time.deltaTime;

                if (timerWaiting >= timeWaiting) {
                    timerWaiting = 0;
                    state = State.GO_DOWN;
                }
            }
                break;
            case State.GO_DOWN:{
                timerGoDown += Time.deltaTime;
                transform.position = Vector3.Lerp(posTop_, posBottom_, timerGoDown / timeGoDown);

                if (timerGoDown >= timeGoDown) {
                    state = State.IDLE;
                    timerGoDown = 0;
                }
            }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        state = State.GO_UP;
    }
}
