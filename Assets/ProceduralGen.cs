using UnityEngine;

public class ProceduralGen
{
    private BlockPlacingScript placer;
    public ProceduralGen(BlockPlacingScript placer, Vector3Int mapSize)
    {
        this.placer = placer;
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

}
