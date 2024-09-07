
using System;
using UnityEngine;
public class PlayerRotation : MonoBehaviour {

    /// @brief Starting pitch (x-axis) rotation.
    private float xRotation = 0f;

    /// @brief Starting yaw (y-axis) rotation.
    private float yRotation = 0f;

    /// @brief Sensitivity of mouse movement for camera rotation.
    [SerializeField] public float mouseSensitivity;

    /// @brief Reference to the main camera transform for rotating the camera.
    [SerializeField] public Transform mainCam;

    public void Start(){
        // Hide mouse and lock to screen center
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

        /**
     * @brief Handles camera rotation based on mouse input.
     * 
     * This method is called after all Update functions have been called. It captures mouse movement input and calls 
     * rotationUpdate() to update the camera rotation, provided motion is not disabled.
     */
    public void LateUpdate() {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        rotationUpdate(mouseX, mouseY);
    }

    /**
     * @brief Updates the camera rotation based on mouse movement.
     * 
     * @param mouseX Horizontal mouse movement.
     * @param mouseY Vertical mouse movement.
     * 
     * This method updates the player's camera rotation by applying pitch and yaw rotations. The camera's pitch rotation
     * (looking up and down) is clamped to prevent extreme angles.
     */
    public void rotationUpdate(float mouseX, float mouseY){
        // Apply pitch rotation (looking up and down)
        xRotation -= mouseY;
        yRotation += mouseX;
        xRotation = Mathf.Clamp(xRotation, -20f, 50f); // Can't look too far up or down
        yRotation = Mathf.Clamp(yRotation, -60f, 60f); // Can't look too far up or down

        // Combine both rotations
        mainCam.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}