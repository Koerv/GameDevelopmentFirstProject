﻿using UnityEngine;
using System.Collections;
using System;

public class Hero : MonoBehaviour
{

    public int level;
    public int hp;
    public int strength;
    public int attSpeed;
    float movSpeed;

    float hAttackTime;
    float hTimeLeft;

    Boss currentBoss;

    bool isFighting = false;

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
        transform.Translate(moveDirection);

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
            moveDirection = turningPoint.directions[UnityEngine.Random.Range(0, turningPoint.directions.Count)] * movSpeed;
 
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
    }

    
    
}
