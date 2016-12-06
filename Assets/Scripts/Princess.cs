using UnityEngine;
using System.Collections;

public class Princess : MonoBehaviour {

    Hero hero;

    public Vector3 initialPosition;

	// Use this for initialization
	void Start () {
        //hero = GameManager.instance.hero;
	}
	
	// Update is called once per frame
	void Update () {
        if (!GameManager.instance.wayDown && !GameManager.instance.buyPhase)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, GameManager.instance.hero.transform.position, GameManager.instance.hero.movSpeed*0.9f);
        }
	}
}
