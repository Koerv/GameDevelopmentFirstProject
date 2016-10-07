using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour {

    public GameManager gameManager;

    void awake()
    {
        Instantiate(gameManager);

    }
}
