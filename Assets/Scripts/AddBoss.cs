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

            if (attribute == GameManager.ROCK)
            {
                boss = Instantiate(Resources.Load("Boss_Rock"), new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity) as GameObject;
            }
            else if(attribute == GameManager.PAPER)
            {
                boss = Instantiate(Resources.Load("Boss_Paper"), new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity) as GameObject;
            }
            else boss = Instantiate(Resources.Load("Boss_Scissors"), new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity) as GameObject;

            boss.GetComponent<Boss>().attribute = attribute;
           
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
