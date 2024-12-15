using System.IO;
using NUnit.Framework;
using UnityEngine;
using System.Security.Cryptography;
using System.Text;
public class FileEncryptionTests
{
    private string filePath;

    public static class EncryptionHelper
    {
    private static readonly string encryptionKey = "TestEncryptionKey1234"; // Muss 16, 24 oder 32 Zeichen lang sein (für AES)

    public static byte[] Encrypt(byte[] data)
    {
        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(encryptionKey.PadRight(32).Substring(0, 32)); // 32 Byte Schlüssel
            aes.IV = new byte[16]; // Initialisierungsvektor (Standard 16 Bytes)

            using (ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
            {
                return encryptor.TransformFinalBlock(data, 0, data.Length);
            }
        }
    }
    }
    
    [SetUp]
    public void SetUp()
    {
        // Pfad für die Testdatei
        filePath = Application.persistentDataPath + "/test_encrypted_file.txt";

        // Sicherstellen, dass keine alte Datei existiert
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }

    [TearDown]
    public void TearDown()
    {
        // Entferne die Testdatei nach jedem Test
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }

    [Test]
    public void TestFileCreationAndEncryption()
    {
        // Beispiel-Daten zum Speichern
        string originalData = "Das ist ein Test zur Verschlüsselung";
        byte[] dataToEncrypt = System.Text.Encoding.UTF8.GetBytes(originalData);

        // Verschlüsselte Daten erstellen
        byte[] encryptedData = EncryptionHelper.Encrypt(dataToEncrypt);

        // Datei speichern
        File.WriteAllBytes(filePath, encryptedData);

        // Überprüfen, ob die Datei erstellt wurde
        Assert.IsTrue(File.Exists(filePath), "Datei wurde nicht erstellt");

        // Datei-Inhalt auslesen und prüfen, ob sie verschlüsselt ist
        byte[] readData = File.ReadAllBytes(filePath);
        Assert.AreNotEqual(dataToEncrypt, readData, "Datei ist nicht verschlüsselt");

        Debug.Log($"Datei erfolgreich erstellt und verschlüsselt: {filePath}");
        Debug.Log($"Datei befindet sich im Ordner: {Application.persistentDataPath}");
    }

    [Test]
    public void TestSimpleFileCreation()
    {
    // Schreibe einfache Daten (ohne Verschlüsselung) in die Datei
    string testContent = "Testinhalt für einfache Datei";
    File.WriteAllText(filePath, testContent);

    // Überprüfen, ob die Datei erstellt wurde
    Assert.IsTrue(File.Exists(filePath), "Datei wurde nicht erstellt");
    Debug.Log($"Datei erstellt unter: {filePath}");

    // Überprüfen, ob die Datei Daten enthält
    string readContent = File.ReadAllText(filePath);
    Assert.AreEqual(testContent, readContent, "Dateiinhalt stimmt nicht überein");
    }

    [Test]
    public void TestMissingFile_CreatesNewFile()
    {
    // Lösche die Datei, falls vorhanden
    if (File.Exists(filePath))
        File.Delete(filePath);

    // Versuche, eine neue Datei zu erstellen
    string newContent = "Das ist eine neue Datei";
    File.WriteAllText(filePath, newContent);

    // Überprüfen, ob die Datei erstellt wurde
    Assert.IsTrue(File.Exists(filePath), "Datei wurde nicht erstellt");
    string readContent = File.ReadAllText(filePath);
    Assert.AreEqual(newContent, readContent, "Dateiinhalt stimmt nicht überein");
    }

    [Test]
    public void TestEmptyFile_HandlesGracefully()
    {
    // Erstelle eine leere Datei
    File.WriteAllBytes(filePath, new byte[0]);

    // Datei-Inhalt prüfen
    byte[] readData = File.ReadAllBytes(filePath);
    Assert.IsNotNull(readData, "Datei-Inhalt sollte nicht null sein");
    Assert.AreEqual(0, readData.Length, "Datei sollte leer sein");
    }

    [Test]
    public void TestCorruptedFile_HandlesGracefully()
    {
    // Erstelle eine beschädigte Datei
    File.WriteAllBytes(filePath, new byte[] { 0xFF, 0xAA, 0xBB });

    // Datei lesen und überprüfen
    byte[] readData = File.ReadAllBytes(filePath);
    Assert.IsNotNull(readData, "Datei-Inhalt sollte nicht null sein");
    Assert.AreEqual(3, readData.Length, "Datei-Inhalt hat unerwartete Größe");
    }

    [Test]
    public void TestLargeFile_SaveAndLoad()
    {
    string largeData = new string('A', 1000000); // 1 Million Zeichen
    byte[] dataToEncrypt = Encoding.UTF8.GetBytes(largeData);

    // Verschlüsselte Daten erstellen und speichern
    byte[] encryptedData = EncryptionHelper.Encrypt(dataToEncrypt);
    File.WriteAllBytes(filePath, encryptedData);

    // Datei lesen und entschlüsseln
    byte[] readData = File.ReadAllBytes(filePath);
    Assert.AreEqual(encryptedData.Length, readData.Length, "Dateigröße stimmt nicht überein");
    }


}
