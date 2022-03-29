using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldGenerator : MonoBehaviour
{
    public int width;
    public int height;

    //fake "noise"
    private int minHeight;
    private int maxHeight;
    public int lastGrassX;
    public int lastGrassY;
    public int repeatSample;
    public int repeat = 0;

    public GameObject grass;
    public GameObject dirt;    
    public GameObject stone;
    public GameObject water;  
    public GameObject cloudFalling;  
    public GameObject cloudSideToSide;  
    public GameObject cloudUpAndDown;  
    public GameObject enemyTest;  


    void Start()
    {
        minHeight = height - 2;
        maxHeight = height + 5;
        
        lastGrassX = width;
        lastGrassY = height;

        GenerateMap();
    }
    void GenerateMap()
    {
        for(int x = 0; x < width; x++)
        {
            if(repeat == 0)
            {
                height = Random.Range(minHeight, maxHeight);
                spawnFlat(x, true);

                repeatSample = Random.Range(2,4);
                repeat = repeatSample;

            } else {
                spawnFlat(x, false);
                repeat--;
            }

        }
    }

    void spawnFlat(int x, bool first)
    {
        //check if water
        if (height == minHeight)
        {
            for (int y = 0; y < height; y++)
            {
                spawnTile(water, x, y);
            }
                spawnTile(water, x, minHeight);
        } 

        //check if needs clouds
        if (first && !(lastGrassY >= minHeight + 2))
            {
                spawnTile(cloudSideToSide, x, minHeight + 2);
            }
        //check if earth 
        else 
        {
            int diffH = height - lastGrassY;
            int diffX = x - lastGrassX;

            if (diffH == 2 && diffX >= 1)
            {
                spawnTile(stone, x-1, lastGrassY+1);
            }

            for (int y = 0; y < height; y++)
            {
                spawnTile(dirt, x, y);
            }

            spawnTile(grass, x, height);
            
            if(repeatSample == 4) 
            {
                //spawn enemy in the grass
                enemyTest = Instantiate(enemyTest, new Vector2(x, height+3), Quaternion.identity);
                enemyTest.transform.parent = this.transform;
            }

            lastGrassY = height;
            lastGrassX = x;   
        }

    }

    void spawnTile(GameObject tiling, int width, int height)
    {
        tiling = Instantiate(tiling, new Vector2(width, height), Quaternion.identity);
        tiling.transform.parent = this.transform;
    }
}
