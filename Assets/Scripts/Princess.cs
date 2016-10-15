using UnityEngine;
using System.Collections;

public class Princess : MonoBehaviour {

    Hero hero;

	// Use this for initialization
	void Start () {
        hero = FindObjectOfType<Hero>();
	}
	
	// Update is called once per frame
	void Update () {
        if (GameManager.instance.wayDown)
        {
            this.transform.position=hero.position;
        }
	}
}
