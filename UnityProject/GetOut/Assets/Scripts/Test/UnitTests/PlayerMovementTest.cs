using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerMovementTest
{

    public PlayerMovement movement;

    // A Test behaves as an ordinary method
    [Test]
    public void Test_Rotation()
    {
        System.Random random = new System.Random();
        float x = (float)random.NextDouble();
        float y = (float)random.NextDouble();

        Transform playerTransform = movement.mainCam;
        movement.rotationUpdate(x, y);

        Assert.AreEqual(playerTransform, movement.mainCam);
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator PlayerMovementTestWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}
