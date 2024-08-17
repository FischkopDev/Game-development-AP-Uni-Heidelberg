using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Interaction component using Ray tracing to calculate the position of the 
 * hit object
 *  Important: Apply a collider to the gameobject, otherwise the interaction won't work!!!
 * 
 * @author
 *  Timo Skrobanek
    
    @date
    17.8.2024
 */

public class InteractionComponent : MonoBehaviour
{
    public Transform interactor; //src transformation
    public float InteractRange = 5; //Maximum range to interact with object


    void Update()
    {
      //send a ray to searched object
        Ray directedRay = new Ray(interactor.position, interactor.forward);

        CheckInteraction(directedRay);
        
    }

    public void CheckInteraction(Ray directedRay){
        //Check if ray is sent and hit any object
        if (Physics.Raycast(directedRay, out RaycastHit hitInfo, InteractRange)){
           if(hitInfo.collider.gameObject.TryGetComponent(out Interactable interactObj)){
                //show that the object is in range to interact
               // interactObj.IsAccessable();

                //Execute interaction
                interactObj.Interact(hitInfo.collider.gameObject);
            
            }
        }
    }
}