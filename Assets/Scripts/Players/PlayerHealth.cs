using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    [SerializeField]
    float maxHealth;
    
    float currentHealth;
    
    
    // Start is called before the first frame update
    void Start() {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AttackSelf(int dmg) {
        currentHealth -= dmg;
        
        Debug.Log("Current health = " + currentHealth);
        
        if (currentHealth <= 0) {
            Destroy(gameObject);
        }
    }
}
