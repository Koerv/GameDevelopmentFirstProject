using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    //yes Patrick, we could have used SerializeField all along to make variables public to the inspector...
    [SerializeField]
    private float fillAmount;

    //Image used for the filling of the health bar
    [SerializeField]
    private Image filling;

    //Offset related to the heroes/boss' position
    private Vector3 posOffset = new Vector3(0f, 0.5f, 0f);
	
	// Update is called once per frame
	void Update () {
        UpdateFilling();
        //TODO why won't it move above hero?
        //transform.position = GameManager.instance.hero.transform.position + posOffset;
	}

    private void UpdateFilling()
    {
        filling.fillAmount = MapHealthToFillAmount(GameManager.instance.hero.maxHP, GameManager.instance.hero.hp);
    }

    private float MapHealthToFillAmount(int maxHP, int currHP)
    {
        return (currHP)/(float)(maxHP);
    }
}
