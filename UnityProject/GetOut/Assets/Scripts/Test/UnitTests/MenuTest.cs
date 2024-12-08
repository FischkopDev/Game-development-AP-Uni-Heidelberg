using NUnit.Framework;
using UnityEngine;
using UnityEngine.Audio;

public class MenuTest : MonoBehaviour
{
    private SettingsMenu settings;

    [SetUp]
    public void SetUp()
    {
        settings = new SettingsMenu();
    }

    [TearDown]
    public void TearDown()
    {
        // Clean up after tests
        GameObject.Destroy(settings.gameObject);
    }

    [Test]
    public void SetVolume()
    {
        float volume = 5f;
        settings.SetVolume(volume);
    }
   
}
