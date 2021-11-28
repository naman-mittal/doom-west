using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MainManager : MonoBehaviour
{
    public static MainManager Manager { get; private set; }

    public GameObject selectedGun;

    public int highscore;

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

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
        saveData.highscore = highscore;

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
