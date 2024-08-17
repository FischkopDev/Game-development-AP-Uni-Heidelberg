using UnityEngine;

/*
    @description
        

    @author
        Timo Skrobanek
    @date
        17.8.2024
*/
public class Cloth_Interactor : MonoBehaviour, Interactable
{

    public void Interact(GameObject obj){
        if(Input.GetKeyDown(KeyCode.E)){
            //TODO implement animation
            Debug.Log("Change outfit");
        }
    }

    public void IsAccessable(){

    }

}