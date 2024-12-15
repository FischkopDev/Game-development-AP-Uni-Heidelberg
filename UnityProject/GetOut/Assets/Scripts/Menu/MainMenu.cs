using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   public void PlayGame() {
    //TODO correct implementation for change to other scenes beside scene 1
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 
   }

   public void QuitGame() {
    Debug.Log("QUIT!");
    Application.Quit();
   }

   void Update() {
       // Überprüfe, ob die R-Taste gedrückt wurde
        if (Input.GetKeyDown(KeyCode.R))
        {
            // Lade die Hauptmenü-Szene
            SceneManager.LoadScene("MainMenu");
        }
   }

}
