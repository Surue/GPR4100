using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour {

    [SerializeField]
    float zValue = -10;

    [SerializeField] Transform target_;

    [SerializeField] bool doLerp = false;

    // Start is called before the first frame update
    void Start() {
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (doLerp) {
            transform.position = Vector3.Lerp(transform.position, new Vector3(target_.position.x, target_.position.y, zValue), 0.5f);
        } else {
            transform.position = target_.position;

            transform.position = new Vector3(transform.position.x, transform.position.y, zValue);
        }
    }
}
