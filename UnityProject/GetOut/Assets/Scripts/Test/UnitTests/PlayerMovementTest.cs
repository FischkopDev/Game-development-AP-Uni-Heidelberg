using System.Collections;
using System.Runtime.CompilerServices;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerMovementTests
{
    private GameObject playerGameObject;
    private PlayerMovement playerMovement;
    private CharacterController characterController;

    [SetUp]
    public void Setup()
    {
        // Create a GameObject to attach the PlayerMovement script
        playerGameObject = new GameObject("Player");
        characterController = playerGameObject.AddComponent<CharacterController>();
        playerMovement = playerGameObject.AddComponent<PlayerMovement>();

        // Set default values for serialized fields
        playerMovement.SetGravity(-9.81f);
        playerMovement.SetRunMultiplier(2f);
        playerMovement.SetMouseSensitivity(100f);
    


        // Create a mock camera Transform
        GameObject cameraObject = new GameObject("MainCamera");
        playerMovement.mainCam = cameraObject.transform;

        // Position character controller for the test
        characterController.center = new Vector3(0, 1, 0);
    }

    [TearDown]
    public void Teardown()
    {
        Object.Destroy(playerGameObject);
    }

    [Test]
    public void PlayerMove_NormalMovement()
    {
        // Arrange
        float inputX = 1f; // Simulate "D" key for right movement
        float inputZ = 0f; // No forward movement

        // Act
        Vector3 calcMove = playerMovement.PlayerMove(inputX, inputZ);

        // Assert
        Vector3 expectedMovement = new Vector3(inputX, 0, 0).normalized * playerMovement.GetSpeed() * Time.deltaTime;
        Assert.AreEqual(expectedMovement.x, calcMove.x, 0.01f, "Player should move right.");
    }

    [Test]
    public void PlayerMove_WithRunning_SpeedIncreases()
    {
        // Arrange
        float inputX = 0f; // No horizontal movement
        float inputZ = 1f; // Simulate "W" key for forward movement

        // Act
        Vector3 calcMove = playerMovement.PlayerMove(inputX, inputZ);

        // Assert
        Vector3 expectedMovement = new Vector3(0, 0, 1).normalized * playerMovement.GetSpeed() * playerMovement.GetRunMultiplier() * Time.deltaTime;
        Assert.AreEqual(expectedMovement.z, calcMove.z, 0.01f, "Player should move faster while running.");
    }

    [Test]
    public void Gravity_AppliedCorrectly()
    {
        // Arrange input
        float inputX = 5f;
        float inputZ = 0f;

        //temporally store previous z-component
        float z = playerMovement.transform.position.z;

        // Calculate new position
        Vector3 move = playerMovement.PlayerMove(inputX, inputZ);
        Assert.IsTrue(move.z == z);
    }

    [Test]
    public void CameraRotation_LimitedCorrectly()
    {
        // Arrange
        float mouseX = 10f;
        float mouseY = -20f;

        // Act
        playerMovement.rotationUpdate(mouseX, mouseY);

        // Assert
        Assert.IsTrue(playerMovement.mainCam.localEulerAngles.x >= -80f && playerMovement.mainCam.localEulerAngles.x <= 70f,
            "Camera pitch should be clamped between -80 and 70 degrees.");
    }

    [UnityTest]
    public IEnumerator Cursor_LockedOnStart()
    {
    // Arrange: Setze den Cursor-Zustand explizit
    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;

    yield return null; // Ein Frame warten

    // Assert
    Assert.AreEqual(CursorLockMode.Locked, Cursor.lockState, "Cursor should be locked on start.");
    Assert.IsFalse(Cursor.visible, "Cursor should be invisible on start.");
    }

}
