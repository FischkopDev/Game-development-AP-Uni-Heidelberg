using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeMenu : MonoBehaviour
{

    public Canvas canvas;
    private bool toggle = true;
    // Start is called before the first frame update
    public void Start()
    {
        canvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.T)){
            canvas.gameObject.SetActive(toggle);

            if(toggle){
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else{
                // Hide mouse and lock to screen center
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
    
    
            toggle = !toggle;
        }
    }

    public void ResumeToGame(){
        canvas.gameObject.SetActive(false);

        // Hide mouse and lock to screen center
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    public void ChangeToMenu(){
        StateManager.state = StateManager.State.SCENE1_INTRO_ANIMATION;
        SceneManager.LoadScene("MainMenu");
    }
}
