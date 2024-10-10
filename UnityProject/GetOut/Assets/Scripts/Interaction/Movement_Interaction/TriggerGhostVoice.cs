/**
 * @author Timo Skrobanek
 * @date 10.10.2024
 * @version 1.0
 */
using UnityEngine;

public class TriggerGhostVoice : MonoBehaviour
{

    public AudioSource src;
    private bool trigger = true;
 void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && trigger)
        {
            src.Play();
            Debug.Log("Player entered the trigger!");
            trigger = false;
        }
    }

}
