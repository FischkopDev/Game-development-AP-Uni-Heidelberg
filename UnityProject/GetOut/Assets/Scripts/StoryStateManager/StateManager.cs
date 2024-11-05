using UnityEngine;
using UnityEngine.SceneManagement;
public class StateManager : MonoBehaviour {
        //Achievements and their states
        private static  int CleanedItems = 0; 
        private static  bool outfit = false; 
        private static bool introToggle = false, intro4Toggle = false;

        public static State state = State.SCENE1_INTRO_ANIMATION; //Has to be changed for debugging when starting from a different state!

        public void Start(){
            Debug.Log("State of game loading: " + state);
            switch(SceneManager.GetActiveScene().buildIndex){
                case 1:
                    state = State.SCENE1_INTRO_ANIMATION;
                    break;
                case 2:
                    state = State.SCENE2;
                    Debug.Log("Loading Scene 2");
                    break;
                case 3:
                    state = State.SCENE3;
                    break;
                case 4:
                    state = State.SCENE4_INTRO;
                    break;
                case 5:
                    state = State.SCENE5;
                    break;
            }
        }
        
        public static bool ItemsLeftToCleanup(){
            return CleanedItems < 3;
        }

        public static void cleanUp(){
            CleanedItems++;
        }

        public static void changeOutfit(){
            outfit = true;
            state = State.SCENE1_COMPLETED;
        }
        public static bool outfitChanged(){
            return outfit;
        }

        public static bool scene1Complete(){
            return !ItemsLeftToCleanup() && outfitChanged();
        }

        public static void stopIntroAnimation(){
            if(!introToggle){
                state = State.SCENE1;
                introToggle = true;
            }
        }

        public static void StopIntroAnimationScene4(){
            if(!intro4Toggle){
                state = State.SCENE4;
                intro4Toggle = true;
            }
        }

        
    public enum State {
        SCENE1 = 0,
        SCENE1_INTRO_ANIMATION = 1,
        SCENE1_CLEANUP_DONE = 2,
        SCENE1__NOT_DRESSED = 3,
        SCENE1_COMPLETED = 4,

        SCENE2 = 5,
        SCENE2_COMPLETED = 7,
        SCENE3 = 6,

        SCENE3_OUTRO = 8,
        SCENE3_OUTRO_DONE = 9,
        SCENE4_INTRO = 10,
        SCENE4 = 11,
        SCENE4_GHOST_APPEAR = 12,
        SCENE5

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

