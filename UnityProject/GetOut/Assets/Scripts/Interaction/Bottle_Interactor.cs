using UnityEngine;

/*
    @description
        Resulting action when interacting with a surrounding wine bottle

    @author
        Timo Skrobanek
    @date
        17.8.2024
*/
public class Bottle_Interactor : MonoBehaviour, Interactable
{

    public void Interact(GameObject obj){
        if(Input.GetKeyDown(KeyCode.E)){
            //On interaction disable rendering
            obj.SetActive(false);
            Debug.Log("Bottle disabled");
        }
    }

    public void IsAccessable(){

    }
}
