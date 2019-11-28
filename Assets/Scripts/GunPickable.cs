using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickable : MonoBehaviour {
    [SerializeField] SO_Gun gunData;

    SpriteRenderer spriteRenderer_;
    
    // Start is called before the first frame update
    void Start() {
        spriteRenderer_ = GetComponent<SpriteRenderer>();

        spriteRenderer_.sprite = gunData.Sprite;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            other.GetComponent<PlayerController>().GetGun().SetNewGun(gunData);
        }
    }
}
