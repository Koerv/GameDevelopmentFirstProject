using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {
    public Text[] texts;
    int index;
    
    // Use this for initialization
    void Start () {
        foreach ( Text text in texts){
            text.enabled = false;
        }
        texts[0].enabled = true;
        index = 0;
        
	}
	
    public void nextPage()
    {
        texts[index].enabled = false;
        index = (index + 1) % texts.Length;
        texts[index].enabled = true;
    }
}
