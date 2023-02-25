using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject[] mainMenuButtons;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewGame()
    {
        DeactiveMainMenuButtons();
    }
    public void LoadGame()
    {
        DeactiveMainMenuButtons();
    }

    void DeactiveMainMenuButtons()
    {
        mainMenuButtons[0].SetActive(false);
        mainMenuButtons[1].SetActive(false);

    }
    void ActiveMainMenuButtons()
    {
        mainMenuButtons[0].SetActive(true);
        mainMenuButtons[1].SetActive(true);

    }

}
