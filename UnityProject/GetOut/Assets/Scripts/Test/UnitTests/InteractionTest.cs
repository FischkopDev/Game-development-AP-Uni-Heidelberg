using NUnit.Framework;
using UnityEngine;

public class InteractionTest
{
    private GameObject player;
    private InteractionComponent interactionComponent;

    [SetUp]
    public void Setup()
    {

    }

    [TearDown]
    public void Teardown()
    {
        GameObject.Destroy(player);
    }

    [Test]
    public void CheckInteractionFailed()
    {
        InteractionComponent interaction = new InteractionComponent();
        
        GameObject myObject = new GameObject("Interactor");
        Transform myTransform = myObject.transform;
        myTransform.position = new Vector3(1,1,1);
        myTransform.rotation = Quaternion.Euler(0f, 90f, 0f);//Rotated by 90° so no interaction

        interaction.SetInteractor(myTransform);

        Ray directedRay = new Ray(interaction.GetInteractor().position, interaction.GetInteractor().forward);

        Assert.AreEqual(interaction.CheckInteraction(directedRay), false);
    }

        [Test]
    public void CheckInteractionSuccessfull()
    {
        InteractionComponent interaction = new InteractionComponent();
        
        GameObject myObject = new GameObject("Interactor");
        Transform myTransform = myObject.transform;
        myTransform.position = new Vector3(0,0,0);
        myTransform.rotation = Quaternion.Euler(0f, 0f, 0f);//Rotated by 90° so no interaction

        interaction.SetInteractor(myTransform);

        Ray directedRay = new Ray(interaction.GetInteractor().position, interaction.GetInteractor().forward);

        Assert.AreEqual(interaction.CheckInteraction(directedRay), true);
    }
}


