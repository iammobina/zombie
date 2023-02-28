using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class SavedData : MonoBehaviour
{
    public static SavedData control;
    public string username;
    public int score;
    public bool[] levels;

    private void Awake()
    {
        if(control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
        } else if (control != null)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file;
        if (File.Exists(Application.persistentDataPath+"/"+ username + ".dat"))
        {
            file = File.Open(Application.persistentDataPath + "/" + username + ".dat", FileMode.Open);
            Debug.Log("exist");
        }
        else
        {
            file = File.Create(Application.persistentDataPath + "/" + username + ".dat");
            Debug.Log("create");

        }
        SaveDataHolder sdh = new SaveDataHolder(username, score, levels);
        bf.Serialize(file, sdh);
        file.Close();
    }

    public bool LoadGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file;
        if (File.Exists(Application.persistentDataPath + "/" + username + ".dat"))
        {
            file = File.Open(Application.persistentDataPath + "/" + username + ".dat", FileMode.Open);
            SaveDataHolder sdh = (SaveDataHolder) bf.Deserialize(file);
            username = sdh.GetUsername();
            score = sdh.GetScore();
            levels = sdh.GetLevels();
            file.Close();
            return true;
        }
        else
        {
            Debug.Log("file not found");
            return false;
        }

    }
}
[Serializable]
class SaveDataHolder
{
    string username;
    int score;
    bool[] levels;
    public SaveDataHolder(string name, int scr, bool[] lvls)
    {
        username = name;
        score = scr;
        levels = new bool[lvls.Length];
        for (int i = 0; i < lvls.Length; i++)
        {
            levels[i] = lvls[i];
        }
    }
    public string GetUsername()
    {
        return username;
    }
    public int GetScore()
    {
        return score;
    }
    public bool[] GetLevels()
    {
        return levels; 
    }
}

