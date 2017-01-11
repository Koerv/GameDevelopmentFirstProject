using UnityEngine;
using System.Collections;

public class AddPotion : MonoBehaviour {

    public GameObject potion;

    // Use this for initialization
    public void addPotion(int type)
    {
        potion = Instantiate(Resources.Load("Potion"), new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity) as GameObject;
        potion.GetComponent<Potion>().type = type;
        potion.GetComponent<SpriteRenderer>().sprite = potion.GetComponent<Potion>().sprites[potion.GetComponent<Potion>().type];
        Debug.Log("Potion type: " + potion.GetComponent<Potion>().typeToString());
        GameManager.instance.newPotion = potion;
        Physics2D.IgnoreCollision(potion.GetComponent<Collider2D>(), GameManager.instance.hero.GetComponent<Collider2D>());
        GameManager.instance.uiManager.hideBuyMenu();
    }

    // Update is called once per frame
    void Update () {
        if (GameManager.instance.potionGrabbed == true)
        {

            Vector3 target;
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            potion.transform.position = new Vector3(target.x, target.y, 0);

        }
    }
}
