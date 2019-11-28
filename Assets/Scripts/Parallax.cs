using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {

    [SerializeField] float speedMovement = 0.1f;

    float zDepth = 10;

    Vector3 lastPosition;

    [SerializeField] List<ParallaxPanel> spritesToSwap;

    float offsetX;
    
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, zDepth);

        lastPosition = Camera.main.transform.position;

        offsetX = Mathf.Abs(spritesToSwap[0].transform.position.x);
    }

    // Update is called once per frame
    void LateUpdate() {

        Vector3 cameraPos = Camera.main.transform.position;
        
        if (Mathf.Abs(cameraPos.x - lastPosition.x) > 0.2f) {
           
            //Movement
            Vector3 diff = cameraPos - transform.position;

            transform.position += diff.normalized * speedMovement;

            transform.position = new Vector3(transform.position.x, cameraPos.y, zDepth);

            lastPosition = Camera.main.transform.position;

            //Swap
            if (Mathf.Abs(transform.position.x - cameraPos.x) > offsetX) {
                if (diff.x < 0) {
                    transform.position  = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, zDepth);
                } else {
                    transform.position  = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, zDepth);
                }
            }

            foreach (ParallaxPanel parallaxPanel in spritesToSwap) {
//                parallaxPanel.UpdateSprite();
            }
        }
    }
}
