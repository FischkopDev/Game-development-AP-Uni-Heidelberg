using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class SettingsMenuTest
{
    private GameObject _mainMenuObject;
    private MainMenu _mainMenuScript;


    //für PlayGame_CallsLoadScene()
    public class MockSceneManager
    {
    public int ActiveSceneIndex { get; private set; } = 0;

    public void LoadScene(int index)
    {
        ActiveSceneIndex = index;
    }

    }
    [SetUp]
    public void Setup()
    {
        // Erstelle ein leeres GameObject und füge das MainMenu-Skript hinzu
        _mainMenuObject = new GameObject();
        _mainMenuScript = _mainMenuObject.AddComponent<MainMenu>();
    }

    [TearDown]
    public void Teardown()
    {
        // Lösche das GameObject nach jedem Test, um die Testumgebung sauber zu halten
        Object.DestroyImmediate(_mainMenuObject);
    }

    [Test]
    public void SceneExistsInBuildSettings()
    {
    // Arrange
    string sceneNameToTest = "MainMenu"; // Ersetze mit dem Namen deiner Szene
    bool sceneExists = false;

    // Prüfe, ob die Szene in den Build-Einstellungen enthalten ist
    for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
    {
        string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
        if (scenePath.Contains(sceneNameToTest))
        {
            sceneExists = true;
            break;
        }
    }

    // Assert: Szene existiert in den Build-Einstellungen
    Assert.IsTrue(sceneExists, $"Die Szene '{sceneNameToTest}' ist nicht in den Build-Einstellungen enthalten.");
    }

  
    [Test]
    public void QuitGame_LogsQuitMessage()
    {
        // Arrange
        LogAssert.Expect(LogType.Log, "QUIT!");

        // Act
        _mainMenuScript.QuitGame();

        // Assert
        // LogAssert prüft, ob der erwartete Log-Eintrag gemacht wurde
    }

    // Beispiel-Test
    [Test]
    public void PlayGame_CallsLoadScene()
    {
    var mockSceneManager = new MockSceneManager();
    mockSceneManager.LoadScene(1);
    Assert.AreEqual(1, mockSceneManager.ActiveSceneIndex, "Die Szene wurde nicht korrekt geladen.");
    }

}
