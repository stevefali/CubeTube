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


    // The floor array will be arranged as (z, y, x)
    private int[,,] InitializeFloorArray(int width, int height, int length)
    {
        int[,,] floorArray = new int[length, height, width];

        for (int z = 0; z < floorArray.GetUpperBound(0); z++)
        {
            for (int y = 0; y < floorArray.GetUpperBound(1); y++)
            {
                for (int x = 0; x < floorArray.GetUpperBound(2); x++)
                {
                    floorArray[z, y, x] = 0;
                }
            }
        }
        return floorArray;
    }


}
