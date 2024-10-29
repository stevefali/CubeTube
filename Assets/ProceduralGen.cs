using System.Collections.Generic;
using System.Linq;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class ProceduralGen
{
    private BlockPlacingScript placer;
    private Vector3Int mapSize;
    private float floorSeed;
    private float wallSeed;

    public static float startHeight;
    public static float startZPos;

    private System.Random r = new System.Random();

    public ProceduralGen(BlockPlacingScript placer, Vector3Int mapSize, float floorSeed, float wallSeed)
    {
        this.placer = placer;
        this.mapSize = mapSize;
        this.floorSeed = floorSeed;
        this.wallSeed = wallSeed;
    }


    public void Generate()
    {
        int[,] emptyFloorArr = InitializeArray(mapSize.x, mapSize.y);
        int[,] finishedFloorMap = GenerateFloorMap(emptyFloorArr, 7, floorSeed);

        placer.RenderFloorMap(finishedFloorMap);

        int[,] emptyWallArr = InitializeArray(mapSize.x, mapSize.y);
        int[,] finishedWallMap = GenerateWallMap(emptyWallArr, wallSeed, 10);

        placer.RenderWallMap(finishedWallMap);
    }


    private int[,] InitializeArray(int width, int height)
    {
        int[,] floorArray = new int[width, height];

        for (int x = 0; x <= floorArray.GetUpperBound(0); x++)
        {
            for (int y = 0; y <= floorArray.GetUpperBound(1); y++)
            {
                floorArray[x, y] = 0;
            }
        }
        return floorArray;
    }


    private int[,] GenerateFloorMap(int[,] floorMap, int minSectionSize, float floorSeed)
    {
        System.Random hashCode = new System.Random(floorSeed.GetHashCode());

        int lastHeight = Random.Range(0, floorMap.GetUpperBound(1));
        startHeight = lastHeight;

        int nextMove = 0;

        int sectionSize = 0;

        // The floor will be flat width-wise for now, so we only have to pass through z axis once.
        for (int x = 0; x <= floorMap.GetUpperBound(0); x++)
        {

            nextMove = hashCode.Next(2);

            // If long enough and not too low or high, use the last height determine whether up or down. (Or do nothing)
            if (nextMove == 0 && lastHeight > 2 && sectionSize > minSectionSize)
            {
                int stepAmount = r.Next(1, 4);
                lastHeight -= stepAmount;
                sectionSize = 0;
            }
            else if (nextMove == 1 && lastHeight < floorMap.GetUpperBound(1) && sectionSize > minSectionSize)
            {
                int stepAmount = r.Next(1, 4);
                lastHeight += stepAmount;
                sectionSize = 0;
            }
            sectionSize++;

            for (int y = lastHeight; y > lastHeight - 3; y--)
            {
                if (y >= 0 && y < floorMap.GetUpperBound(1))
                {
                    floorMap[x, y] = 1;
                }

            }
        }

        // // For the player's starting y position, we can get the highest index with value 1 in the first row.
        // int[] firstRow = new int[floorMap.GetLength(1)];
        // for (int i = 0; i < floorMap.GetLength(1); i++)
        // {
        //     firstRow[i] = floorMap[0, i];
        // }
        // // The block is at the last index with value 1 in the row
        // int firstBlock = firstRow.Count(num => num == 1);
        // startHeight = firstBlock;
        return floorMap;
    }



    private int[,] GenerateWallMap(int[,] map, float wallSeed, int interval)
    {

        int newPoint, points;

        float reduction = 0.5f;

        Vector2Int currentPos, lastPos;

        List<int> noiseX = new List<int>();
        List<int> noiseZ = new List<int>();

        //Generate the noise
        for (int x = 0; x < map.GetUpperBound(0); x += interval)
        {
            newPoint = Mathf.FloorToInt((Mathf.PerlinNoise(x, (wallSeed * reduction))) * map.GetUpperBound(1));
            noiseZ.Add(newPoint);
            noiseX.Add(x);
        }

        points = noiseZ.Count;
        //Start at 1 so we have a previous position already
        for (int i = 1; i < points; i++)
        {

            currentPos = new Vector2Int(noiseX[i], noiseZ[i]);

            lastPos = new Vector2Int(noiseX[i - 1], noiseZ[i - 1]);

            Vector2 diff = currentPos - lastPos;

            float heightChange = diff.y / interval;

            float currHeight = lastPos.y;

            for (int x = lastPos.x; x < currentPos.x; x++)
            {
                for (int z = Mathf.FloorToInt(currHeight); z > (Mathf.FloorToInt(currHeight) - 4); z--)
                {
                    if (z >= 0)
                    {
                        map[x, z] = 1;
                    }

                }
                currHeight += heightChange;
            }
        }

        // We can reference where to start the player's z position based on the blocks in the third row (roughly the x position)
        int[] thirdRowFromStart = new int[map.GetLength(1)];
        for (int i = 0; i < map.GetLength(1); i++)
        {
            thirdRowFromStart[i] = map[2, i];
        }
        // The block is at the last index with value 1 in the row
        int mostInnerblock = thirdRowFromStart.Count(num => num == 1);

        startZPos = mostInnerblock + 2.5f;
        return map;
    }

}
