using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisiblePlatform : MonoBehaviour {

    [SerializeField] InvisiblePlatform nextPlatform;
    [SerializeField] bool isActive = false;
    
    
    SpriteRenderer spriteRenderer_;
    BoxCollider2D[] boxColliders_;
    
    
    // Start is called before the first frame update
    void Start() {
        spriteRenderer_ = GetComponent<SpriteRenderer>();
        boxColliders_ = GetComponents<BoxCollider2D>();

        if (isActive) {
            Active();
        } else {
            Deactivate();
        }
    }

    public void Active() {
        foreach (BoxCollider2D boxCollider2D in boxColliders_) {
            boxCollider2D.enabled = true;
        }
        
        spriteRenderer_.enabled = true;
        spriteRenderer_.color = new Color(0.0f, 1f, 0.0f, 1.0f);
    }

    public void Deactivate() {

        foreach (BoxCollider2D boxCollider2D in boxColliders_) {
            boxCollider2D.enabled = false;
        }
        
        spriteRenderer_.enabled = false;
    }
    
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            if (nextPlatform != null) {
                nextPlatform.Active();
            }
        }
    }
}
