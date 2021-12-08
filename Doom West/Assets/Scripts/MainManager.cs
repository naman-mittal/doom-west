using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MainManager : MonoBehaviour
{
    public static MainManager Manager { get; private set; } // ENCAPSULATION

    public GameObject selectedGun;

    // ENCAPSULATION
    public int highscore
    {
        get
        {
            return myHighscore;
        }
        set
        {
            if(value >= 0)
            {
                myHighscore = value;
            }
            else
            {
                myHighscore = 0;
                Debug.Log("negative value is not allowed");
            }
        }
    }

    private int myHighscore;

    private void Awake()
    {
        if (Manager != null)
        {
            Destroy(gameObject);
            return;
        }

        Manager = this;
        DontDestroyOnLoad(gameObject);
        LoadData();

    }

    [System.Serializable]
    public class SaveData
    {
        public int highscore;
    }

    public void SaveHighscore()
    {
        string path = Application.persistentDataPath + "/saveFile.json";

        SaveData saveData = new SaveData();
        saveData.highscore = myHighscore;

        string json = JsonUtility.ToJson(saveData);

        File.WriteAllText(path, json);
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/saveFile.json";

        if (File.Exists(path))
        {
            SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(path));
            highscore = saveData.highscore;
        }
    
    }
}
