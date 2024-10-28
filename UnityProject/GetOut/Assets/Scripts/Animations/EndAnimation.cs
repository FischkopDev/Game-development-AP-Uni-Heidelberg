/**
 * @class EndAnimation
 * @brief Handles the delay before transitioning to the credits scene after the end animation.
 * 
 * This class waits for a specified duration and then loads the "Credits" scene.
 * Useful for creating a timed transition after a concluding animation.
 */

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndAnimation : MonoBehaviour
{
    /**
     * @brief Starts the coroutine that waits before loading the next scene.
     * 
     * This method initiates the waitForEnd coroutine to handle the delay.
     */
    public void Start() {
        StartCoroutine(waitForEnd());
    }

    /**
     * @brief Coroutine that waits for a set period before loading the credits scene.
     * 
     * This coroutine waits for 7 seconds in real-time and then loads the "Credits" scene
     * via SceneManager.
     * 
     * @return IEnumerator Required for coroutine functionality.
     */
    IEnumerator waitForEnd() {
        Debug.Log("Waiting for end");
        
        // Wait for 7 seconds in real time
        yield return new WaitForSecondsRealtime(7);

        SceneManager.LoadScene("Credits");
    }
}
