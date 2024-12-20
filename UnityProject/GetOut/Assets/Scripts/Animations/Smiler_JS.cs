/**
 * @class Smiler_JS
 * @brief Controls the visibility of the "smiler" GameObject based on game state.
 * 
 * This class disables the MeshRenderer of the "smiler" object when the game enters SCENE4, 
 * making it invisible.
 */

using UnityEngine;

public class Smiler_JS : MonoBehaviour
{
    /** The GameObject representing the smiler character. */
    public GameObject smiler;

    /**
     * @brief Checks the game state and updates the smiler's visibility.
     * 
     * This method is called once per frame. It disables the MeshRenderer component of the
     * smiler object when the game state is set to SCENE4, hiding the smiler in that scene.
     */
    void Update()
    {
        if (StateManager.state == StateManager.State.SCENE4) {
            smiler.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
