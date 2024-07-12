using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour {

    [SerializeField] private float speed = 10;
    [SerializeField] private float gravity = -20f;
    [SerializeField] private float jumpHeight = 2;
    [SerializeField] private float runMultiplier = 2;
    [SerializeField, Range(0f, 90f)] private float jumpSlopeLimit;

    private CharacterController charController;
    private float jumpMult;
    private float yVelocity;
    private float originalSlopeLimit;
    private bool isGrounded;

    // Mouse look
    private float xRotation = 0f;
    private float yRotation = 0f;
    [SerializeField] public float mouseSensitivity;
    [SerializeField] public Transform mainCam;

    public void Start() {
        charController = GetComponent<CharacterController>();

        originalSlopeLimit = charController.slopeLimit;
        jumpMult = Mathf.Sqrt(jumpHeight * -2f * gravity);
        
        // Hide mouse and lock to screen center
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Update() {
        
        // Allow player control of mouse sensitivity
        if (Input.GetKeyDown(KeyCode.Equals)) mouseSensitivity = Mathf.Clamp(mouseSensitivity + 20, 20f, 1000f);
        if (Input.GetKeyDown(KeyCode.Minus)) mouseSensitivity = Mathf.Clamp(mouseSensitivity - 20, 20f, 1000f);

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        PlayerMove(x , z);
    }

    public void LateUpdate() {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

       rotationUpdate(mouseX, mouseY);
    }

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

    public void PlayerMove(float x, float z) {
        isGrounded = charController.isGrounded;
        if (charController.isGrounded || charController.collisionFlags == CollisionFlags.Above) yVelocity = -0.1f;

        if (charController.isGrounded) {
            charController.slopeLimit = originalSlopeLimit;
        }
        else {
            charController.slopeLimit = jumpSlopeLimit;
        }


        Vector3 move = (transform.right * x + transform.forward * z).normalized;
        move = move * speed * Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftShift)) move *= runMultiplier;

        if (Input.GetButtonDown("Jump") && charController.isGrounded) {
            yVelocity += jumpMult;
        }

        yVelocity += gravity * Time.deltaTime;

        move.y = yVelocity * Time.deltaTime;

        charController.Move(move);
    }


}
