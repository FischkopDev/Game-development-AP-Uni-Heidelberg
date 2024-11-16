using NUnit.Framework;
using UnityEngine;

public class StateManagerTest : MonoBehaviour
{
    private StateManager stateManager;

    [SetUp]
    public void SetUp()
    {
        // Reset static variables before each test
        stateManager = new GameObject("StateManager").AddComponent<StateManager>();
    }

    [TearDown]
    public void TearDown()
    {
        // Clean up after tests
        GameObject.Destroy(stateManager.gameObject);
    }

 [Test]
    public void TestStartScene1()
    {
        // Arrange
        StateManager.startScene1();
        
        // Assert initial values
        Assert.AreEqual(StateManager.state, StateManager.State.SCENE1_INTRO_ANIMATION);
        Assert.AreEqual(StateManager.CleanedItems, 0);
        Assert.IsFalse(StateManager.Outfit);
        Assert.IsFalse(StateManager.IntroToggle);
    }

    [Test]
    public void TestItemsLeftToCleanup()
    {
        Assert.AreEqual(StateManager.ItemsLeftToCleanup(), StateManager.CleanedItems < 3); // There are items left to clean up (CleanedItems < 3)
    }


    [Test]
    public void TestChangeOutfit()
    {
        // Act
        StateManager.changeOutfit();
        
        // Assert
        Assert.IsTrue(StateManager.Outfit == true); // Outfit should be true after changeOutfit is called
        Assert.AreEqual(StateManager.state, StateManager.State.SCENE1_COMPLETED); // State should change to SCENE1_COMPLETED
    }

    [Test]
    public void TestScene1Complete()
    {
        // Act
        bool result = StateManager.scene1Complete();
        
        // Assert
        Assert.IsTrue(result && StateManager.CleanedItems == 3 && StateManager.Outfit); // scene1Complete should return true when CleanedItems == 3 and outfit == true
    }

    [Test]
    public void TestStopIntroAnimation()
    {

        // Act
        StateManager.stopIntroAnimation();

        // Assert
        Assert.AreEqual(StateManager.state, StateManager.State.SCENE1); // State should be SCENE1
        Assert.IsTrue(StateManager.IntroToggle); // introToggle should be true
    }

   
}
