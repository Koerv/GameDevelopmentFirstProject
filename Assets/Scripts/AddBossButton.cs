using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class AddBossButton : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    
    public GameObject boss;
    public int bossType;
    public bool dragging = false;

    public void OnBeginDrag(PointerEventData eventData)
    {


        if (GameManager.instance.coins >= GameManager.instance.getBossCosts())
        {
            Debug.Log("Begin Dragging");
            dragging = true;
            GameManager.instance.bossGrabbed = true;

            if (bossType == GameManager.ROCK)
            {
                boss = Instantiate(Resources.Load("Boss_Rock"), new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity) as GameObject;
            }
            else if (bossType == GameManager.PAPER)
            {
                boss = Instantiate(Resources.Load("Boss_Paper"), new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity) as GameObject;
            }
            else boss = Instantiate(Resources.Load("Boss_Scissors"), new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity) as GameObject;

            boss.GetComponent<Boss>().attribute = bossType;
            GameManager.instance.newBoss = boss;
            Physics2D.IgnoreCollision(boss.GetComponent<Collider2D>(), GameManager.instance.hero.GetComponent<Collider2D>());
            //GameManager.instance.uiManager.hideBuyMenu();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        
        if (GameManager.instance.bossGrabbed == true)
        {

            Vector3 target;
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            boss.transform.position = new Vector3(target.x, target.y, 0);

        }
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {

        if (GameManager.instance.bossGrabbed)
        {
            if (GameManager.instance.getMouseOnTile() == false)
            {
                Destroy(boss);
            }
            else
            {
                Floor selectedTile = GameManager.instance.selectedTile;
                Debug.Log("Boss should be added");
                GameManager.instance.newBoss.transform.position = selectedTile.transform.position;

                GameManager.instance.bossGrabbed = false;
                GameManager.instance.coins -= GameManager.instance.getBossCosts();
                GameManager.instance.bossCount += 1;
                selectedTile.bossOnTile = GameManager.instance.newBoss;
                Physics2D.IgnoreCollision(selectedTile.bossOnTile.GetComponent<Collider2D>(), GameManager.instance.hero.GetComponent<Collider2D>(), false);
                GameManager.instance.uiManager.showBuyMenu();
                GameManager.instance.newBoss = null;
            }
        }
        boss = null;
        GameManager.instance.bossGrabbed = false;
        dragging = false;
    }
   

}
