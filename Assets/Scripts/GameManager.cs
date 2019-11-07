using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    static GameManager instance = null;

    public int value;

    [SerializeField] GameObject canvasPause;
    [SerializeField] string sceneMainMenu = "MainMenu";
    
    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this);
        } else {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        canvasPause.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) {
            value++;
        }

        if (Input.GetKeyDown(KeyCode.P)) {
            canvasPause.SetActive(true);
        }
    }

    public void ReturnGame() {
        canvasPause.SetActive(false);
    }

    public void ReturnMainMenu() {
        SceneManager.LoadScene(sceneMainMenu);
        canvasPause.SetActive(false);
    }
}
