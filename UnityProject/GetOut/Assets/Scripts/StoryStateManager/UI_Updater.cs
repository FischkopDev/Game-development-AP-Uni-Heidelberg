using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Updater : MonoBehaviour
{
    public TMP_Text task;

    public void Start(){
        Debug.Log("UI started");
    }
    public void Update(){
        UpdateUI_Task();
    }

    private void UpdateUI_Task(){
        switch(StateManager.state){
            case StateManager.State.SCENE1_INTRO_ANIMATION:
                task.SetText("");
                break;
            case StateManager.State.SCENE1:
                task.SetText("Ah shit. It's already late. I have to go to work ...*sigh* but I have to cleanup this place first");
                break;
            case StateManager.State.SCENE1_CLEANUP_DONE:
                task.SetText("Since I live alone in this house, everything is messed up but I'm ok with it... Now I have to get dressed before I can go to work");
                break;
            case StateManager.State.SCENE1_COMPLETED:
                task.SetText("Finally I can go to my office now");
                break;
            case StateManager.State.SCENE2:
                task.SetText("I'm way too late, my boss is gonna kill me.");
                break;
        }
    }
}
