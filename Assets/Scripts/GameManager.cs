using UnityEngine;
using System.Collections;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    //attributes for strength/weaknesses
    public const int ROCK = 0;
    public const int PAPER = 1;
    public const int SCISSORS = 2;
    
    public static GameManager instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
    public int coins;
    public bool buyPhase = true;
    public bool wayDown = true;
    public bool wayEast = false;
    public bool wayWest = false;
    bool stopMovement = false;
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

    public bool potionGrabbed=false;
    public GameObject newPotion;

    public float attributeModifier = 1.5f;

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
        //Call the InitGame function to initialize the first level 
        InitGame();
    }

    //Initializes the game for each level.
    void InitGame()
    {
        uiManager.updateCoins();
        bossCount = 0;
        grid.startInstantiation();
        StartBuyPhase();
    }

    public void gameOver()
    {
        stopMovement = true;
        uiManager.showLoseScreen();
    }

    public void stageClear()
    {
        StartCoroutine(uiManager.showWinScreen());
        uiManager.stageNr.text = ("Stage " + hero.level);
        //player gets coins for defeating the hero! yeah
        coins += (int)(hero.level * 100 + hero.hp * 5 + hero.strength * 10 + hero.attSpeed * 10);
        uiManager.updateCoins();

        ///reset posisiton of the hero to the entrance
        hero.layoutPosition = hero.layoutStartPosition;
        hero.transform.position = hero.initialPosition;

        //reset princess back into her cage
        grid.princess.transform.position = grid.princess.initialPosition;

        //every second stage: add potion to place in the dungeon
        if(hero.level %2 == 0)
        {
            potionGrabbed = true;
            GetComponent<AddPotion>().addPotion(Random.Range(0,4));
        }
    }
  

    public void EndBuyPhase()
    {

        buyPhase = false;
        hero.movSpeed = 0.015f;
        grid.calcSumOfStats();

    }

    public void StartBuyPhase()
    {
        buyPhase = true;
        //hero.GetComponent<Rigidbody2D>().isKinematic = false;
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

    public string attrToString(int attribute)
    {
        switch (attribute)
        {
            case 0:
                return "Rock";
            case 1:
                return "Paper";
            case 2:
                return "Scissors";
            default:
                return "No attribute";
        }
    }

    public float checkWeaknesses(Boss boss, Hero hero)
    {
        if (boss.attribute == ROCK)
        {
            if(hero.attribute == PAPER)
            {
                return 1/attributeModifier;
            }
            else if (hero.attribute == SCISSORS)
            {
                return attributeModifier;
            }
        }
        else if (boss.attribute == PAPER)
        {
            
            if (hero.attribute == SCISSORS)
            {
                return 1 / attributeModifier;
            }
          
            else if (hero.attribute == ROCK)
            {
                return attributeModifier;
            }
        }
        else
        {
            if (hero.attribute == ROCK)
            {
                return 1 / attributeModifier;
            }

            else if (hero.attribute == PAPER)
            {
                return attributeModifier;
            }
        }
        return 1f;
    }

    public int getBossCosts()
    {
        return bossCount * 50 + 100;
    }

    public int getUpgradeCosts()
    {
        return (int)(Mathf.Pow(selectedBoss.level, 1.3f) * updateCosts);
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

    void Update()
    {
        if (!buyPhase && !hero.isFighting && !stopMovement)
        {
            letTheHeroWalk();
        }
    }

    void letTheHeroWalk()
    {

        if (wayDown)
        {
            int southTileXCoord = (int)hero.layoutPosition.x+1;
            int southTileYCoord = (int)hero.layoutPosition.y;

            //default way: walk south
            if (grid.layout[southTileXCoord, southTileYCoord] != 0)
            {
                wayEast = wayWest = false;
                hero.moveDirection = new Vector3(0f, hero.movSpeed, 0f);
                hero.transform.position = Vector3.MoveTowards(hero.transform.position, grid.floorTiles[southTileXCoord, southTileYCoord].transform.position, hero.movSpeed);
                if (hero.transform.position == grid.floorTiles[southTileXCoord, southTileYCoord].transform.position)
                {
                    hero.layoutPosition = new Vector2(southTileXCoord, southTileYCoord);
                }

            }
            else
            {
                westOrEast();
            }
        }
        else
        {
            //walk north
            int northTileXCoord = (int)hero.layoutPosition.x - 1;
            int northTileYCoord = (int)hero.layoutPosition.y;
            if (grid.layout[northTileXCoord, northTileYCoord] != 0)
            {
                wayEast = wayWest = false;
                hero.moveDirection = new Vector3(0f, hero.movSpeed, 0f);
                hero.transform.position = Vector3.MoveTowards(hero.transform.position, grid.floorTiles[northTileXCoord, northTileYCoord].transform.position, hero.movSpeed);
                if (hero.transform.position == grid.floorTiles[northTileXCoord, northTileYCoord].transform.position)
                {
                    hero.layoutPosition = new Vector2(northTileXCoord, northTileYCoord);
                    if (hero.layoutPosition == hero.layoutStartPosition)
                    {
                        gameOver();
                    }
                }

            }
            else
            {
                westOrEast();
            }
        }

    }

    //makes decision if hero should move to the left or to the right
    void westOrEast()
    {
        float statsEast = grid.floorTiles[(int)hero.layoutPosition.x, (int)hero.layoutPosition.y].sumOfStatsEast;
        float statsWest = grid.floorTiles[(int)hero.layoutPosition.x, (int)hero.layoutPosition.y].sumOfStatsWest;

        //walk east if more stats on east
        int EastTileXCoord = (int)hero.layoutPosition.x;
        int EastTileYCoord = (int)hero.layoutPosition.y + 1;
        int WestTileYCoord = (int)hero.layoutPosition.y - 1;
        int WestTileXCoord = (int)hero.layoutPosition.x;

        if (EastTileYCoord > grid.gridSizeY)
        {
            wayWest = true;
        }
        else if (WestTileYCoord == 0)
        {
            wayEast = true;
        }
        //if no tiles on one side, walk to the opposite side
        else if (grid.floorTiles[EastTileXCoord, EastTileYCoord] == null)
        {
            wayWest = true;
        }
        else if (grid.floorTiles[WestTileXCoord, WestTileYCoord] == null)
        {
            wayEast = true;
        }
        
        //walk east if more stats on east
        if (!(wayEast || wayWest))
        {
            Debug.Log("Sum of stats east: " +grid.floorTiles[(int)hero.layoutPosition.x, (int)hero.layoutPosition.y].sumOfStatsEast + " Sum of stats west: " + grid.floorTiles[(int)hero.layoutPosition.x, (int)hero.layoutPosition.y].sumOfStatsWest);
            if (statsEast > statsWest)
            {
                wayEast = true;
                hero.moveDirection = new Vector3(hero.movSpeed, 0f, 0f);
            }
            else if(statsEast < statsWest)
            {
                wayWest = true;
                hero.moveDirection = new Vector3(-hero.movSpeed, 0f, 0f);
            }
            //if both values are equal
            else
            {


                if (Random.Range(0,2) == 1)
                {
                    wayWest = true;
                    hero.moveDirection = new Vector3(hero.movSpeed, 0f, 0f);
                }
                else
                {
                    wayEast = true;
                    hero.moveDirection = new Vector3(-hero.movSpeed, 0f, 0f);
                }
            }
        }
        if (wayEast)
        {
            walkEast();
        }
        if (wayWest)
        {

            walkWest();
        }
    }

    void walkEast(){

        int EastTileXCoord = (int)hero.layoutPosition.x;
        int EastTileYCoord = (int)hero.layoutPosition.y + 1;

        hero.transform.position = Vector3.MoveTowards(hero.transform.position, grid.floorTiles[EastTileXCoord, EastTileYCoord].transform.position, hero.movSpeed);

        if (hero.transform.position == grid.floorTiles[EastTileXCoord, EastTileYCoord].transform.position)
        {
            hero.layoutPosition = new Vector2(EastTileXCoord, EastTileYCoord);

        }
    }

    void walkWest()
    {
        int WestTileYCoord = (int)hero.layoutPosition.y - 1;
        int WestTileXCoord = (int)hero.layoutPosition.x;

      
        hero.transform.position = Vector3.MoveTowards(hero.transform.position, grid.floorTiles[WestTileXCoord, WestTileYCoord].transform.position, hero.movSpeed);

        if (hero.transform.position == grid.floorTiles[WestTileXCoord, WestTileYCoord].transform.position)
        {
            hero.layoutPosition = new Vector2(WestTileXCoord, WestTileYCoord);
        }
    }




}
