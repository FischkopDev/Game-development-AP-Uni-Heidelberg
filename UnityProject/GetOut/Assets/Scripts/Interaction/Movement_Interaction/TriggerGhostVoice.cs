/**
 * @class TriggerGhostVoice
 * @brief Plays a ghost voice sound effect when the player enters a specified trigger area.
 * 
 * This class activates an audio source when the player enters the trigger zone, playing a 
 * sound effect once and preventing further activation.
 * 
 * @author Timo Skrobanek
 * @date 28.10.2024
 * @version 1.0
 */

using UnityEngine;

public class TriggerGhostVoice : MonoBehaviour
{
    /** Audio source that plays the ghost voice sound effect. */
    public AudioSource src;
    
    /** Boolean to ensure the audio plays only once upon initial trigger. */
    private bool trigger = true;

    /**
     * @brief Plays the ghost voice audio when the player enters the trigger zone.
     * 
     * This method checks if the colliding object has the "Player" tag and if the audio 
     * hasn't already been triggered. If both conditions are true, it plays the audio 
     * and disables further triggers.
     * 
     * @param other The Collider of the object entering the trigger zone.
     */
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && trigger) {
            src.Play();
            Debug.Log("Player entered the trigger!");
            trigger = false;
        }
    }
}
