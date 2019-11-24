using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPanelSwitcher : MonoBehaviour {
    [SerializeField] GameObject panelMainMenu;
    [SerializeField] GameObject panelCredits;
    [SerializeField] GameObject panelRules;

    enum State {
        MAIN_MENU,
        CREDITS,
        RULES
    }
    
    // Start is called before the first frame update
    void Start()
    {
        panelMainMenu.SetActive(true);
        panelCredits.SetActive(false);
        panelRules.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangePanel(GameObject panel) {
        panelMainMenu.SetActive(false);
        panelCredits.SetActive(false);
        panelRules.SetActive(false);

        if (panel == panelCredits) {
            panelCredits.SetActive(true);
        }

        if (panel == panelRules) {
            panelRules.SetActive(true);
        }

        if (panel == panelMainMenu) {
            panelMainMenu.SetActive(true);
        }
    }
}
