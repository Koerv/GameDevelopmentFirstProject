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
    public Text bossInfo;
    public Text heroInfo;
    public Text finalWinScreen;

    void Awake()
    {
        loseScreen.enabled = false;
        winScreen.enabled = false;
        finalWinScreen.enabled = false;

        stageNr.text = ("Stage 1");
        coins.text = ("Coins: " + coins);
    }
    public void hideBuyMenu()
    {
        button1.SetActive(false);
        button2.SetActive(false);
        button3.SetActive(false);

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

    public void showFinalWinScreen()
    {
        //hide everything
        foreach (Text text in GetComponentsInChildren<Text>())
        {
            text.enabled = false;
        }
        button1.SetActive(false);
        button2.SetActive(false);

        finalWinScreen.enabled = true;
    }

    void Update()
    {
        button2.GetComponentInChildren<Text>().text = "ADD Boss (" + GameManager.instance.getBossCosts() + ")";
        if (GameManager.instance.selectedBoss != null)
        {
            button3.GetComponentInChildren<Text>().text = "Upgrade Boss (" + GameManager.instance.getUpgradeCosts() + ")";
        }
        updateCoins();
        updateBossInfo();
        showUpgradeMenu();
        updateHeroInfo();
    }
    public void updateCoins()
    {
        coins.text = "Coins: " + GameManager.instance.coins;
    }

    public void updateBossInfo()
    {
        if (GameManager.instance.selectedBoss != null)
        {
            bossInfo.enabled = true;
            Boss boss = GameManager.instance.selectedBoss;
            bossInfo.text = "Current Boss Stats: Level: " + boss.level + " HP: " + boss.hp + " Strength: " + boss.strength + " AttackSpeed: " + boss.attSpeed;
        }
        else
            bossInfo.enabled = false;
    }

    public void updateHeroInfo()
    {
        Hero hero = GameManager.instance.hero;
        heroInfo.text = "Hero Stats: HP: " + hero.hp + " Strength: " + hero.strength + " AttackSpeed: " + hero.attSpeed;
    }

    public void showBuyMenu()
    {
        button1.SetActive(true);
        button2.SetActive(true);

    }

    public void showUpgradeMenu()
    {
        if (GameManager.instance.selectedBoss != null)
        {
            button3.SetActive(true);

        }
        else
        {
            button3.SetActive(false);
        }
    }
}
