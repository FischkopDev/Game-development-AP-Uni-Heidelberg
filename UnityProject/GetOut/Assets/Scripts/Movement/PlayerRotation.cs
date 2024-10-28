
using System;
using UnityEngine;
/**
 * @class PlayerRotation
 * @brief Handles the rotation logic for the player character in Unity.
 *
 * This script allows the player to rotate the camera inside a predefined angle.
 * In Scene 3 of the game, the player is only allowed to move his head. With a few
 * more requirements it's more efficient to have an disjoint script for head rotation.
 *
 * @details 
 * Features:
 * - Player rotation in x and y axes inside predefined angles
 *
 * Requirements:
 * - This script should be attached to a GameObject with a Rigidbody component.
 * - The GameObject should also have a Collider component for proper collision detection.
 *
 * How to Use:
 * - Attach the PlayerMovement script to your player GameObject.
 * - Configure rotation speed and limits.
 * - Set the correct input axes in the Unity Input Manager (default is WASD or arrow keys).
 *
 * @author Timo Skrobanek
 * @date 28.10.2024
 * @version 2.0
 */
public class PlayerRotation : MonoBehaviour {

    /// @brief Starting pitch (x-axis) rotation.
    //private float xRotation = 0f;

    /// @brief Starting yaw (y-axis) rotation.
    //private float yRotation = 0f;

    //updated:
    private float pitch = 0f;

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
        if (StateManager.state != StateManager.State.SCENE3_OUTRO)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            rotationUpdate(mouseX, mouseY);
        }
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
        /*yRotation -= mouseY;
        xRotation += mouseX;
        yRotation = Mathf.Clamp(yRotation, -20f, 50f); // Can't look too far up or down
        xRotation = Mathf.Clamp(xRotation, -60f, 60f); // Can't look too far up or down*/

        //updated version
        pitch -= mouseY;
        //limit looking up or down
        pitch = Mathf.Clamp(pitch, -20f, 50f); 

        // Rotate the camera around the y-axis (yaw) based on the horizontal mouse movement
        mainCam.Rotate(Vector3.up * mouseX);

        // Combine both rotations
        //mainCam.localRotation = Quaternion.Euler(yRotation, xRotation, 0f);

        //updated:
        mainCam.localRotation = Quaternion.Euler(pitch, mainCam.localRotation.eulerAngles.y, 0f);
    }
}