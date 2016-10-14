using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
    public int coins;
    public bool buyPhase = true;
    public UIManager uiManager;
    //Awake is always called before any Start functions
    
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
        //Get a component reference to the attached BoardManager script
        //boardScript = GetComponent<BoardManager>();
      
        //Call the InitGame function to initialize the first level 
        InitGame();
    }

    //Initializes the game for each level.
    void InitGame()
    {
        //boardScript.BoardSetup();

    }

    public void EndBuyPhase()
    {
        GameObject hero;
        buyPhase = false;
        hero = Instantiate(Resources.Load("Hero"), new Vector3(0.48f, 1.47f, 0), Quaternion.identity) as GameObject;
    }

    public void StartBuyPhase()
    {
        buyPhase = true;
        uiManager.showBuyMenu();
    }

}
