using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToMainMenu : MonoBehaviour
{
    void Update()
    {
        // Überprüfe, ob die Escape-Taste gedrückt wurde
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Lade die Hauptmenü-Szene
            SceneManager.LoadScene("Menu"); // Ersetze "MainMenu" durch den Namen der Hauptmenü-Szene
        }
    }
}

