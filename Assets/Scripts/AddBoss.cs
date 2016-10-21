using UnityEngine;
using System.Collections;

public class AddBoss : MonoBehaviour {
    public GameObject boss;
    bool buildMode = false;
	// Use this for initialization
	public void CreateBoss (){
        //wait for mouse click
        buildMode = true;
        if (GameManager.instance.coins >= GameManager.instance.getBossCosts())
        {
            boss = Instantiate(Resources.Load("Boss_1"), new Vector3(transform.position.x, transform.position.y,0), Quaternion.identity) as GameObject;
        }
    }

    void Update()
    {
        if (buildMode == true)
        {

            Vector3 target;
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);      
            boss.transform.position = new Vector3(target.x,target.y,0);
            if (Input.GetMouseButtonDown(0))
                {
                    buildMode = false;
                    GameManager.instance.coins -= GameManager.instance.getBossCosts();
                    GameManager.instance.bossCount += 1;
                }
            }
        
    }


}
