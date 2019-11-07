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

    public void AttackSelf(int dmg) {
        currentHealth -= dmg;
        
        Debug.Log("Current health = " + currentHealth);
        
        if (currentHealth <= 0) {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int dmg) {
        currentHealth -= dmg;
        
        Debug.Log("Current health = " + currentHealth);
        
        if (currentHealth <= 0) {
            Destroy(gameObject);
        }
    }
}
