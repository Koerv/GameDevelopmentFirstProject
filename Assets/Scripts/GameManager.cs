using UnityEngine;
using System.Collections;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
    public int coins;
    public bool buyPhase = true;
    public bool wayDown = true;
    public UIManager uiManager;
    public Hero hero;
    public Boss selectedBoss;
    public int bossCosts;
    public int bossCount;
    public int stages;
    public int updateCosts;
    public BoardManager bm;
    public Grid grid;
    public bool bossGrabbed;
    public GameObject newBoss;

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
      //  bm.BoardSetup();
        uiManager.updateCoins();
        bossCount = 0;
        //boardScript.BoardSetup();
        grid.startInstantiation();
    }

    public void gameOver()
    {
        uiManager.showLoseScreen();
    }

    public void stageClear()
    {
        StartCoroutine(uiManager.showWinScreen());
        uiManager.stageNr.text = ("Stage " + hero.level);
        //player gets coins for defeating the hero! yeah
        coins += (int)(hero.level * 100 + hero.hp * 5 + hero.strength * 10 + hero.attSpeed * 10);
        uiManager.updateCoins();
    }
  

    public void EndBuyPhase()
    {

        buyPhase = false;
        //hero (Resources.Load("Hero"), new Vector3(0.48f, 1.47f, 0), Quaternion.identity) as GameObject;
        if (wayDown)
        {
            hero.transform.position = new Vector3(0.48f, 1.47f, 0);
        }
        hero.movSpeed = 0.015f;

    }

    public void StartBuyPhase()
    {
        buyPhase = true;
        uiManager.showBuyMenu();
    }

    public void upgradeBoss(Boss boss)
    {
        if (selectedBoss != null)
        {
            selectedBoss.GetComponent<SpriteRenderer>().sprite = selectedBoss.standardSprite;
        }

        selectedBoss = boss;
        selectedBoss.GetComponent<SpriteRenderer>().sprite = selectedBoss.selectedSprite;
    }

    public int getBossCosts()
    {
        return bossCount * 50 + 100;
    }

    public int getUpgradeCosts()
    {
        return (int)(Mathf.Pow(selectedBoss.level, 1.2f) * updateCosts);
    }

    public void checkEnd()
    {
        if(hero.level > stages)
        {
            buyPhase = false;
            selectedBoss = null;
            uiManager.showFinalWinScreen();
        }
    }
    

}
