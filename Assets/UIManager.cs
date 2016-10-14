using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {

    public GameObject button1;
    public GameObject button2;
    public void hideBuyMenu()
    {
        button1.SetActive(false);
        button2.SetActive(false);

    }

    public void showBuyMenu()
    {
        button1.SetActive(true);
        button2.SetActive(true);

    }
}
