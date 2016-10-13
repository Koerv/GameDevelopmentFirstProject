using UnityEngine;
using System.Collections;

public class AddBoss : MonoBehaviour {
    public GameObject boss;
    bool buildMode = false;
	// Use this for initialization
	public void CreateBoss (){
        //wait for mouse click
        buildMode = true;
	}

    void Update()
    {
        if (buildMode == true){
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 target;
                target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                
                //Prefab has to be in Resources folder
                boss = Instantiate(Resources.Load("Boss_1"), new Vector3(target.x, target.y, 0), Quaternion.identity) as GameObject;
                buildMode = false;
            }
        }
    }


}
