using UnityEngine;
using System.Collections;
using System;

public class Hero : MonoBehaviour
{

    public int level;
    public int hp;
    public int strength;
    public int attSpeed;
    public float movSpeed;

    float hAttackTime;
    float hTimeLeft;

    Boss currentBoss;

    bool isFighting = false;

    bool dirChange = false;
    float sumTime = 0f;

    //Move direction
    Vector3 moveDirection;

    //access Turning Points
    TurningPoint turningPoint;


    // Use this for initialization
    void Start()
    {

        //initialize stats (semi random), only level 1 for now
        level = 1;
        hp = (int)(15 + level + Mathf.Round(UnityEngine.Random.Range(0f, level)));
        strength = (int)(1 + level + Mathf.Round(UnityEngine.Random.Range(0f, level)));
        attSpeed = (int)(1 + level + Mathf.Round(UnityEngine.Random.Range(0f, level)));
        movSpeed = 0.01f;
        Debug.Log("Hero Stats: Level: " + level + ", HP: " + hp + ", STR: " + strength + ", SPD: " + attSpeed);
        moveDirection = new Vector3(0, -movSpeed, 0);

        hAttackTime = 1 - this.attSpeed * 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.buyPhase)
        { 
            transform.Translate(moveDirection);
        }

        if (dirChange)
        {
            waitAndChangeDir();
        }
        if (isFighting)
        {

            hTimeLeft -= Time.deltaTime;

            if (hTimeLeft <= 0)
            {

                currentBoss.hp -= strength;
                Debug.Log("Boss HP: " + currentBoss.hp);
                if (currentBoss.hp <= 0)
                {
                    isFighting = false;
                    currentBoss.isFighting = false;
                    //TODO: Boss should not be destroyed but removed from the dungeon
                    //Destroy(currentBoss.gameObject);
                    //currentBoss.enabled = false;
                    currentBoss.transform.position = new Vector3(4.5f, 0.8f);
                    Debug.Log("Stirb!");
                }
                //reset Attack Time
                hTimeLeft = hAttackTime;
            }
        }
    }

    public static explicit operator Hero(GameObject v)
    {
        throw new NotImplementedException();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Contains("Floor"))
        {
            //move to a new direction (chosen Randomly)
            turningPoint = collision.GetComponent<TurningPoint>();
            Debug.Log("dirChange starts");
            dirChange = true;

            //yield return new WaitForSeconds(movSpeed * 5);
            //moveDirection = turningPoint.newDirection() * movSpeed;
        }
    }

    void waitAndChangeDir()
    {
        sumTime += Time.deltaTime;
        if (sumTime >= movSpeed * 40)
        {
            sumTime = 0f;
            moveDirection = turningPoint.newDirection() * movSpeed;
            dirChange = false;
            Debug.Log("dirChange over");
        }
    }

    
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.name.Contains("Boss"))
        {
            //add boss interaction here
            isFighting = true;
            currentBoss = collision.collider.GetComponentInParent<Boss>();
            Debug.Log(currentBoss.hp);
        }
        if (collision.collider.name.Contains("cage"))
        {
            GameManager.instance.wayDown = false;
            moveDirection = new Vector3(0, movSpeed, 0);
        }
    }

    
    
}
