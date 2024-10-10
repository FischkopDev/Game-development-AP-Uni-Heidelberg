using UnityEngine;

public class Smiler_JS : MonoBehaviour
{

    public GameObject smiler;
    

    // Update is called once per frame
    void Update()
    {
        if(StateManager.state == StateManager.State.SCENE4){
            smiler.GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
