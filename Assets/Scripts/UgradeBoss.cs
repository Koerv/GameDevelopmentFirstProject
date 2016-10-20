using UnityEngine;
using System.Collections;

public class UgradeBoss : MonoBehaviour {

    public void executeUpgrade()
    {
        GameManager.instance.selectedBoss.upgradeBoss();
    }

}
