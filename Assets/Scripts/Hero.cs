using UnityEngine;
using System.Collections;
using System;

public class Hero : MonoBehaviour
{

    public int level;
    public int hp;
    public int strength;
    public float attSpeed;
    public float movSpeed;
    public AudioClip fightingSound;
    public int attribute;

    float hAttackTime;
    float hTimeLeft;

    //position of hero on the tile layout
    public Vector2 layoutPosition;
    public Vector2 layoutStartPosition;

    //world position from which a new hero should start
    public Vector3 initialPosition;

    Boss currentBoss;

    public bool isFighting = false;

    public bool isStealthed = false;
    public bool isPoisoned = false;

    int poisonCounter = 0;

    bool dirChange = false;
    float sumTime = 0f;
    AudioSource deathSound;

    //Move direction
    public Vector3 moveDirection;
    Vector3 preFightPosition;

    //access Turning Points
    TurningPoint turningPoint;


    // Use this for initialization
    void Start()
    {

        //initialize stats (semi random), only level 1 for now
        level = 1;
        RerollStats();

        hAttackTime = 1 - this.attSpeed * 0.1f;
        deathSound = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        //if (!GameManager.instance.buyPhase && !isFighting)
        //{ 
        //    transform.Translate(moveDirection);
        //}

        //if (dirChange)
        //{
        //    waitAndChangeDir();
        //}
        if (isFighting)
        {

            hTimeLeft -= Time.deltaTime;

            //animation for smoother movement
            if (hTimeLeft <= (hAttackTime / 2))
            {
                Vector3.MoveTowards(this.transform.position, preFightPosition, 5f);
            }
            //for moving back
            else
            {
                transform.position = (transform.position - moveDirection * hTimeLeft);
            }

            if (hTimeLeft <= 0)
            {
                //set position back to where hero was before the fight
                transform.position = preFightPosition;
             
                currentBoss.hp -= strength;
                Debug.Log("Boss HP: " + currentBoss.hp);
                if (currentBoss.hp <= 0)
                {
                    isFighting = false;
                    currentBoss.isFighting = false;
                    //sonst gibts ne Null-Reference
                    if (GameManager.instance.selectedBoss == currentBoss)
                    {
                        GameManager.instance.selectedBoss = null;
                    }
                    Destroy(currentBoss.gameObject);
                    GameManager.instance.bossCount -= 1;


                    //currentBoss.transform.position = new Vector3(4.5f, 0.8f);
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
        if (collision.name.Contains("Entrance") && !GameManager.instance.wayDown)
        {
            this.movSpeed = 0.0f;
            moveDirection = new Vector3(0, -movSpeed, 0);
            GameManager.instance.gameOver();
        }

    }

    public void Defeated()
    {

        isFighting = false;
        transform.position = new Vector3(-4.5f, 0.8f);
        movSpeed = 0.0f;
        deathSound.Play();
        level++;
        RerollStats();
        GameManager.instance.wayDown = true;
        if (level > GameManager.instance.stages)
        {
            GameManager.instance.checkEnd();
            Destroy(gameObject);
        }

    }

    private void RerollStats()
    {
        attribute = UnityEngine.Random.Range(0, 3);

        hp = (int)(15 + level*3 + Mathf.Round(UnityEngine.Random.Range(0f, level)));
        strength = (int)(1 + level + Mathf.Round(UnityEngine.Random.Range(0f, level)));
        attSpeed = (1.0f + level + Mathf.Round(UnityEngine.Random.Range(0f, level)))*0.8f;
        movSpeed = 0.015f;
        Debug.Log("New Heros Stats: Level: " + level + ", HP: " + hp + ", STR: " + strength + ", SPD: " + attSpeed + ", attribute: " + GameManager.instance.attrToString(attribute));
        moveDirection = new Vector3(0, -movSpeed, 0);
    }

    public Vector3 getMoveDirection()
    {
        return moveDirection;
    }

    public void lookForNewDirection(Grid grid) { }

    public void applyPoison()
    {
        poisonCounter++;
        if (poisonCounter >= 4)
        {
            hp--;
            poisonCounter = 0;
        }
    }

    public void undoPotionEffects()
    {
        isStealthed = false;
        isPoisoned = false;
    }

    
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.name.Contains("Boss") && !GameManager.instance.buyPhase)
        {
            //add boss interaction here
            isFighting = true;
            //store position so that after attacking the hero is at the same place he used to be before
            preFightPosition = transform.position;
            currentBoss = collision.collider.GetComponentInParent<Boss>();          
        }
        if (collision.collider.name.Contains("Potion"))
        {
            Potion currentPotion = collision.collider.GetComponentInParent<Potion>();
            currentPotion.activate(this);
            Debug.Log("Hero HP: " + hp);
            Destroy(currentPotion.gameObject);
        }
        if (collision.collider.name.Contains("cage"))
        {
            GameManager.instance.wayDown = false;
            GameManager.instance.StartBuyPhase();
            //if(GameManager.instance.coins < GameManager.instance.getBossCosts())
            //{
            //    GameManager.instance.coins += GameManager.instance.getBossCosts();
            //}
            moveDirection = new Vector3(0, movSpeed, 0);
        }
    }

    
    
}
