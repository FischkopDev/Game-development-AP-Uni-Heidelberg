using UnityEngine;
using UnityEngine.SceneManagement;

/*
    @description
        Resulting action when interacting with a surrounding wine bottle

    @author
        Timo Skrobanek
    @date
        17.8.2024
*/
public class Scene1_Change_Interactor : MonoBehaviour, Interactable
{

    public void Interact(GameObject obj){
        if(Input.GetKeyDown(KeyCode.E)){
            ChangeToScene2();
            Debug.Log("Change of scene");
        }
    }

    public void IsAccessable(GameObject obj){

    }

    void ChangeToScene2(){
         SceneManager.LoadScene("Scene2");
    }
}