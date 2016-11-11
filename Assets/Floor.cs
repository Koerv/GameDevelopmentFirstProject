using UnityEngine;
using System.Collections;

public class Floor : MonoBehaviour {

    void OnMouseDown()
    {
        Debug.Log(this.transform.localPosition);
        if (GameManager.instance.bossGrabbed) { 
            GameManager.instance.newBoss.transform.position = this.transform.position;
            GameManager.instance.bossGrabbed = false;
            GameManager.instance.coins -= GameManager.instance.getBossCosts();
            GameManager.instance.bossCount += 1;
            
        }
    }
}
