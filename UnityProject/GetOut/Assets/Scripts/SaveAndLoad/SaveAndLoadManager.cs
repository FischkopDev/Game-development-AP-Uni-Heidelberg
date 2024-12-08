using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
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
    public static SaveLoadManager Instance => instance;

    void Awake() 
    {
        if(instance == null) 
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            saveFilePath = Application.persistentDataPath + "/save.dat";
            LoadGame();
        } 
        else 
        {
            Destroy(gameObject);
        }
    }

    public void SaveGame()
    {
        currentSave.lastSceneIndex = SceneManager.GetActiveScene().buildIndex;
        StateManager.Instance.UpdateGameState(currentSave);

        BinaryFormatter formatter = new BinaryFormatter();
        using (MemoryStream memoryStream = new MemoryStream())
        {
            // Serialisieren der aktuellen Spieldaten
            formatter.Serialize(memoryStream, currentSave);
            byte[] serializedData = memoryStream.ToArray();

            // Verschlüsseln der serialisierten Daten
            byte[] encryptedData = EncryptionHelper.Encrypt(serializedData);

            // Hash der verschlüsselten Daten erstellen, um Manipulation zu erkennen
            byte[] hash = HashHelper.ComputeSHA256Hash(encryptedData);

            // Kombiniere die Hash-Werte und die verschlüsselten Daten (erst der Hash, dann die verschlüsselten Daten)
            byte[] combinedData = new byte[hash.Length + encryptedData.Length];
            System.Buffer.BlockCopy(hash, 0, combinedData, 0, hash.Length);
            System.Buffer.BlockCopy(encryptedData, 0, combinedData, hash.Length, encryptedData.Length);

            File.WriteAllBytes(saveFilePath, combinedData);
        }
    }

    public void LoadGame()
    {
        if (File.Exists(saveFilePath))
        {
            byte[] combinedData = File.ReadAllBytes(saveFilePath);

            // Extrahiere den Hash und die verschlüsselten Daten
            byte[] hash = new byte[32]; // SHA-256 Hash ist 32 Bytes lang
            byte[] encryptedData = new byte[combinedData.Length - 32];
            System.Buffer.BlockCopy(combinedData, 0, hash, 0, 32);
            System.Buffer.BlockCopy(combinedData, 32, encryptedData, 0, encryptedData.Length);

            // Überprüfe den Hash, um Manipulationen zu erkennen
            byte[] calculatedHash = HashHelper.ComputeSHA256Hash(encryptedData);
            if (!HashHelper.CompareHashes(hash, calculatedHash))
            {
                Debug.LogError("Save file has been tampered with!");
                return;
            }

            // Entschlüsseln der verschlüsselten Spieldaten
            byte[] decryptedData = EncryptionHelper.Decrypt(encryptedData);

            BinaryFormatter formatter = new BinaryFormatter();
            using (MemoryStream memoryStream = new MemoryStream(decryptedData))
            {
                currentSave = (SaveData)formatter.Deserialize(memoryStream);
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

public static class EncryptionHelper 
{
    private static readonly string encryptionKey = "DeinSuperSichererSchlüssel123!"; // Geheimer Schlüssel, 32 Zeichen (für AES-256)

    public static byte[] Encrypt(byte[] data) 
    {
        using (Aes aes = Aes.Create()) 
        {
            aes.Key = Encoding.UTF8.GetBytes(encryptionKey.PadRight(32).Substring(0, 32)); // 32 Byte Schlüssel für AES-256
            aes.IV = new byte[16]; // Initialisierungsvektor, 16 Bytes für AES-128 / AES-256

            using (ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV)) 
            {
                return encryptor.TransformFinalBlock(data, 0, data.Length);
            }
        }
    }

    public static byte[] Decrypt(byte[] encryptedData) 
    {
        using (Aes aes = Aes.Create()) 
        {
            aes.Key = Encoding.UTF8.GetBytes(encryptionKey.PadRight(32).Substring(0, 32)); // 32 Byte Schlüssel
            aes.IV = new byte[16]; // Gleicher IV wie beim Verschlüsseln

            using (ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV)) 
            {
                return decryptor.TransformFinalBlock(encryptedData, 0, encryptedData.Length);
            }
        }
    }
}

public static class HashHelper 
{
    public static byte[] ComputeSHA256Hash(byte[] data) 
    {
        using (SHA256 sha256 = SHA256.Create()) 
        {
            return sha256.ComputeHash(data);
        }
    }

    public static bool CompareHashes(byte[] hash1, byte[] hash2) 
    {
        if (hash1.Length != hash2.Length) 
            return false;

        for (int i = 0; i < hash1.Length; i++) 
        {
            if (hash1[i] != hash2[i]) 
                return false;
        }

        return true;
    }
}
