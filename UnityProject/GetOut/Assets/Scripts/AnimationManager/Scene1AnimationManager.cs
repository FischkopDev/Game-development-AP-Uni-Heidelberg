using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene1AnimationManager : MonoBehaviour
{
    public Animator scene1Intro;
    // Start is called before the first frame update
    void Start()
    {
        // Hide mouse and lock to screen center
        scene1Intro.Play("Scene1_Intro");
    }

}
