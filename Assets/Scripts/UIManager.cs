﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public GameObject button1;
    public GameObject button2;
    public Text loseScreen;

    void Awake()
    {
        loseScreen.enabled = false;
    }
    public void hideBuyMenu()
    {
        button1.SetActive(false);
        button2.SetActive(false);

    }

    public void showLoseScreen()
    {
        loseScreen.enabled = true;
    }

    public void showBuyMenu()
    {
        button1.SetActive(true);
        button2.SetActive(true);

    }
}
