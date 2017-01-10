using UnityEngine;
using System.Collections;

public class Floor : MonoBehaviour {

    public int type;

    //#of stats of bosses along each direction
    public float sumOfStatsEast = 0f;
    public float sumOfStatsWest = 0f;

    public int sumOfPotionsEast = 0;
    public int sumOfPotionsWest = 0;

    //position of the tile on layout
    public int layoutPosX;
    public int layoutPosY;

    //boss that is occupying this tile
    public GameObject bossOnTile;

    //potion that is on this tile
    public GameObject potionOnTile;

    //all other tiles
    public Grid grid;


    void OnMouseDown()
    {
        grid = GameManager.instance.grid;
        Debug.Log(this.transform.localPosition);
        if (GameManager.instance.bossGrabbed && type == 1 && bossOnTile == null && potionOnTile == null &&
            grid.getTile(layoutPosX + 1, layoutPosY).bossOnTile == null &&
            grid.getTile(layoutPosX - 1, layoutPosY).bossOnTile == null &&
            grid.getTile(layoutPosX, layoutPosY + 1).bossOnTile == null &&
            grid.getTile(layoutPosX, layoutPosY - 1).bossOnTile == null
            )
            {
                GameManager.instance.newBoss.transform.position = this.transform.position;
                GameManager.instance.bossGrabbed = false;
                GameManager.instance.coins -= GameManager.instance.getBossCosts();
                GameManager.instance.bossCount += 1;
                bossOnTile = GameManager.instance.newBoss;
                Physics2D.IgnoreCollision(bossOnTile.GetComponent<Collider2D>(), GameManager.instance.hero.GetComponent<Collider2D>(), false);
                GameManager.instance.uiManager.showBuyMenu();
        }
        

        else if (GameManager.instance.potionGrabbed && bossOnTile == null && potionOnTile == null)
        {
            GameManager.instance.newPotion.transform.position = this.transform.position;
            GameManager.instance.potionGrabbed = false;
            potionOnTile = GameManager.instance.newPotion;
            Physics2D.IgnoreCollision(potionOnTile.GetComponent<Collider2D>(), GameManager.instance.hero.GetComponent<Collider2D>(), false);
            GameManager.instance.uiManager.showBuyMenu();
        }
    }
}
