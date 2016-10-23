using UnityEngine;

public class Loader : MonoBehaviour {

    public GameManager gameManager;

    void awake()
    {
        Instantiate(gameManager);

    }
}
