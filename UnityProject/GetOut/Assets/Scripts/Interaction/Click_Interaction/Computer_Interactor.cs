/**
 * @class Computer_Interactor
 * @brief Handles the interactaction between player his computer in office
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
 * @date 1.9.2024
 * @version 1.0
 */
using UnityEngine;
using UnityEngine.SceneManagement;

public class Computer_Interactor : MonoBehaviour, Interactable
{

    public GameObject key;
    /**
     * @brief Executes the interaction logic when the player interacts with this object.
     *
     * @param obj The GameObject that is being interacted with.
     *
     * This method checks if the interaction key (E) is pressed, and if so, it triggers
     * the logic to change the player's outfit. The actual animation implementation is pending.
     */
    public void Interact(GameObject obj)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // TODO: Implement animation
            Debug.Log("interact with computer");
            key.GetComponent<MeshRenderer>().enabled = false;
            
            StateManager.state = StateManager.State.SCENE3;
            SceneManager.LoadScene("Scene3");
        }
    }

    /**
     * @brief Indicates whether the object is accessible for interaction.
     *
     * This method is intended to provide feedback or state information
     * about whether the object can be interacted with. Currently, it is not implemented.
     */
    public void IsAccessable(GameObject obj)
    {
        key.GetComponent<MeshRenderer>().enabled = true;
    }

    public void IsNotAccessable(){
        key.GetComponent<MeshRenderer>().enabled = false;
    }
}
