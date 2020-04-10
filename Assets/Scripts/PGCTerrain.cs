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
        terrainData.size = new Vector3(mapWidth, mapDepth, mapHeight);
        return terrainData;
    }

    public void ResizeTerrain() { 
    Terrain terrain2 = GetComponent<Terrain>();
    terrain2.terrainData = GenerateTerrain(terrain2.terrainData);
    }
}
