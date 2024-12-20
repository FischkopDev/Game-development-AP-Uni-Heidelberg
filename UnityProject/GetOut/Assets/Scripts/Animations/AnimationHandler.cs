/**
 * @class AnimationHandler
 * @brief Behandelt die Animationen aller Szenen im Spiel und steuert diese abhängig vom aktuellen Spielstatus.
 * 
 * Diese Klasse überprüft regelmäßig den Zustand des Spiels und startet entsprechende Animationen für 
 * verschiedene Szenen. Die Klasse verwendet einen Animator, um Übergänge und Animationen basierend 
 * auf den Zuständen im StateManager zu steuern.
 * 
 * @author Timo Skrobanek
 * @date 28.10.2024
 * @version 2.0
 */

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationHandler : MonoBehaviour {
    
    /** Animator, der die verschiedenen Szenen-Animationen steuert. */
    public Animator animator;

    /**
     * @brief Initialisiert die Animationen beim Start des Spiels.
     *
     * Diese Methode überprüft den Spielstatus beim Start und startet ggf. die passende Animation.
     */
    public void Start() {
        CheckState();
        Debug.Log("Checking state for animation");
    }

    /**
     * @brief Aktualisiert die Animationen in jeder Frame-Iteration.
     *
     * Diese Methode wird in jedem Frame aufgerufen und prüft, ob Animationen abgeschlossen sind, 
     * um entsprechende Maßnahmen zu ergreifen.
     */
    public void Update() {
        FinishAnimation();
    }

    /**
     * @brief Überprüft den aktuellen Spielstatus und startet die passende Animation.
     *
     * Diese Methode wechselt zwischen den verschiedenen Spielzuständen und spielt die jeweilige Animation
     * basierend auf dem Zustand im StateManager.
     */
    public void CheckState() {
        switch (StateManager.state) {
            case StateManager.State.SCENE1_INTRO_ANIMATION:
                // Startet die Intro-Animation für Szene 1
                animator.Play("Scene1_Intro");
                break;

            case StateManager.State.SCENE3_OUTRO:
                // Startet die Outro-Animation für Szene 3
                animator.Play("Scene3_Outro");
                break;

            case StateManager.State.SCENE4_INTRO:
                // Startet die Intro-Animation für Szene 4
                animator.Play("Scene4_Intro");
                break;
        }
    }
    
    /**
     * @brief Beendet die Animation und führt Spielaktionen nach Abschluss der Animation durch.
     *
     * Diese Methode prüft, ob die laufende Animation beendet ist und schaltet dann den Animator ab.
     * Bei bestimmten Zuständen wird der Spielstatus aktualisiert und es wird ggf. eine neue Szene geladen.
     */
    private void FinishAnimation() {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Intro_Done")) {
            animator.enabled = false;
            StateManager.stopIntroAnimation();
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Scene3_Outro")) {
            Debug.Log("Loading Scene 4");
            animator.enabled = false;
            StateManager.state = StateManager.State.SCENE4_INTRO;
            SceneManager.LoadScene("Scene4");
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Scene4_Done")) {
            animator.enabled = false;
            StateManager.StopIntroAnimationScene4();
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Ghost_Appeared")) {
            animator.enabled = false;
            StateManager.state = StateManager.State.SCENE4;
        }
    }
}
