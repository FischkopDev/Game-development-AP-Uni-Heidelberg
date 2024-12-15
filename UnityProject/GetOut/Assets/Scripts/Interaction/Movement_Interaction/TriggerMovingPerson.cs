/**
 * @class TriggerMovingPerson
 * @brief Handles triggering animations, sounds, and object states when the player enters a specific area.
 * 
 * This class initiates a sequence of actions when the player enters the trigger zone: 
 * it updates the game state, plays an audio clip, enables an animation, and toggles visibility 
 * of objects (e.g., tables and doors).
 * 
 * @details The script ensures that the sequence is only triggered once upon the first entry of 
 * the player into the trigger zone.
 * 
 * @author Timo Skrobanek
 * @date 10.10.2024
 * @version 1.0
 */

using UnityEngine;

public class TriggerMovingPerson : MonoBehaviour
{
    /** Audio source for playing a sound effect upon triggering. */
    public AudioSource src;
    
    /** GameObject representing the table in an upside-down state. */
    public GameObject tableUpsideDown;
    
    /** GameObject representing the table in its original position. */
    public GameObject table;
    
    /** Animator component that controls the movement of a character or object. */
    public Animator anim;
    
    /** GameObject representing open doors that will be toggled off. */
    public GameObject doorsOpen;
    
    /** GameObject representing closed doors that will be toggled on. */
    public GameObject doorsClosed;
    
    /** Boolean to ensure the trigger actions occur only once. */
    private bool trigger = true;

    /**
     * @brief Activates various effects and updates the game state when the player enters the trigger zone.
     * 
     * This method checks if the colliding object is tagged "Player" and if the trigger hasn't already been activated.
     * Upon triggering, it:
     * - Sets the game state to SCENE4_GHOST_APPEAR
     * - Plays the associated audio
     * - Enables the assigned animation
     * - Toggles table and door visibility
     * 
     * @param other The Collider of the object entering the trigger zone.
     */
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && trigger) {
            StateManager.state = StateManager.State.SCENE4_GHOST_APPEAR;
            src.Play();
            anim.enabled = true;
            Debug.Log("Player entered the trigger!");

            trigger = false;

            tableUpsideDown.SetActive(true);
            table.SetActive(false);

            doorsClosed.SetActive(true);
            doorsOpen.SetActive(false);
        }
    }
}
