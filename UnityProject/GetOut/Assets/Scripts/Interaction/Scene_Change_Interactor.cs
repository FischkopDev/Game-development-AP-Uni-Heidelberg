/**
 * @class Scene1_Change_Interactor
 * @brief Handles the change between scenes
 *
 *
 * @details 
 * Features:
 * - Trigger for interaction
 * - Receive the hit object
 * - Check if the object is in a specific range for interaction
 *
 * Requirements:
 * - Check each frame for interaction
 *
 * How to Use:
 * Give this component to the player in unity and add another class using @Interactable to any gameobject you'd like to
 * interact with. 
 *
 * @author Timo Skrobanek
 * @date 2.9.2024
 * @version 1.0
 */
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene1_Change_Interactor : MonoBehaviour, Interactable
{

    // @brief Target scene indicated by name
    public String sceneName;

    public void Interact(GameObject obj){
        if(Input.GetKeyDown(KeyCode.E)){
            ChangeToScene2();
            Debug.Log("Change of scene");
        }
    }

    public void IsAccessable(GameObject obj){

    }

    /**
     * @brief Triggers the event to change the scene to @scenename
     *
     */
    void ChangeToScene2(){
         SceneManager.LoadScene(sceneName);
    }

        public void IsNotAccessable(){
    }
}