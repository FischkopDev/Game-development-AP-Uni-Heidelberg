/**
 * @class Cleanup_Interactor
 * @brief Handles the interaction logic for cleaning up or disabling objects in the game.
 *
 * This component is designed to be attached to objects that should be disabled or "cleaned up"
 * when the player interacts with them. It implements the Interactable interface.
 *
 * @details 
 * Features:
 * - Disable gameobjects when clicked
 *
 * Requirements:
 * - Check each frame for interaction
 * - Make gameobjects invisible that got cleaned up
 *
 * How to Use:
 * Give this component to any gameobject you'd like to cleanup or make disappear when clicked.
 *
 * @author Timo Skrobanek
 * @date 18.8.2024
 * @version 1.0
 */
using UnityEngine;

public class Cleanup_Interactor : MonoBehaviour, Interactable
{

    public GameObject key;
    private bool keyVisibility = true;
    /**
     * @brief Executes the interaction logic when the player interacts with this object.
     *
     * @param obj The GameObject that is being interacted with.
     *
     * This method checks if the interaction key (E) is pressed, and if so, it disables the 
     * object, effectively removing it from the scene. A debug message is logged to confirm 
     * the action.
     */
    public void Interact(GameObject obj)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // On interaction, disable the object's rendering and functionality
            obj.SetActive(false);
            key.GetComponent<MeshRenderer>().enabled = false;
            Debug.Log("Object disabled");
            StateManager.cleanUp();

            if(!StateManager.ItemsLeftToCleanup()){
                StateManager.state = StateManager.State.SCENE1_CLEANUP_DONE;
                Debug.Log("Cleanup task done");
            }
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
        keyVisibility = true;
    }

    public void IsNotAccessable(){

    }
}
