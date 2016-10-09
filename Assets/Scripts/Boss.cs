using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour
{

    public int level;
    public int hp;
    public int strength;
    public int attSpeed;

    float bAttackTime;
    float bTimeLeft;

    public Hero hero;

    bool isFighting = false;

    // Use this for initialization
    void Start()
    {
        //initialize stats (semi random), only level 1 for now
        level = 1;
        hp = (int)(9 + level + Mathf.Round(Random.Range(0f, level)));
        strength = (int)(1 + level + Mathf.Round(Random.Range(0f, level)));
        attSpeed = (int)(1 + level + Mathf.Round(Random.Range(0f, level)));
        Debug.Log("Boss Stats: Level: " + level + ", HP: " + hp + ", STR: " + strength + ", SPD: " + attSpeed);

        bAttackTime = 1 - this.attSpeed * 0.1f;

    }

    // Update is called once per frame
    void Update()
    {
        if (isFighting)
        {
            bTimeLeft -= Time.deltaTime;

            if(bTimeLeft <= 0)
            {

                hero.hp -= strength;
                Debug.Log("Hero HP: " + hero.hp);
                if (hero.hp <= 0)
                {
                  isFighting=false;
                  Destroy(hero.gameObject);
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
        isFighting = true;
    }
}
