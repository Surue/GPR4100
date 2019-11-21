using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    [SerializeField]
    float maxHealth;
    
    float currentHealth;

    [SerializeField] Image lifeBarImage;

    [SerializeField] Color fullLife;
    [SerializeField] Color lowLife;

    [SerializeField] float timeBeforeLifeUp = 3;
    float timerBeforeLifeUp = 0;

    [SerializeField] GameObject prefabUiLoseLife;
    
    // Start is called before the first frame update
    void Start() {
        currentHealth = maxHealth;
    }

    void Update() {
        timerBeforeLifeUp += Time.deltaTime;

        if (timerBeforeLifeUp >= timeBeforeLifeUp) {
            if (currentHealth < maxHealth) {
                currentHealth += 0.2f;
                UpdateUiLifeBar();
            }
        }
    }

    void UpdateUiLifeBar() {
        lifeBarImage.fillAmount = currentHealth / maxHealth;
        lifeBarImage.color = Color.Lerp(lowLife, fullLife, currentHealth / maxHealth);
    }

    public void AttackSelf(int dmg) {
        currentHealth -= dmg;
        
        if (currentHealth <= 0) {
            Destroy(gameObject);
        }
        
        UpdateUiLifeBar();
        timerBeforeLifeUp = 0;

        Instantiate(prefabUiLoseLife, transform.position, Quaternion.identity);
        
    }

    public void TakeDamage(int dmg) {
        currentHealth -= dmg;
        
        Debug.Log("Current health = " + currentHealth);
        
        if (currentHealth <= 0) {
            Destroy(gameObject);
        }
        
        UpdateUiLifeBar();
        timerBeforeLifeUp = 0;
        
        Instantiate(prefabUiLoseLife, transform.position, Quaternion.identity);
    }
}
