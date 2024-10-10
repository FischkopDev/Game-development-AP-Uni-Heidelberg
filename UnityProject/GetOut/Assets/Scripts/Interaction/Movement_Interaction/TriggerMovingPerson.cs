/**
 * @author Timo Skrobanek
 * @date 10.10.2024
 * @version 1.0
 */
using UnityEngine;

public class TriggerMovingPerson : MonoBehaviour
{

    public AudioSource src;
    public GameObject tableUpsideDown;
    public GameObject table;
    private bool trigger = true;

 void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && trigger)
        {
            src.Play();
            Debug.Log("Player entered the trigger!");

            trigger = false;

            tableUpsideDown.SetActive(true);
            table.SetActive(false);
        }
    }

}
