using NUnit.Framework;
using UnityEngine;

public class InteractionTest
{
    private GameObject player;
    private InteractionComponent interactionComponent;

    [SetUp]
    public void Setup()
    {
        // Setup a new GameObject with the InteractionComponent
        player = new GameObject("Player");
        interactionComponent = player.AddComponent<InteractionComponent>();
    }

    [TearDown]
    public void Teardown()
    {
        // Clean up after tests
        GameObject.Destroy(player);
    }

    [Test]
    public void CheckInteractionFailed()
    {
        // Arrange
        GameObject myObject = new GameObject("Interactor");
        Transform myTransform = myObject.transform;
        myTransform.position = new Vector3(1, 1, 1);
        myTransform.rotation = Quaternion.Euler(0f, 90f, 0f); // Rotated by 90° so no interaction

        interactionComponent.SetInteractor(myTransform);

        // Act
        Ray directedRay = new Ray(interactionComponent.GetInteractor().position, interactionComponent.GetInteractor().forward);
        bool result = interactionComponent.CheckInteraction(directedRay);

        // Assert
        Assert.IsFalse(result);
    }

    [Test]
    public void CheckInteractionSuccessful()
    {
            // Arrange
        // Create the interactor GameObject
        GameObject interactorObject = new GameObject("Interactor");
        Transform interactorTransform = interactorObject.transform;
        interactorTransform.position = Vector3.zero; // Position at (0, 0, 0)
        interactorTransform.rotation = Quaternion.identity; // Default rotation

        // Create the target interactable object
        GameObject targetObject = GameObject.CreatePrimitive(PrimitiveType.Cube); // Cube with a collider
        targetObject.transform.position = new Vector3(0, 0, 5); // Place directly in front of the interactor
        targetObject.AddComponent<Computer_Interactor>(); // Add the Interactable component

        // Ensure the interaction component is properly initialized
        interactionComponent.SetInteractor(interactorTransform);
        interactionComponent.interactRange = 10f; // Set a range that includes the target

        // Act
        // Create a ray from the interactor, pointing forward
        Ray directedRay = new Ray(interactorObject.transform.position, interactorObject.transform.forward);
        bool result = interactionComponent.CheckInteraction(directedRay);

        // Assert
        Assert.IsTrue(result);

        // Cleanup
        Object.DestroyImmediate(interactorObject);
        Object.DestroyImmediate(targetObject);
    }

}
