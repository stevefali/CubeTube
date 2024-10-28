using UnityEngine;

public class ProceduralGen
{
    private BlockPlacingScript placer;
    private Vector3Int mapSize;
    public ProceduralGen(BlockPlacingScript placer, Vector3Int mapSize)
    {
        this.placer = placer;
        this.mapSize = mapSize;
    }



    public void TestGenerate()
    {
        TestPlacing();
    }

    private void TestPlacing()
    {
        Vector3 pos = new(15f, 15f, 15f);
        placer.PlaceBlock(pos);
    }


    private int[,,] InitializeFloorArray(int width, int height, int length)
    {
        int[,,] floorArray = new int[width, height, length];

        for (int x = 0; x < floorArray.GetUpperBound(0); x++)
        {
            for (int y = 0; y < floorArray.GetUpperBound(1); y++)
            {
                for (int z = 0; z < floorArray.GetUpperBound(2); z++)
                {
                    floorArray[x, y, z] = 0;
                }
            }
        }
        return floorArray;
    }




}
