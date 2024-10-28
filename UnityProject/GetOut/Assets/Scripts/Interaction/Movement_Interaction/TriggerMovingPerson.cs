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
    public Animator anim;

    public GameObject doorsOpen;
    public GameObject doorsClosed;
    private bool trigger = true;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && trigger)
        {
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
