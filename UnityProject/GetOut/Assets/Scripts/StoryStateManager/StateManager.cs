using UnityEngine;    
    public class StateManager : MonoBehaviour {
        //Achievements and their states
        public int CleanedItems = 0; 
        public bool outfit = false; 

        public State state = State.SCENE1;
        
        public bool ItemsLeftToCleanup(){
            return CleanedItems < 5;
        }

        public bool changedOutfit(){
            return outfit;
        }

        public void blockPlayerMovement(){
            GameObject player = GameObject.Find("Main Camera");
        }

        
    public enum State {
        SCENE1 = 0,
        SCENE1_READ_LETTER = 1,
        SCENE1_INTRO_ANIMATION = 2,
    }

    // from here on for Save and Load
    public static StateManager Instance {get; private set;}

    public SaveData currentGameState;

    void Awake()
    {
       if(Instance == null) 
       {
        Instance = this;
        DontDestroyOnLoad(gameObject);
       } 
       else 
       {
        Destroy(gameObject);
       }
    }

    public void UpdateGameState(SaveData newGameState)
    {
        currentGameState = newGameState;
    }
}

