/**
 * @class Letter_Interactor
 * @brief Handles the interaction logic for readable items
 *
 * This component is designed to be attached to gameobject. Providing an id you'll be
 * able to read items.
 *
 * @details 
 * Features:
 * - Show text when clicking on an object containing this component
 *
 * Requirements:
 * - Check if the item is clicked with "E"
 * - 
 *
 * How to Use:
 * Give this component to any gameobject you'd like to cleanup or make disappear when clicked.
 *
 * @author Timo Skrobanek
 * @date 18.8.2024
 * @version 1.0
 */
using UnityEngine;

public class Letter_Interactor : MonoBehaviour, Interactable
{
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
            Debug.Log("Object disabled");
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
        // Implementation pending
    }
}
