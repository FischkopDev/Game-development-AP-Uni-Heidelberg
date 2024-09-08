using UnityEngine;

public class InteractionComponent : MonoBehaviour
{
    /// @brief The Transform of the camera or the head where the raycast should start.
    public Transform interactor;

    /// @brief The maximum range within which the player can interact with objects.
    public float interactRange = 5f;

    /**
     * @brief Called once per frame to check for interactions.
     *
     * This method casts a ray from the interactor's (camera or head's) position forward,
     * checking if it hits any objects within the specified interaction range.
     */
    void Update()
    {
        // Ray originates from the interactor (camera/head) position, going forward in the camera's forward direction
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
        if (Physics.Raycast(directedRay, out RaycastHit hitInfo, interactRange))
        {
            // Check if the hit object has an Interactable component
            if (hitInfo.collider.gameObject.TryGetComponent(out Interactable interactObj))
            {
                // Optional: Show that the object is in range to interact
                interactObj.IsAccessable(hitInfo.collider.gameObject);

                // Execute interaction with the object
                interactObj.Interact(hitInfo.collider.gameObject);
            }
        }
    }
}
