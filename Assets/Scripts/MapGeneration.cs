using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour
{
    public int width;
    public int height;

    //fake "noise"
    private int minHeight;
    private int maxHeight;
    public int lastGrassX;
    public int lastGrassY;
    public int lastWaterX;
    public int lastWaterY;
    public int repeatSample;
    public int repeat = 0;
  
    public GameObject cloudFalling;  
    public GameObject cloudSideToSide;  
    public GameObject cloudUpAndDown;  
    public GameObject grass;
    public GameObject stone;
    public GameObject dirt;  
    public GameObject water;  
    public GameObject enemyTest;  


    void Start()
    {
        minHeight = height - 2;
        maxHeight = height + 5;
        
        lastGrassX = width;
        lastGrassY = height;

        lastWaterX = width;
        lastWaterY = height;

        GenerateMap();
    }
    void GenerateMap()
    {
        for(int x = 0; x < width; x++)
        {
            if(repeat == 0)
            {
                repeatSample = Random.Range(2,5);
                height = Random.Range(minHeight, maxHeight);
                spawnColumn(x);

                if(repeatSample >=4) { spawnEnemy(x); }

                repeat = repeatSample;

            } else {
                spawnColumn(x);
                repeat--;
            }

        }
    }

    void spawnColumn(int x)
    {
        spawnPlatforms(x);

        //decide between dirt and water
        if(height == minHeight)
        {
            for(int y = 0; y < height; y++)
            {
                spawnTile(water, x, y);
                lastWaterX = x;
                lastWaterY = y;
            }

        } else {
            for(int y = 0; y < height; y++)
            {
                spawnTile(dirt, x, y);
            }
            spawnTile(grass, x, height);
            lastGrassX = x;   
            lastGrassY = height;
        }


    }

     void spawnEnemy(int x)
    {
        enemyTest = Instantiate(enemyTest, new Vector2(x, height + 1), Quaternion.identity);
        enemyTest.transform.parent = this.transform;
    }

    void spawnPlatforms(int x)
    {
        int diffHGrass = height - lastGrassY;
        int diffHWater = height - lastWaterY;
        int diffX = x - lastGrassX;
        int diffWaterGrass = lastWaterX - lastGrassX;

        if (diffHGrass == 2 && diffX >= 1)
        {
            spawnTile(stone, x - 1, lastGrassY + 1);
        } 
        else if (diffHGrass >= 3 && diffX >= 2)
        {
            spawnTile(cloudUpAndDown, x - 2, lastGrassY + 2);
        } 
        else if (diffHGrass >= 3 && repeatSample >= 4)
        {
            spawnTile(cloudUpAndDown, x - 2, lastGrassY + 2);
        } 
        else if (diffHGrass == 4 && diffX >= 1)
        {
            spawnTile(cloudUpAndDown, x - 2, lastGrassY + 2);
        }
        else if (diffHGrass == 3 && diffX >= 1)
        {
            spawnTile(stone, x - 1, lastGrassY + 2);
            spawnTile(stone, x - 2, lastGrassY + 1);
        } 
        else if (diffX >= 7 && lastWaterX == x - 1 && diffX <= 9)
        {
            spawnTile(cloudSideToSide, x - 3, lastGrassY + 1);
        }
        else if (diffX >= 10 && lastWaterX == x - 1)
        {
            spawnTile(cloudSideToSide, x - 3, lastGrassY + 1);
            spawnTile(cloudSideToSide, x - 6, lastGrassY + 2);
        }
         else if (diffX >= 6 && lastWaterY >= height - 1 && diffX <= 7)
        {
            spawnTile(cloudFalling, x - 3, lastGrassY + 1);
        }


    }

    void spawnTile(GameObject tiling, int width, int height)
    {
        tiling = Instantiate(tiling, new Vector2(width, height), Quaternion.identity);
        tiling.transform.parent = this.transform;
    }
}
