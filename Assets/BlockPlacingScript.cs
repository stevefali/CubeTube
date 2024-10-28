using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class BlockPlacingScript : MonoBehaviour
{

    public GameObject block;
    public GameObject block1;
    public GameObject block2;
    public GameObject block3;

    public Vector3Int mapSize;
    public float blockSize;

    private Vector3 blockOffset = new(0.25f, 0.25f, 0.25f);

    private ProceduralGen proceduralGen;

    private System.Random r;

    private static bool isReady = false;

    void Start()
    {
        r = new System.Random();


        float floorSeed = r.Next(10000, 10000000);
        float wallSeed = r.Next(10000, 10000000);

        proceduralGen = new ProceduralGen(this, mapSize, floorSeed, wallSeed);

        proceduralGen.Generate();

    }


    public void RenderFloorMap(int[,] floorArray)
    {
        for (int x = 0; x <= floorArray.GetUpperBound(0); x++)
        {
            for (int y = 0; y <= floorArray.GetUpperBound(1); y++)
            {
                if (floorArray[x, y] == 1)
                {
                    Vector3 pos = new(x, y, 0);
                    PlaceBlock((pos * blockSize) + blockOffset);
                }
            }
        }
        isReady = true;
    }

    public void RenderWallMap(int[,] wallArray)
    {
        for (int x = 0; x <= wallArray.GetUpperBound(0); x++)
        {
            for (int z = 0; z <= wallArray.GetUpperBound(1); z++)
            {
                if (wallArray[x, z] == 1)
                {
                    Vector3 pos = new(x, 0, z);
                    PlaceBlock((pos * blockSize) + blockOffset);
                }
            }
        }
        isReady = true;
    }



    public void PlaceBlock(Vector3 position)
    {
        int selector = r.Next(0, 3);
        switch (selector)
        {
            case 0:
                Instantiate(block, position, transform.rotation);
                break;
            case 1:
                Instantiate(block1, position, transform.rotation);
                break;
            case 2:
                Instantiate(block2, position, transform.rotation);
                break;
            case 3:
                Instantiate(block3, position, transform.rotation);
                break;
        }

    }

    public static bool GetIsReady()
    {
        if (isReady)
        {
            isReady = false;
            return true;
        }
        return false;
    }

    public static Vector3 GetStartPos()
    {
        return new Vector3(0, ProceduralGen.startHeight, 0);
    }
}
