using System;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class BlockPlacingScript : MonoBehaviour
{

    public GameObject floorBlock;
    public GameObject wallBlock;

    public Vector3Int mapSize;
    public float blockSize;

    private Vector3 floorBlockOffset = new(0.25f, 0.25f, 16f);
    private Vector3 wallBlockOffset = new(0.25f, 0.25f, 0.25f);

    private Vector3 verticalSpace = new(0f, 7.5f, 0f);
    private Vector3 horizontalSpace = new(0f, 0f, 5f);


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
                    PlaceBlock((pos * blockSize), false);
                }
            }
        }
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
                    PlaceBlock((pos * blockSize), true);
                }
            }
        }
        isReady = true;
    }



    public void PlaceBlock(Vector3 position, bool isWall)
    {
        if (isWall)
        {
            Instantiate(wallBlock, position + wallBlockOffset, transform.rotation);
            print("!");
            Instantiate(wallBlock, position + wallBlockOffset + horizontalSpace, transform.rotation);
        }
        else
        {
            Instantiate(floorBlock, position + floorBlockOffset, transform.rotation);
            print("!!");
            Instantiate(floorBlock, position + floorBlockOffset + verticalSpace, transform.rotation);
        }

    }

    // public static bool GetIsReady()
    // {
    //     if (isReady)
    //     {
    //         isReady = false;
    //         return true;
    //     }
    //     return false;
    // }

    // public static Vector3 GetStartPos()
    // {
    //     return new Vector3(1.5f, ProceduralGen.startHeight, ProceduralGen.startZPos);

    // }
}
