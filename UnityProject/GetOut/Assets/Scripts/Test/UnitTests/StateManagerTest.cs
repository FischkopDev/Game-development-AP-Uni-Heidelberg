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
    public void TestChangeOutfit()
    {
        // Act
        StateManager.changeOutfit();
        
        // Assert
        Assert.IsTrue(StateManager.Outfit, "Outfit should be true after calling changeOutfit.");
        Assert.AreEqual(StateManager.State.SCENE1_COMPLETED, StateManager.state, "State should change to SCENE1_COMPLETED after calling changeOutfit.");
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
    Assert.AreEqual(StateManager.State.SCENE1, StateManager.state, "State should change to SCENE1 after calling stopIntroAnimation.");
    Assert.IsTrue(StateManager.IntroToggle, "IntroToggle should be true after calling stopIntroAnimation.");
    }

   
}
