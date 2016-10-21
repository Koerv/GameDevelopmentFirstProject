using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public Text loseScreen;
    public Text winScreen;
    public Text stageNr;
    public Text coins;

    void Awake()
    {
        loseScreen.enabled = false;
        winScreen.enabled = false;

        stageNr.text = ("Stage 1");
        coins.text = ("Coins: " + coins);
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

    public IEnumerator showWinScreen()
    {
        winScreen.enabled = true;
        yield return new WaitForSeconds(5.0f);
        winScreen.enabled = false;
    }

    void Update()
    {
        button2.GetComponentInChildren<Text>().text = "ADD Boss (" + GameManager.instance.getBossCosts() + ")";
        if (GameManager.instance.selectedBoss != null)
        {
            button3.GetComponentInChildren<Text>().text = "Upgrade Boss (" + GameManager.instance.getUpgradeCosts() + ")";
        }
        updateCoins();
    }
    public void updateCoins()
    {
        coins.text = "Coins: " + GameManager.instance.coins;
    }

    public void showBuyMenu()
    {
        button1.SetActive(true);
        button2.SetActive(true);

    }

    public void showUpgradeMenu()
    {
        button3.SetActive(true);
    }
}
