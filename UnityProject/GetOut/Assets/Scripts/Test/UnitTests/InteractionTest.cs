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
        GameObject myObject = new GameObject("Interactor");
        Transform myTransform = myObject.transform;
        myTransform.position = new Vector3(0, 0, 0);
        myTransform.rotation = Quaternion.Euler(0f, 0f, 0f); // Aligned for interaction

        interactionComponent.SetInteractor(myTransform);

        // Act
        Ray directedRay = new Ray(interactionComponent.GetInteractor().position, interactionComponent.GetInteractor().forward);
        bool result = interactionComponent.CheckInteraction(directedRay);

        // Assert
        Assert.IsTrue(result);
    }
}
