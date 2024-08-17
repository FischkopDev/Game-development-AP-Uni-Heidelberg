using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour {

    [SerializeField] private float speed;
    [SerializeField] private float gravity;
    [SerializeField] private float runMultiplier = 2; //Multiply movement speed by 2 when player runs

    private CharacterController charController;
    private float yVelocity;

    // Start rotation at 0
    private float xRotation = 0f;
    private float yRotation = 0f;
    [SerializeField] public float mouseSensitivity;
    [SerializeField] public Transform mainCam;

    public void Start() {
        charController = GetComponent<CharacterController>();
        
        // Hide mouse and lock to screen center
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    /*
        For each iteration check if player input is captured. If so call @PlayerMove()
    */
    public void Update() {
        
        // Allow player control of mouse sensitivity
        if (Input.GetKeyDown(KeyCode.Equals)) mouseSensitivity = Mathf.Clamp(mouseSensitivity + 20, 20f, 1000f);
        if (Input.GetKeyDown(KeyCode.Minus)) mouseSensitivity = Mathf.Clamp(mouseSensitivity - 20, 20f, 1000f);

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        PlayerMove(x , z);
    }

    /*
        Check for mouse movement and call @rotationUpdate()
    */
    public void LateUpdate() {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

       rotationUpdate(mouseX, mouseY);
    }

    /*
        Update Camera rotation after mouse movement.
    */
    public void rotationUpdate(float mouseX, float mouseY){
        // Apply pitch rotation (looking up and down)
        xRotation -= mouseY;
        yRotation += mouseX;
        xRotation = Mathf.Clamp(xRotation, -80f, 70f); // Can't look too far up or down

        // Apply yaw rotation (looking left and right)
        transform.Rotate(Vector3.up * mouseX);
        transform.Rotate(Vector3.left * mouseY);

        // Combine both rotations
        mainCam.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }

    /*
        Playermovement method. Calculation of next position depending on movement vector of
        x and z relative to camera. Furthermore, movement to gravity center is calculated here
        to keep player always grounded.
    */
    public void PlayerMove(float x, float z) {
        //current position
        Vector3 move = (transform.right * x + transform.forward * z).normalized;

        //store new position depending on speed and delta time
        move = move * speed * Time.deltaTime;

        //check if player is running
        if (Input.GetKey(KeyCode.LeftShift))
            move *= runMultiplier;

        //keep player grounded
        yVelocity += gravity * Time.deltaTime;
        move.y = yVelocity * Time.deltaTime;

        //Apply movement to player controller
        charController.Move(move);
    }


}
