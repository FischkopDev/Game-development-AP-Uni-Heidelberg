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

    public void Interact(GameObject obj){
        if(Input.GetKeyDown(KeyCode.E)){
            if(StateManager.state == StateManager.State.SCENE1_COMPLETED){
                Debug.Log("Switching to Scene 2");
                StateManager.state = StateManager.State.SCENE2;
                ChangeScene("Scene2");
             }   
             else if(StateManager.state == StateManager.State.SCENE3_SIT_DOWN){
                Debug.Log("Switching to Scene 2");
                ChangeScene("Scene4");
             } 
             else{
                Debug.Log("Change not allowed here: " + StateManager.state);
             }
        }
    }

    public void IsAccessable(GameObject obj){

    }

    /**
     * @brief Triggers the event to change the scene to @scenename
     *
     */
    void ChangeScene(String name){
        SceneManager.LoadScene(name);
    }

        public void IsNotAccessable(){
    }
}