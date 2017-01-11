using UnityEngine;
using System.Collections;

public class GreedyAndGeeky : Hero {

    public GreedyAndGeeky()
    {

    }

    void Start()
    {
        level = GameManager.instance.stage;

        deathSound = GetComponent<AudioSource>();
        RerollStats();
    }
    private void RerollStats()
    {
        attribute = UnityEngine.Random.Range(0, 3);

        hp = (int)(15 + level * 3 + Mathf.Round(UnityEngine.Random.Range(0f, level)));
        strength = (int)(1 + level + Mathf.Round(UnityEngine.Random.Range(0f, level*0.7f)));
        attSpeed = (1.0f + level*0.3f + Mathf.Round(UnityEngine.Random.Range(0f, level*0.3f))) * 0.7f;
        movSpeed = 0.015f;
        Debug.Log("New Heros Stats: Level: " + level + ", HP: " + hp + ", STR: " + strength + ", SPD: " + attSpeed + ", attribute: " + GameManager.instance.attrToString(attribute));
        moveDirection = new Vector3(0, -movSpeed, 0);
        hAttackTime = 1 - this.attSpeed * 0.1f;
    }

    public override void chooseYourPath(float statsEast, float statsWest, int potionsEast, int potionsWest, Grid grid)
    {
        Debug.Log("Sum of potions east: " + grid.floorTiles[(int)layoutPosition.x, (int)layoutPosition.y].sumOfPotionsEast + " Sum of potions west: " + grid.floorTiles[(int)layoutPosition.x, (int)layoutPosition.y].sumOfPotionsWest);
        if (potionsEast > potionsWest)
        {
            GameManager.instance.wayEast = true;
            moveDirection = new Vector3(movSpeed, 0f, 0f);
        }
        else if (potionsEast < potionsWest)
        {
            GameManager.instance.wayWest = true;
            moveDirection = new Vector3(-movSpeed, 0f, 0f);
        }
        //if both values are equal
        else
        {


            if (Random.Range(0, 2) == 1)
            {
                GameManager.instance.wayWest = true;
                moveDirection = new Vector3(-movSpeed, 0f, 0f);
            }
            else
            {
                GameManager.instance.wayEast = true;
                moveDirection = new Vector3(movSpeed, 0f, 0f);
            }
        }
    }

    public override string getName()
    {
        return "Greedy and Geeky";
    }
}
