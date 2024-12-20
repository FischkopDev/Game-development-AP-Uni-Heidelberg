using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Startbutton : MonoBehaviour
{

public Button yourButton;
   public void startGame(){
         SceneManager.LoadScene("Scene1");
   }
}
