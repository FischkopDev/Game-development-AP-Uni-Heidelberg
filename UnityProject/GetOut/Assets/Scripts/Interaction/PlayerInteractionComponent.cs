/**
 * @class InteractionComponent
 * @brief Handles the interact-action between player and gameobjects
 *
 * This component checks if the player is performing an interaction at the moment
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
 * @date 18.8.2024
 * @version 1.0
 */

using UnityEngine;


public class InteractionComponent : MonoBehaviour
{
    /// @brief The Transform of the interactor (e.g., the player or camera).
    public Transform interactor;

    /// @brief The maximum range within which the player can interact with objects.
    public float InteractRange = 5;

    /**
     * @brief Called once per frame to check for interactions.
     *
     * This method casts a ray from the interactor's position forward, checking if it hits
     * any objects within the specified interaction range. If an interactable object is hit,
     * it attempts to interact with it.
     */
    void Update()
    {
        // Send a ray from the interactor's position forward
        Ray directedRay = new Ray(interactor.position, interactor.forward);

        // Check for interactions with objects the ray hits
        CheckInteraction(directedRay);
    }

    /**
     * @brief Checks if the directed ray intersects with an interactable object.
     *
     * @param directedRay The ray cast from the interactor to check for interactions.
     *
     * This method uses a raycast to detect if any objects within the interaction range
     * are interactable. If an interactable object is detected, the interaction method
     * on that object is called.
     */
    public void CheckInteraction(Ray directedRay)
    {
        // Check if the ray hits any objects within the interaction range
        if (Physics.Raycast(directedRay, out RaycastHit hitInfo, InteractRange))
        {
            
            // Check if the hit object has an Interactable component
            if (hitInfo.collider.gameObject.TryGetComponent(out Interactable interactObj))
            {
                // Show that the object is in range to interact (this line is commented out)
                 interactObj.IsAccessable(hitInfo.collider.gameObject);

                // Execute interaction with the object
                interactObj.Interact(hitInfo.collider.gameObject);
            }
        }
    }
}