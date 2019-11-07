using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour {
    [SerializeField] int value = 10;
    [SerializeField][Range(0, 5)] float floatingDistance = 1;
    
    int directionY = 1;

    Vector3 originalPosition;

    [SerializeField]AnimationCurve animationCurve_;
    
    // Start is called before the first frame update
    void Start() {
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update() {
//        float offsetY = Mathf.PingPong(Time.time, floatingDistance * 2) - floatingDistance; 

//        float offsetY = Mathf.Sin(Time.time);

        float offsetY = animationCurve_.Evaluate(Time.time);
        transform.position = new Vector3(originalPosition.x, originalPosition.y + offsetY * floatingDistance);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            other.GetComponent<PlayerController>().AddMoney(value);
            
            Destroy(gameObject);
        }
    }

    void OnDrawGizmos() {
        if (originalPosition == Vector3.zero) {
            Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y + floatingDistance));
            Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - floatingDistance));
        } else {
            Gizmos.DrawLine(originalPosition, new Vector2(originalPosition.x, originalPosition.y + floatingDistance));
            Gizmos.DrawLine(originalPosition, new Vector2(originalPosition.x, originalPosition.y - floatingDistance));
        }
    }
}
