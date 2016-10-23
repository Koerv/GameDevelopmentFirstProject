using UnityEngine;
using System.Collections;

public class UgradeBoss : MonoBehaviour {
    private Boss boss;
    public void executeUpgrade()
    {
        boss = GameManager.instance.selectedBoss;
        if (GameManager.instance.coins >= GameManager.instance.getUpgradeCosts())
        {
            GameManager.instance.coins -= GameManager.instance.getUpgradeCosts();
            GameManager.instance.selectedBoss.upgradeBoss();
        }
    }

}
