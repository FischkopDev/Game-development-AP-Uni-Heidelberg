using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class MovementTest
{
    private GameObject player;
    private PlayerMovement playerMovement;
    private CharacterController charController;
    private Transform cameraTransform;

    [SetUp]
    public void Setup()
    {
        // Create a new GameObject and add necessary components
        player = new GameObject();
        charController = player.AddComponent<CharacterController>();
        playerMovement = player.AddComponent<PlayerMovement>();

        // Create a camera for the player
        GameObject camera = new GameObject();
        cameraTransform = camera.transform;
        playerMovement.mainCam = cameraTransform;

        // Initialize the PlayerMovement script
        playerMovement.mouseSensitivity = 100f;  // Set a default value for mouse sensitivity
    }

    [TearDown]
    public void Teardown()
    {
        Object.Destroy(player);
    }

    [Test]
    public void PlayerStartsWithCharacterController()
    {
        Assert.IsNotNull(charController);
    }

    [Test]
    public void PlayerMovement_OnStart_InitializesValues()
    {
        playerMovement.Start();

        Assert.AreEqual(CursorLockMode.Locked, Cursor.lockState);
        Assert.IsFalse(Cursor.visible);
    }

    [UnityTest]
    public IEnumerator PlayerMovement_RotatesCamera_OnMouseMovement()
    {
        playerMovement.Start();

        float initialXRotation = playerMovement.transform.eulerAngles.x;
        float initialYRotation = playerMovement.transform.eulerAngles.y;

        // Simulate mouse movement
        float mouseX = 10f;
        float mouseY = 5f;

        playerMovement.LateUpdate(); // Ensure rotation logic runs

        // Update the rotation using private method
        playerMovement.rotationUpdate(mouseX, mouseY);

        yield return null; // Wait for end of frame

        Assert.AreNotEqual(initialXRotation, playerMovement.transform.eulerAngles.x);
        Assert.AreNotEqual(initialYRotation, playerMovement.transform.eulerAngles.y);
    }

    [UnityTest]
    public IEnumerator PlayerMovement_Moves_OnInput()
    {
        playerMovement.Start();

        Vector3 initialPosition = player.transform.position;

        // Simulate input for movement
        playerMovement.PlayerMove(1, 1);

        yield return null; // Wait for end of frame

        Assert.AreNotEqual(initialPosition, player.transform.position);
    }

















/*
    Is jumping required by the game?
    [UnityTest]
    public IEnumerator PlayerMovement_Jumps_WhenGrounded()
    {
        playerMovement.Start();

        // Set player as grounded
        playerMovement.isGrounded = true;

        // Simulate jump input
        Input.SetButtonDown("Jump");

        playerMovement.Update();

        yield return null; // Wait for end of frame

        Assert.IsTrue(playerMovement.yVelocity > 0);
    }

    [UnityTest]
    public IEnumerator PlayerMovement_DoesNotJump_WhenNotGrounded()
    {
        playerMovement.Start();

        // Set player as not grounded
        playerMovement.isGrounded = false;

        // Simulate jump input
        Input.SetButtonDown("Jump");

        playerMovement.Update();

        yield return null; // Wait for end of frame

        Assert.IsFalse(playerMovement.yVelocity > 0);
    }

    */
}
