/**
 * Behandelt die Animationen aller Scenen. Diese werden abhängig vom aktuellen Stand im Spiel gestartet.
 * @author Timo Skrobanek
 * @date 8.10.2024
 * @version 1.0
 */
using UnityEngine;

public class AnimationHandler : MonoBehaviour {

    public Animator scene1Intro;

    public void Start() {
        //Check if player is in scene 1. If so play the intro animation
        if(StateManager.state == StateManager.State.SCENE1_INTRO_ANIMATION){
            scene1Intro.Play("Scene1_Intro");
        }
    }


    public void Update(){
        FinishAnimation();
    }
    
    private void FinishAnimation(){
        if(scene1Intro.GetCurrentAnimatorStateInfo(0).IsName("Intro_Done")){
            scene1Intro.enabled = false;
            StateManager.stopIntroAnimation();
        }
    }
}