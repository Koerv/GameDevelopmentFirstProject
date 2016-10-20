using UnityEngine;
using System.Collections;

public class UgradeBoss : MonoBehaviour {
    private Boss boss;
    public void executeUpgrade()
    {
        boss = GameManager.instance.selectedBoss;
        if (GameManager.instance.coins >= boss.level * 20)
        {
            GameManager.instance.coins -= boss.level * 20;
            GameManager.instance.selectedBoss.upgradeBoss();
        }
    }

}
