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

        public GameObject letter;
        public GameObject stateManager;
        //added boolean because of letter bug
        private bool isLetterOpen = false;
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
        // Move the letter back along the Z-axis to make it appear farther away
            letter.transform.position += new Vector3(0, 0, -1f); // Move the letter 1 unit farther from the camera

            if (Input.GetKeyDown(KeyCode.E) && !isLetterOpen)
            {
                    // On interaction, disable the object's rendering and functionality
                    letter.SetActive(true);
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = false;
                    isLetterOpen = true;
                
            }

            // After reading letter, press escape in order to continue with the game
            if (isLetterOpen && (Input.GetKeyDown(KeyCode.R) || Input.GetKeyDown(KeyCode.Escape)))
            {
                // close letter and lock cursor
                letter.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked; // Locks cursor
                Cursor.visible = false; // Hide cursor
                isLetterOpen = false;
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

            public void IsNotAccessable(){
        }
    }

