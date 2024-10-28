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

    void Start()
    {

        proceduralGen = new ProceduralGen(this, mapSize);

        r = new System.Random();


        for (float i = 0; i < 160; i++)
        {
            Vector3 position = new(i / 2, 0, 0);
            position += blockOffset;
            PlaceBlock(position);
        }

        proceduralGen.TestGenerate();

    }

    // Update is called once per frame
    // void Update()
    // {

    // }


    public void Render3DTileMap(int[,,] threeDArr)
    {
        for (int x = 0; x < threeDArr.GetUpperBound(0); x++)
        {
            for (int y = 0; y < threeDArr.GetUpperBound(1); y++)
            {
                for (int z = 0; z < threeDArr.GetUpperBound(2); z++)
                {
                    if (threeDArr[x, y, z] == 1)
                    {
                        Vector3 pos = new(x, y, z);
                        PlaceBlock(pos * blockSize);
                    }
                }
            }
        }
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
}
