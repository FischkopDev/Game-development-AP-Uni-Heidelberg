using UnityEngine;

/*
    @description
        Resulting action when interacting with a surrounding objects to cleanup
        the house.

    @author
        Timo Skrobanek
    @date
        17.8.2024
*/
public class Cleanup_Interactor : MonoBehaviour, Interactable
{

    public void Interact(GameObject obj){
        if(Input.GetKeyDown(KeyCode.E)){
            //On interaction disable rendering
            obj.SetActive(false);
            Debug.Log("Object disabled");
        }
    }

    public void IsAccessable(){

    }
}
