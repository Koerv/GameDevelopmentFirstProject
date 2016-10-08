using UnityEngine;
using System.Collections;

public class Hero : MonoBehaviour
{

    int level;
    int hp;
    int strength;
    int attSpeed;
    float movSpeed;

    //access Turning Points
    TurningPoint turningPoint;


    // Use this for initialization
    void Start()
    {

        //initialize stats (semi random), only level 1 for now
        level = 1;
        hp = (int)(9 + level + Mathf.Round(Random.Range(0f, level)));
        strength = (int)(1 + level + Mathf.Round(Random.Range(0f, level)));
        attSpeed = (int)(1 + level + Mathf.Round(Random.Range(0f, level)));
        movSpeed = 0.01f;
        Debug.Log("Hero Stats: Level: " + level + ", HP: " + hp + ", STR: " + strength + ", SPD: " + attSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + new Vector3(0f, -movSpeed, 0f);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Contains("Floor"))
        {
            //move to a new direction (chosen Randomly)
            transform.position = turningPoint.directions[(int)(Mathf.Round(Random.Range(1, turningPoint.directions.Capacity)))];
            Debug.Log("list size = " + turningPoint.directions.Capacity);
        }

        if (collision.name.Contains("Boss"))
        {
            //add boss interaction here
        }
    }
}
