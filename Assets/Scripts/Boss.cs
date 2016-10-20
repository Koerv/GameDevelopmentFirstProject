using UnityEngine;
using System.Collections;
using UnityEditor;

public class Boss : MonoBehaviour
{

    public int level;
    public int hp;
    public int strength;
    public float attSpeed;

    float bAttackTime;
    float bTimeLeft;

    public Hero hero;

    public bool isFighting = false;

    Vector3 preFightPosition;

    // Use this for initialization
    void Start()
    {
        //initialize stats (semi random), only level 1 for now
        level = 1;
        hp = (int)(9 + level + Mathf.Round(Random.Range(0f, level)));
        strength = (int)(1 + level + Mathf.Round(Random.Range(0f, level)));
        attSpeed = (1 + level + Mathf.Round(Random.Range(0f, level)))*0.9f;
        Debug.Log("Boss Stats: Level: " + level + ", HP: " + hp + ", STR: " + strength + ", SPD: " + attSpeed);

        bAttackTime = 1 - this.attSpeed * 0.1f;

    }

    // Update is called once per frame
    void Update()
    {
        if (isFighting)
        {
            bTimeLeft -= Time.deltaTime;
            //also make the boss move towards the hero, but not as strongly since bosses are HUGE and MIGHTY!
            transform.position = (transform.position + hero.getMoveDirection() * 0.4f * bTimeLeft);

            if (bTimeLeft <= 0)
            {
                transform.position = preFightPosition;
                hero.hp -= strength;
                Debug.Log("Hero HP: " + hero.hp);
                if (hero.hp <= 0)
                {
                    transform.position = preFightPosition;
                    isFighting=false;
                    hero.Defeated();
                    //Destroy(hero.gameObject);
                    GameManager.instance.stageClear();
                    GameManager.instance.StartBuyPhase();
                    Debug.Log("Stirb!");
                }
                //reset Attack Time
                bTimeLeft = bAttackTime;
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Kämpf!");
        //needed to assign Hero to runtime-generated bosses
        hero = collision.collider.GetComponentInParent<Hero>();
        isFighting = true;
        preFightPosition = transform.position;
    }

    void OnMouseDown()
    {
        //test purpose only
        //upgradeBoss();
        GameManager.instance.upgradeBoss(this);
    }

    public void upgradeBoss()
    {
        level += 1;
        hp = (int)(9 + level + Mathf.Round(Random.Range(0f, level)));
        strength = (int)(strength + Mathf.Round(Random.Range(0f, level)));
        attSpeed= (int)(attSpeed + Mathf.Round(Random.Range(0f, level)));
        Debug.Log("Boss Stats: Level: " + level + ", HP: " + hp + ", STR: " + strength + ", SPD: " + attSpeed);
    }
    
}
