using UnityEngine;

public class TestTerrainScript : MonoBehaviour
{


    float[,] testHeights = { { 0.01f, 0.01f, 0.01f, 0.01f, 0.01f, 0.01f, 0.01f, 0.01f, 0.01f, 0.01f, 0.01f, 0.01f, 0.01f, 0.01f }, { 0.01f, 0.01f, 0.01f, 0.01f, 0.01f, 0.01f, 0.01f, 0.01f, 0.01f, 0.01f, 0.01f, 0.01f, 0.01f, 0.01f } };
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Terrain tester;
    void Start()
    {
        tester = GetComponent<Terrain>();
        tester.terrainData.SetHeights(0, 500, testHeights);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
