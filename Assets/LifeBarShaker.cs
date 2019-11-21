using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBarShaker : MonoBehaviour {

    [SerializeField] Image lifeBar;
    [SerializeField] Animator animator;

    float minSpeed = 0.1f;
    
    // Start is called before the first frame update
    void Start() {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        animator.speed = minSpeed;
        
        if (lifeBar.fillAmount < 0.2f) {
            animator.speed = 1 - (lifeBar.fillAmount * 5) + minSpeed;
        }

        animator.SetFloat("Speed", animator.speed);
    }
}
