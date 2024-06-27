using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    This class handles events triggere by position of the player.
    For this the requested position needs to be covered by an object having
    a collider component (Attribute isTrigger = true).
    This class needs to be assigned to the player.

*/
public interface PlayerEventHandler {
    public void execute();
}

public class TriggerEvent : MonoBehaviour
{

    public string ColliderName;

    public void OnTriggerEnter(Collider other){
        if(other.gameObject.name == ColliderName){
            if (other.TryGetComponent(out PlayerEventHandler eventhandler)) {
               eventhandler.execute();
            } 
            else{
                Debug.LogError("The assigned Component needs EventHandler!");
            }  
        }
    }
}
