/**
 * @class PlayerMovement
 * @brief Handles the movement logic for the player character in Unity.
 *
 * This script allows the player to move in various directions using keyboard input,
 * and it manages both walking and running speeds. The script is designed to work 
 * with Unity's Rigidbody component for physics-based movement.
 *
 * @details 
 * Features:
 * - Supports basic movement (forward, backward, left, right) using WASD or arrow keys.
 * - Allows the player to sprint by holding down a designated key (e.g., Left Shift).
 * - Implements smooth movement by interpolating the player's velocity.
 * - Adjusts movement speed based on whether the player is walking or running.
 * - Includes optional jump functionality if the player is grounded.
 *
 * Requirements:
 * - This script should be attached to a GameObject with a Rigidbody component.
 * - The GameObject should also have a Collider component for proper collision detection.
 *
 * How to Use:
 * - Attach the PlayerMovement script to your player GameObject.
 * - Configure movement speed, running speed, and jump force in the Unity Inspector.
 * - Ensure the Rigidbody component is set to "Interpolate" for smooth movement.
 * - Set the correct input axes in the Unity Input Manager (default is WASD or arrow keys).
 *
 * @author Timo Skrobanek
 * @date 18.8.2024
 * @version 1.0
 */
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour {

    /// @brief Movement speed of the player.
    [SerializeField] private float speed;

    /// @brief Gravity force applied to the player.
    [SerializeField] private float gravity;

    /// @brief Multiplier for the player's speed when running.
    [SerializeField] private float runMultiplier = 2; // Multiply movement speed by 2 when player runs

    private CharacterController charController; ///< Reference to the CharacterController component.
    private float yVelocity; ///< Current vertical velocity due to gravity.

    /// @brief Starting pitch (x-axis) rotation.
    private float xRotation = 0f;

    /// @brief Starting yaw (y-axis) rotation.
    public float yRotation = -90f;

    /// @brief Sensitivity of mouse movement for camera rotation.
    [SerializeField] public float mouseSensitivity;

    /// @brief Reference to the main camera transform for rotating the camera.
    [SerializeField] public Transform mainCam;

    /**
     * @brief Initializes the PlayerMovement script.
     * 
     * This method is called on the first frame when the script is enabled. It sets up the CharacterController component,
     * locks the cursor to the center of the screen, and plays the introductory animation.
     */
    public void Start() {
        charController = GetComponent<CharacterController>();
        
        // Hide mouse and lock to screen center
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    /**
     * @brief Handles player input for movement.
     * 
     * This method is called once per frame. It checks if the introductory animation has finished, then captures player input
     * for movement and calls the PlayerMove() method to move the player accordingly.
     */
    public void Update() {
        if(StateManager.state != StateManager.State.SCENE1_INTRO_ANIMATION){
            float x = Input.GetAxisRaw("Horizontal");
            float z = Input.GetAxisRaw("Vertical");

            PlayerMove(x , z);
        }
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

        if(StateManager.state != StateManager.State.SCENE1_INTRO_ANIMATION)
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
        xRotation = Mathf.Clamp(xRotation, -80f, 70f); // Can't look too far up or down

        // Combine both rotations
        mainCam.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }

    /**
     * @brief Moves the player based on input.
     * 
     * @param x Horizontal movement input.
     * @param z Vertical movement input.
     * 
     * This method calculates the player's movement vector based on the input axes and the player's orientation relative to the camera.
     * It also applies gravity to keep the player grounded and checks if the player is running to adjust the movement speed.
     */
    public void PlayerMove(float x, float z) {
        // Current position
        Vector3 move = (transform.right * x + transform.forward * z).normalized;

        // Store new position depending on speed and delta time
        move = move * speed * Time.deltaTime;

        // Check if player is running
        if (Input.GetKey(KeyCode.LeftShift))
            move *= runMultiplier;

        // Keep player grounded
        yVelocity += gravity * Time.deltaTime;
        move.y = yVelocity * Time.deltaTime;

        // Apply movement to player controller
        charController.Move(move);
    }

}
