﻿using UnityEngine;
using System.Collections;

public class Floor : MonoBehaviour {

    public int type;

    //#of stats of bosses along each direction
    public int sumOfStatsEast;
    public int sumOfStatsWest;

    //#of tiles along each direction
    //public int tilesEast;
    //public int tilesWest;

    //position of the tile on layout
    public int layoutPosX;
    public int layoutPosY;

    //boss that is occupying this tile
    public GameObject bossOnTile;

    //TODO potion that is on this tile

    void OnMouseDown()
    {
        Debug.Log(this.transform.localPosition);
        if (GameManager.instance.bossGrabbed && type==1) { 
            GameManager.instance.newBoss.transform.position = this.transform.position;
            GameManager.instance.bossGrabbed = false;
            GameManager.instance.coins -= GameManager.instance.getBossCosts();
            GameManager.instance.bossCount += 1;
            bossOnTile=GameManager.instance.newBoss;
        }
    }
}
