using System.Collections.Generic;
using UnityEngine;

public class ProceduralGen
{
    private BlockPlacingScript placer;
    private Vector3Int mapSize;
    private float floorSeed;
    private float wallSeed;

    public static float startHeight;

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
        int[,] emptyFloorArr = InitializeFloorArray(mapSize.x, mapSize.y);
        int[,] finishedFloorMap = GenerateFloorMap(emptyFloorArr, 7, floorSeed);

        placer.RenderFloorMap(finishedFloorMap);
    }


    private int[,] InitializeFloorArray(int width, int height)
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
                floorMap[x, y] = 1;
            }
        }
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
                for (int z = Mathf.FloorToInt(currHeight); z > 0; z--)
                {
                    map[x, z] = 1;
                }
                currHeight += heightChange;
            }
        }

        return map;
    }

}
