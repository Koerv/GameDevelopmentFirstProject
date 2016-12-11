using UnityEngine;
using System.Collections;

public class SmartAndSneaky : Hero {

    public SmartAndSneaky()
    {

    }

    void Start()
    {
        level = GameManager.instance.stage;
        //RerollStats();

        deathSound = GetComponent<AudioSource>();
        RerollStats();
    }
    private void RerollStats()
    {
        attribute = UnityEngine.Random.Range(0, 3);

        hp = (int)(15 + level * 3 + Mathf.Round(UnityEngine.Random.Range(0f, level)));
        strength = (int)(1 + level + Mathf.Round(UnityEngine.Random.Range(0f, level)));
        attSpeed = (1.0f + level + Mathf.Round(UnityEngine.Random.Range(0f, level))) * 0.8f;
        movSpeed = 0.015f;
        Debug.Log("New Heros Stats: Level: " + level + ", HP: " + hp + ", STR: " + strength + ", SPD: " + attSpeed + ", attribute: " + GameManager.instance.attrToString(attribute));
        moveDirection = new Vector3(0, -movSpeed, 0);
        hAttackTime = 1 - this.attSpeed * 0.1f;
    }

    public override void chooseYourPath(float statsEast, float statsWest, int potionsEast, int potionsWest, Grid grid)
    {
        Debug.Log("Sum of stats east: " + grid.floorTiles[(int)layoutPosition.x, (int)layoutPosition.y].sumOfStatsEast + " Sum of stats west: " + grid.floorTiles[(int)layoutPosition.x, (int)layoutPosition.y].sumOfStatsWest);
        if (statsEast < statsWest)
        {
            GameManager.instance.wayEast = true;
            moveDirection = new Vector3(movSpeed, 0f, 0f);
        }
        else if (statsEast > statsWest)
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
        return "Smart and Sneaky";
    }
}
