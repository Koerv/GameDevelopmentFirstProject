using UnityEngine;
using System.Collections;

public class AddBoss : MonoBehaviour {

    public GameObject boss;
	// Use this for initialization
	public void CreateBoss (int attribute){
        //wait for mouse click
        
        if (GameManager.instance.coins >= GameManager.instance.getBossCosts())
        {
            GameManager.instance.bossGrabbed = true;
            boss = Instantiate(Resources.Load("Boss_1"), new Vector3(transform.position.x, transform.position.y,0), Quaternion.identity) as GameObject;
            boss.GetComponent<Boss>().attribute = attribute;
            if (attribute == 0)
            {
                boss.GetComponent<Boss>().GetComponent<SpriteRenderer>().color = new Color(0.9f, 0.4f, 0);
            }
            else if (attribute == 1)
            {
                boss.GetComponent<Boss>().GetComponent<SpriteRenderer>().color = new Color(0, 0.4f, 0.6f);
            }
            GameManager.instance.newBoss = boss;
            Physics2D.IgnoreCollision(boss.GetComponent<Collider2D>(), GameManager.instance.hero.GetComponent<Collider2D>());
            GameManager.instance.uiManager.hideBuyMenu();
        }
    }

    void Update()
    {
        if (GameManager.instance.bossGrabbed == true)
        {

            Vector3 target;
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);      
            boss.transform.position = new Vector3(target.x,target.y,0);

        }
                   
    }
     

}
