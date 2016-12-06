using UnityEngine;
using System.Collections;

public class StrongAndStupid : Hero
{

    public StrongAndStupid()
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


}
