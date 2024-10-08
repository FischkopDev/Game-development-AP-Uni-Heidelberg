/**
 * Behandelt die Animationen aller Scenen. Diese werden abhängig vom aktuellen Stand im Spiel gestartet.
 * @author Timo Skrobanek
 * @date 8.10.2024
 * @version 1.0
 */
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationHandler : MonoBehaviour {

    public Animator animator;

    public void Start() {
        CheckState();
        Debug.Log("Checking state");
    }


    public void Update(){
        FinishAnimation();
    }

    public void CheckState(){
        switch(StateManager.state){
            case StateManager.State.SCENE1_INTRO_ANIMATION:
                //Check if player is in scene 1. If so play the intro animation
                animator.Play("Scene1_Intro");
                break;

            case StateManager.State.SCENE3_OUTRO:
                animator.Play("Scene3_Outro");
                break;
            
            case StateManager.State.SCENE4_INTRO:
                animator.Play("Scene4_Intro");
                break;
        }
    }
    
    private void FinishAnimation(){
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("Intro_Done")){
            animator.enabled = false;
            StateManager.stopIntroAnimation();

        }
        else if(animator.GetCurrentAnimatorStateInfo(0).IsName("Scene3_Outro")){
            Debug.Log("Loading Scene 4");
            animator.enabled = false;
            StateManager.state = StateManager.State.SCENE4_INTRO;
            SceneManager.LoadScene("Scene4");
        }
        else if(animator.GetCurrentAnimatorStateInfo(0).IsName("Scene4_Done")){
            animator.enabled = false;
            StateManager.state = StateManager.State.SCENE4;
        }
    }
}