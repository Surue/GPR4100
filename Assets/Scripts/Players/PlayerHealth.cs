using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    [Header("Health")]
    [SerializeField]
    float maxHealth;
    
    float currentHealth;

    [Header("Animation")]
    [SerializeField] Image lifeBarImage;

    [SerializeField] Color fullLife;
    [SerializeField] Color lowLife;

    [SerializeField] float timeBeforeLifeUp = 3;
    float timerBeforeLifeUp = 0;

    [SerializeField] GameObject prefabUiLoseLife;
    
    // Start is called before the first frame update
    void Start() {
        //Set the current health to the max health
        currentHealth = maxHealth;
    }

    void Update() {
        //Update timer
        timerBeforeLifeUp += Time.deltaTime;

        //Check if can gain life 
        if (!(timerBeforeLifeUp >= timeBeforeLifeUp)) return;
        if (!(currentHealth < maxHealth)) return;
        
        //Update life
        currentHealth += 0.2f;
        UpdateUiLifeBar();
    }

    void UpdateUiLifeBar() {
        lifeBarImage.fillAmount = currentHealth / maxHealth;
        lifeBarImage.color = Color.Lerp(lowLife, fullLife, currentHealth / maxHealth);
    }

    public void TakeDamage(int dmg) {
        currentHealth -= dmg;
        
        Debug.Log("Current health = " + currentHealth);
        
        if (currentHealth <= 0) {
            Destroy(gameObject);
        }
        
        UpdateUiLifeBar();
        timerBeforeLifeUp = 0;
        
        //Instantiate splash text when loosing life
        Instantiate(prefabUiLoseLife, transform.position, Quaternion.identity);
    }

    void ReduceLife() {
        
    }
}
