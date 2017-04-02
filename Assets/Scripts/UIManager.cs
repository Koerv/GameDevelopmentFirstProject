using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour {

    public GameObject buyPhaseButton;
    public GameObject rockBossButton;
    public GameObject paperBossButton;
    public GameObject scissorsBossButton;
    public GameObject upgradeBossButton;
    public GameObject pointer;
    public Text loseScreen;
    public Text winScreen;
    public Text stageNr;
    public Text coins;
    public Text bossInfo;
    public Text heroInfo;
    public Text potionInfo;
    public Text finalWinScreen;
    public Canvas tutorialCanvas;
    public GameObject speedModeButton;

    void Awake()
    {
        loseScreen.enabled = false;
        winScreen.enabled = false;
        finalWinScreen.enabled = false;
        tutorialCanvas.GetComponent<Canvas>().enabled = false;
        pointer.GetComponent<SpriteRenderer>().enabled = false;
        pointer.transform.position = new Vector2(0f, 0f);
   
        stageNr.text = ("Stage 1");
        coins.text = ("Coins: " + coins);
    }
    public void hideBuyMenu()
    {
        Debug.Log("Buy menu hidden");
        buyPhaseButton.SetActive(false);
        rockBossButton.SetActive(false);
        paperBossButton.SetActive(false);
        scissorsBossButton.SetActive(false);
        upgradeBossButton.SetActive(false);

    }

    public void hideSpeedButton()
    {
        speedModeButton.SetActive(false);
    }

    public void showSpeedButton()
    {
        speedModeButton.SetActive(true);
    }

    public void showLoseScreen()
    {
        loseScreen.enabled = true;
    }

    public IEnumerator showWinScreen()
    {
        winScreen.enabled = true;
        yield return new WaitForSeconds(10.0f);
        winScreen.enabled = false;
    }

    public void showFinalWinScreen()
    {
        //hide everything
        foreach (Text text in GetComponentsInChildren<Text>())
        {
            text.enabled = false;
        }
        buyPhaseButton.SetActive(false);
        rockBossButton.SetActive(false);
        paperBossButton.SetActive(false);
        scissorsBossButton.SetActive(false);

        finalWinScreen.enabled = true;
    }

    void Update()
    {
        rockBossButton.GetComponentInChildren<Text>().text = "Add Rock Boss (" + GameManager.instance.getBossCosts() + ")";
        paperBossButton.GetComponentInChildren<Text>().text = "Add Paper Boss (" + GameManager.instance.getBossCosts() + ")";
        scissorsBossButton.GetComponentInChildren<Text>().text = "Add Scissors Boss (" + GameManager.instance.getBossCosts() + ")";

        if (GameManager.instance.selectedBoss != null)
        {
            upgradeBossButton.GetComponentInChildren<Text>().text = "Upgrade Boss (" + GameManager.instance.getUpgradeCosts() + ")";
        }
        updateCoins();
        updateBossInfo();
        updatePotionInfo();
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
            bossInfo.text = "Current Boss Stats: Level: " + boss.level + " HP: " + boss.hp + " Strength: " + boss.strength + " AttackSpeed: " + boss.attSpeed + " Attribute: " + GameManager.instance.attrToString(boss.attribute);
        }
        else
            bossInfo.enabled = false;
    }

    public void updateHeroInfo()
    {
        if (GameManager.instance.hero != null)
        {
            Hero hero = GameManager.instance.hero;
            heroInfo.text = "Hero Type: " + hero.getName() + ", Stats: HP: " + hero.hp + " Strength: " + hero.strength + " AttackSpeed: " + hero.attSpeed + " Attribute: " + GameManager.instance.attrToString(hero.attribute);
        }
    }

    public void updatePotionInfo()
    {
        if (GameManager.instance.newPotion != null)
        {
            Potion potion = GameManager.instance.newPotion.GetComponent<Potion>();
            potionInfo.text = "Current potion: " + potion.typeToString();
        }
        else
        {
            potionInfo.text = "Current potion: None";
        }
    }

    public void showBuyMenu()
    {
        buyPhaseButton.SetActive(true);
        rockBossButton.SetActive(true);
        paperBossButton.SetActive(true);
        scissorsBossButton.SetActive(true);
    }

    public void showUpgradeMenu()
    {
        if (GameManager.instance.selectedBoss != null && GameManager.instance.buyPhase && !GameManager.instance.bossGrabbed && !GameManager.instance.potionGrabbed)
        {   
            upgradeBossButton.SetActive(true);
        }
        else
        {
            upgradeBossButton.SetActive(false);
        }
    }

    public void showHideTutorial()
    {
        if (tutorialCanvas.GetComponent<Canvas>().enabled == false)
        {
            tutorialCanvas.GetComponent<Canvas>().enabled = true;
        }
        else tutorialCanvas.GetComponent<Canvas>().enabled = false;
    }

    public void movePointer(Vector3 bossPos)
    {
        pointer.GetComponent<SpriteRenderer>().enabled = true;
        pointer.transform.position = new Vector2(bossPos.x, bossPos.y+0.5f);
    }
}
