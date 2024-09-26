using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SaveData 
{
    public int lastSceneIndex;
    public Dictionary<string, object> globalVariables = new Dictionary<string, object>();

}

public class SaveLoadManager : MonoBehaviour
{
    private static SaveLoadManager instance;
    private SaveData currentSave;
    private string saveFilePath;

    void Awake() 
    {
        if(instance == null) 
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            // using Application.persistentDataPath because paths are different from operating system to operating system
            saveFilePath = Application.persistentDataPath + "/save.dat";
            // load existing save if available
            LoadGame();
        } 
        else 
        {
            Destroy(gameObject);
        }
    }

    public static SaveLoadManager Instance => instance;

/*  
Implementation without StateManager  
        public void SaveGame() 
    {
        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream stream = new FileStream(saveFilePath, FileMode.Create)) 
        {
            // get scene index in order to save the last scene
            currentSave.lastSceneIndex = SceneManager.GetActiveScene().buildIndex;
            formatter.Serialize(stream, currentSave);
        }
    }

    public void LoadGame() 
    {
        if(File.Exists(saveFilePath)) 
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(saveFilePath, FileMode.Open)) 
            {
                currentSave = (SaveData)formatter.Deserialize(stream);
                SceneManager.LoadScene(currentSave.lastSceneIndex);
            }
        }
        else 
        {
            currentSave = new SaveData();
        }
    }
*/

//Implementation with StateManager
public void SaveGame()
{
    currentSave.lastSceneIndex = SceneManager.GetActiveScene().buildIndex;
    StateManager.Instance.UpdateGameState(currentSave);

    BinaryFormatter formatter = new BinaryFormatter();
    using (FileStream stream = new FileStream(saveFilePath, FileMode.Create))
    {
        formatter.Serialize(stream, currentSave);
    }
}

public void LoadGame()
{
    if (File.Exists(saveFilePath))
    {
        BinaryFormatter formatter = new BinaryFormatter();
        using (FileStream stream = new FileStream(saveFilePath, FileMode.Open))
        {
            currentSave = (SaveData)formatter.Deserialize(stream);
            StateManager.Instance.UpdateGameState(currentSave);
            SceneManager.LoadScene(currentSave.lastSceneIndex);
        }
    }
    else
    {
        currentSave = new SaveData();
        StateManager.Instance.UpdateGameState(currentSave);
    }
}

    public void SetGlobalVariable(string key, object value) 
    {
        currentSave.globalVariables[key] = value;
    }

    public object GetGlobalVariable(string key) 
    {
        return currentSave.globalVariables.ContainsKey(key) ? currentSave.globalVariables[key] : null;
    }
}