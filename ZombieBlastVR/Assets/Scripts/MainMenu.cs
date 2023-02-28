using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject[] mainMenuButtons;
    public GameObject[] mainMenus;
    public GameObject savedgameprefabs;
    void Update()
    {
        if (XRSettings.loadedDeviceName.Equals("cardboard"))
        {
            SceneManager.LoadSceneAsync(1);
        }
    }

    public void NewGame()
    {
        DeactiveMainMenuButtons();
        mainMenus[0].SetActive(true); 
    }
    //public void LoadGame()
    //{
    //    DeactiveMainMenuButtons();
    //    mainMenus[1].SetActive(true);
    //    GameObject content = mainMenus[1].transform.GetChild(0).gameObject.GetComponent<ScrollRect>().content.gameObject; 
    //}

    void DeactiveMainMenuButtons()
    {
        mainMenuButtons[0].SetActive(false);
        mainMenuButtons[1].SetActive(false);
        mainMenuButtons[2].SetActive(true);

    }
    public void ActiveMainMenuButtons()
    {
        mainMenus[0].SetActive(false);
        mainMenus[1].SetActive(false);
        mainMenuButtons[0].SetActive(true);
        mainMenuButtons[1].SetActive(true);
        mainMenuButtons[2].SetActive(false);
        

    }

    public void LoadGame()
    {
        string[] AllNames = PlayerPrefs.GetString("ZombieBlastSavedGames").Split(',');
        if (AllNames.Length != 0)
        {
            DeactiveMainMenuButtons();
            mainMenus[1].SetActive(true);
            GameObject content = mainMenus[1].transform.GetChild(0).gameObject.GetComponent<ScrollRect>().content.gameObject;
            float h = 100 * (AllNames.Length - 1);
            content.GetComponent<RectTransform>().sizeDelta = new Vector2(0f, h);
            Vector3 position = new Vector3(-27f, (h / 2 - 30f), 0f);
            foreach (string name in AllNames)
            {
                GameObject sgp = Object.Instantiate(savedgameprefabs, content.transform, false);
                sgp.GetComponent<RectTransform>().localPosition = position;
                sgp.GetComponent<RectTransform>().sizeDelta = new Vector2(600f, 60f);
                sgp.GetComponent<TextMeshProUGUI>().text = name;
                sgp.GetComponent<Button>().onClick.AddListener(() => LoadingGame(name));
                position = position + new Vector3(0f, -100f, 0f);
            }
        }
    }

    public void LoadingGame(string sgp)
    {
        SavedData.control.username = sgp;
        SavedData.control.LoadGame();
        StartCoroutine(VRLoader());
    }



    public void CreateGame(GameObject InputFiledText)
    {
        SavedData.control.username = InputFiledText.GetComponent<TextMeshProUGUI>().text;
        SavedData.control.username = SavedData.control.username.ToUpper();
        if (!SavedData.control.LoadGame())
        {
            SavedData.control.SaveGame();
            string allnames = PlayerPrefs.GetString("ZombieBlastSavedGames");
            allnames = allnames + "," + SavedData.control.username;
            PlayerPrefs.SetString("ZombieBlastSavedGames", allnames);
        }
        StartCoroutine(VRLoader());
    }
    IEnumerator VRLoader()
    {
        XRSettings.LoadDeviceByName("cardboard");
        StartCoroutine(waitingForFrame());
        yield return 0;
    }

    IEnumerator waitingForFrame()
    {
        yield return 0;
        XRSettings.enabled = true;
    }
}
