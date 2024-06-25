using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Interaction component using Ray tracing to calculate the position of the 
 * hit object
 * 
 * @author
 *  Timo Skrobanek
 */
interface Interactable
{
    public void interact();
}

public class InteractionComponent : MonoBehaviour
{
    public Transform interactor; //src transformation
    public float InteractRange = 5; //Maximum range to interact with object


    void Update()
    {

      //send a ray to searched object
        Ray directedRay = new Ray(interactor.position, interactor.forward);

        //If the obejct is hit, execute the method
        if (Physics.Raycast(directedRay, out RaycastHit hitInfo, InteractRange))
        {
            if (hitInfo.collider.gameObject.TryGetComponent(out Interactable interactObj)) {
                interactObj.interact();
            }    
        }
    }
}
