using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndAnimation : MonoBehaviour
{
    public void Start(){
        StartCoroutine(waitForEnd());
    }


    IEnumerator waitForEnd()
    {
        Debug.Log("Waiting for end");
        //Wait for 4 seconds
        yield return new WaitForSecondsRealtime(7);

        SceneManager.LoadScene("Credits");

    }
}
