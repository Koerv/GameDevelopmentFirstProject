using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {

    public int[,] layout = new int[15,10];
    public GameObject[] tiles ;

    //floor tiles contain more information about the objects on and around it
    public Floor[,] floorTiles = new Floor[15,10];
    public Princess princess;
    public Hero hero;

    public int gridSizeX;
    public int gridSizeY;

    public void startInstantiation()
    {
        setupLayout();
        hero = GameManager.instance.hero;
        placeTiles();
    }

    public void setupLayout()
    {
        gridSizeX =15;
        gridSizeY =10;

        layout[0, 0] = 0;
        layout[0, 1] = 0;
        layout[0, 2] = 0;
        layout[0, 3] = 0;
        layout[0, 4] = 0;
        layout[0, 5] = 0;
        layout[0, 6] = 0;
        layout[0, 7] = 0;
        layout[0, 8] = 0;
        layout[0, 9] = 0;
        layout[1, 0] = 0;
        layout[1, 1] = 0;
        layout[1, 2] = 0;
        layout[1, 3] = 0;
        layout[1, 4] = 2;
        layout[1, 5] = 0;
        layout[1, 6] = 0;
        layout[1, 7] = 0;
        layout[1, 8] = 0;
        layout[1, 9] = 0;
        layout[2, 0] = 0;
        layout[2, 1] = 0;
        layout[2, 2] = 1;
        layout[2, 3] = 1;
        layout[2, 4] = 3;
        layout[2, 5] = 1;
        layout[2, 6] = 1;
        layout[2, 7] = 1;
        layout[2, 8] = 1;
        layout[2, 9] = 0;
        layout[3, 0] = 0;
        layout[3, 1] = 0;
        layout[3, 2] = 1;
        layout[3, 3] = 0;
        layout[3, 4] = 0;
        layout[3, 5] = 0;
        layout[3, 6] = 0;
        layout[3, 7] = 0;
        layout[3, 8] = 1;
        layout[3, 9] = 0;
        layout[4, 0] = 0;
        layout[4, 1] = 1;
        layout[4, 2] = 1;
        layout[4, 3] = 1;
        layout[4, 4] = 1;
        layout[4, 5] = 0;
        layout[4, 6] = 0;
        layout[4, 7] = 1;
        layout[4, 8] = 1;
        layout[4, 9] = 0;
        layout[5, 0] = 0;
        layout[5, 1] = 1;
        layout[5, 2] = 0;
        layout[5, 3] = 0;
        layout[5, 4] = 1;
        layout[5, 5] = 0;
        layout[5, 6] = 0;
        layout[5, 7] = 1;
        layout[5, 8] = 0;
        layout[5, 9] = 0;
        layout[6, 0] = 0;
        layout[6, 1] = 1;
        layout[6, 2] = 1;
        layout[6, 3] = 1;
        layout[6, 4] = 1;
        layout[6, 5] = 0;
        layout[6, 6] = 1;
        layout[6, 7] = 1;
        layout[6, 8] = 1;
        layout[6, 9] = 0;
        layout[7, 0] = 0;
        layout[7, 1] = 0;
        layout[7, 2] = 1;
        layout[7, 3] = 0;
        layout[7, 4] = 0;
        layout[7, 5] = 0;
        layout[7, 6] = 1;
        layout[7, 7] = 0;
        layout[7, 8] = 1;
        layout[7, 9] = 0;
        layout[8, 0] = 0;
        layout[8, 1] = 0;
        layout[8, 2] = 1;
        layout[8, 3] = 0;
        layout[8, 4] = 0;
        layout[8, 5] = 0;
        layout[8, 6] = 1;
        layout[8, 7] = 1;
        layout[8, 8] = 1;
        layout[8, 9] = 0;
        layout[9, 0] = 0;
        layout[9, 1] = 0;
        layout[9, 2] = 1;
        layout[9, 3] = 1;
        layout[9, 4] = 1;
        layout[9, 5] = 0;
        layout[9, 6] = 0;
        layout[9, 7] = 1;
        layout[9, 8] = 0;
        layout[9, 9] = 0;
        layout[10, 0] = 0;
        layout[10, 1] = 0;
        layout[10, 2] = 0;
        layout[10, 3] = 0;
        layout[10, 4] = 1;
        layout[10, 5] = 1;
        layout[10, 6] = 1;
        layout[10, 7] = 1;
        layout[10, 8] = 1;
        layout[10, 9] = 0;
        layout[11, 0] = 0;
        layout[11, 1] = 0;
        layout[11, 2] = 0;
        layout[11, 3] = 0;
        layout[11, 4] = 0;
        layout[11, 5] = 0;
        layout[11, 6] = 1;
        layout[11, 7] = 0;
        layout[11, 8] = 1;
        layout[11, 9] = 0;
        layout[12, 0] = 0;
        layout[12, 1] = 0;
        layout[12, 2] = 0;
        layout[12, 3] = 0;
        layout[12, 4] = 0;
        layout[12, 5] = 0;
        layout[12, 6] = 3;
        layout[12, 7] = 1;
        layout[12, 8] = 1;
        layout[12, 9] = 0;
        layout[13, 0] = 0;
        layout[13, 1] = 0;
        layout[13, 2] = 0;
        layout[13, 3] = 0;
        layout[13, 4] = 0;
        layout[13, 5] = 0;
        layout[13, 6] = 4;
        layout[13, 7] = 0;
        layout[13, 8] = 0;
        layout[13, 9] = 0;
        layout[14, 0] = 0;
        layout[14, 1] = 0;
        layout[14, 2] = 0;
        layout[14, 3] = 0;
        layout[14, 4] = 0;
        layout[14, 5] = 0;
        layout[14, 6] = 0;
        layout[14, 7] = 0;
        layout[14, 8] = 0;
        layout[14, 9] = 0;






    }

    void placeTiles()
    {
        for (int i = 0; i< gridSizeX-1; i++)
        {
 
            for(int j = 0; j <= gridSizeY-1; j++)
            {
                
                if (layout[i, j] != 0)
                {

                    GameObject toInstantiate = tiles[layout[i,j]];
                    GameObject instance =
                        Instantiate(toInstantiate) as GameObject;

                    //initialize tiles and give them a type(entrance, safe space, regular tile or cage)
                    floorTiles[i,j] =  instance.GetComponent<Floor>();
                    floorTiles[i,j].type = layout[i, j];
                    floorTiles[i,j].layoutPosX = i;
                    floorTiles[i,j].layoutPosY = j;

                    instance.transform.SetParent(this.transform, false);
                    //Prevent overlapping of tiles
                    instance.GetComponent<SpriteRenderer>().sortingOrder = i;
                    //without the numbers the grid would be upside down
                    instance.transform.localPosition = new Vector2(j-5, -i + 8);
                    if(layout[i,j] == 4)
                    {
                        princess.transform.position = instance.transform.position;
                        princess.initialPosition = princess.transform.position;
                        princess.GetComponent<SpriteRenderer>().sortingOrder = i-1;
                    }
                    if (layout[i, j] == 2)
                    {
                        //set initial position and sprite order of the hero
                        GameManager.instance.heroInitialPosition = instance.transform.position;
                        //hero.transform.position = hero.initialPosition;
                        //hero.GetComponent<SpriteRenderer>().sortingOrder = gridSizeX;
                        //hero.layoutStartPosition = new Vector2(i, j);
                        //hero.layoutPosition = hero.layoutStartPosition;
                        GameManager.instance.heroInitialLayoutPosition = new Vector2(i, j);
                    }


                }
            }
        }
    }

    //for every tile in the grid, see what is left and right
    public void calcSumOfStats()
    {
        foreach (Floor floorTile in floorTiles)
        {

            if(floorTile != null)
            {
                //reset sumOfStats
                floorTile.sumOfStatsEast = 0;
                floorTile.sumOfStatsWest = 0;

                //reset sumOfPotions
                floorTile.sumOfPotionsEast = 0;
                floorTile.sumOfPotionsWest = 0;

                int eastTileXCoord = floorTile.layoutPosX;
                int eastTileYCoord = floorTile.layoutPosY + 1;
                while (layout[eastTileXCoord, eastTileYCoord] != 0 && eastTileYCoord < gridSizeY)
                {
                    //increase total stats if a boss is on this floor
                    if(floorTiles[eastTileXCoord,eastTileYCoord].bossOnTile !=null) {
                        Boss tileBoss = floorTiles[eastTileXCoord, eastTileYCoord].bossOnTile.GetComponent<Boss>();
                        floorTile.sumOfStatsEast += tileBoss.hp+tileBoss.strength*3+tileBoss.attSpeed*3;

                    }

                    //increase counter if potion is on this floor
                    if(floorTiles[eastTileXCoord,eastTileYCoord].potionOnTile != null)
                    {
                        floorTile.sumOfPotionsEast++;
                    }
                    eastTileYCoord++;
                }
                int westTileXCoord = floorTile.layoutPosX;
                int westTileYCoord = floorTile.layoutPosY - 1;

                while (layout[westTileXCoord, westTileYCoord] != 0 && westTileYCoord >= 0)
                {
                    if (floorTiles[westTileXCoord, westTileYCoord].bossOnTile != null)
                    {
                        Boss tileBoss = floorTiles[westTileXCoord, westTileYCoord].bossOnTile.GetComponent<Boss>();
                        floorTile.sumOfStatsWest += tileBoss.hp + tileBoss.strength * 3 + tileBoss.attSpeed * 3;
                    }
                    if (floorTiles[westTileXCoord, westTileYCoord].potionOnTile != null)
                    {
                        floorTile.sumOfPotionsWest++;
                    }
                    westTileYCoord--;
                }

            }
        }
    }

    public Floor getTile(int x, int y)
    {
        if (floorTiles[x, y] == null)
            return new Floor();
        else
            return floorTiles[x, y];
    }
   
}
