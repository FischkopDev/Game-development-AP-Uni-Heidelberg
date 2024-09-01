/**
 * @class Interactable
 * @brief Handles the interact-action between player and gameobjects
 *
 * Using this interface a gameobject, has an interaction abbility
 *
 * @details 
 * Features:
 * - Trigger for interaction
 * - Receive the hit object
 * - Check if the object is in a specific range for interaction
 *
 * Requirements:
 * - When the object including this interface is hit by the ray sent from the user @Interact() is executed
 * - When the object including this interface is in a predefined distance from the interactor @IsAccessable() is executed
 *
 * How to Use:
 * Extend a class by this interface and give it to the gameobject as component. Finally give the player 
 * @PlayerInteractionComponent resulting in a working system.
 *
 * @author Timo Skrobanek
 * @date 18.8.2024
 * @version 1.0
 */
using UnityEngine;
interface Interactable
{
    /*
        @brief executed when the interaction with the given object is going on right now
        
        @param obj GameObject that is aimed for from users view.
    */
    public void Interact(GameObject obj);

    /*
        @brief executed when the interaction with the given object is possible from a certain distance
        
    */
    public void IsAccessable(GameObject obj);
}
