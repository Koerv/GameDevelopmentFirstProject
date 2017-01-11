using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Boss : MonoBehaviour
{

    public int level;
    public int hp;
    public int strength;
    public float attSpeed;
    public Sprite standardSprite;
    public Sprite selectedSprite;
    public float attributeModifier;

    //TODO add buttons for Bosses with different attributes
    public int attribute;

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
        attSpeed = (1 + level + Mathf.Round(Random.Range(0f, level)))*0.8f;
        Debug.Log("Boss Stats: Level: " + level + ", HP: " + hp + ", STR: " + strength + ", SPD: " + attSpeed + ", attribute: " + GameManager.instance.attrToString(attribute));

        bAttackTime = 1 - this.attSpeed * 0.1f;

    }

    // Update is called once per frame
    void Update()
    {
        if (isFighting)
        {
            bTimeLeft -= Time.deltaTime;
            //also make the boss move towards the hero, but not as strongly since bosses are HUGE and MIGHTY!
            //animation for smoother movement
            if (bTimeLeft <= (bAttackTime / 2))
            {
                Vector3.MoveTowards(transform.position, preFightPosition, 1f);
            }
            //for moving back
            else
            {
                transform.position = (transform.position + hero.getMoveDirection() * 0.4f * bTimeLeft);
            }

            if (bTimeLeft <= 0)
            {
                GetComponent<AudioSource>().Play();
                transform.position = preFightPosition;
                hero.hp -= (int)(Mathf.Round(strength*attributeModifier));
                Debug.Log("Hero HP: " + hero.hp);
                if (hero.hp <= 0)
                {

                    transform.position = preFightPosition;
                    isFighting=false;
                    hero.Defeated();
                    //Destroy(hero.gameObject);
                    
                    GameManager.instance.stageClear();
                }
                //reset Attack Time
                bTimeLeft = bAttackTime;
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (!GameManager.instance.buyPhase)
        {
            Debug.Log("Kämpf!");
            //needed to assign Hero to runtime-generated bosses
            hero = collision.collider.GetComponentInParent<Hero>();

            isFighting = true;
            attributeModifier = GameManager.instance.checkWeaknesses(this, hero);
            Debug.Log("attribute Modifier: " + attributeModifier);
            preFightPosition = transform.position;
        }
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
        hp = (int)(hp + level*3 + Mathf.Round(Random.Range(0f, level)));
        strength = (int)(strength + Mathf.Round(Random.Range(0f, level * 0.8f)));
        attSpeed= attSpeed + Mathf.Round(Random.Range(0f, level))*0.3f;
        bAttackTime = 1 - this.attSpeed * 0.1f;
        Debug.Log("Boss Stats: Level: " + level + ", HP: " + hp + ", STR: " + strength + ", SPD: " + attSpeed + ", attribute: " + GameManager.instance.attrToString(attribute));
    }
    
}
