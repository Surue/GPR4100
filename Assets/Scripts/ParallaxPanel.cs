using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ParallaxPanel : MonoBehaviour {

    float originPos;

    float offsetX;

    [SerializeField] float parallaxSpeed;
    // Start is called before the first frame update
    void Start() {
        offsetX = transform.position.x;
        originPos = transform.position.x;
    }

    // Update is called once per frame
    void Update() {
        
        Vector3 cameraPos = Camera.main.transform.position;
        float temp = cameraPos.x * (1 - parallaxSpeed);
        float diff = cameraPos.x * parallaxSpeed;
        
        transform.position = new Vector3(originPos + diff, transform.position.y, transform.position.z);
        
        if (temp > originPos + offsetX) {
            originPos += offsetX;
        } else if(temp < originPos - offsetX) {
            originPos -= offsetX;
        }
    }
}
