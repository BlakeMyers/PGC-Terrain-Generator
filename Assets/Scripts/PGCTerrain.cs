using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PGCTerrain : MonoBehaviour
{
    public int mapWidth = 256;
    public int mapHeight = 256;
    public int mapDepth = 20;

    private void start()
    {
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
    }
    TerrainData GenerateTerrain(TerrainData terrainData) {
        terrainData.heightmapResolution = mapWidth + 1;
        terrainData.size = new Vector3(mapWidth, mapDepth, mapHeight);
        float[,] heightmap = new float[mapWidth, mapHeight];
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++) {
                heightmap[x, y] = Random.Range(0.0f, 1.0f);
            }
        }
        terrainData.SetHeights(0, 0, heightmap);
        return terrainData;
    }

    public void ResizeTerrain() { 
    Terrain terrain2 = GetComponent<Terrain>();
    terrain2.terrainData = GenerateTerrain(terrain2.terrainData);
    }
}
