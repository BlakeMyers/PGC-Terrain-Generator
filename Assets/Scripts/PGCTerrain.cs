using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PGCTerrain : MonoBehaviour
{
    public int mapWidth = 256;
    public int mapHeight = 256;
    public int mapDepth = 20;
    public float scale = 1.0f;
    public float xOffset = 100.0f;
    public float yOffset = 100.0f;

    private void start()
    {
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
    }
    TerrainData GenerateTerrain(TerrainData terrainData) {
        terrainData.heightmapResolution = mapWidth + 1;
        terrainData.size = new Vector3(mapWidth, mapDepth, mapHeight);
        float[,] heightmap = new float[mapWidth, mapHeight];
        for (int x = 0; x < mapHeight; x++)
        {
            for (int y = 0; y < mapWidth; y++) {
                heightmap[x, y] = Random.Range(0.0f, 1.0f);
            }
        }
        terrainData.SetHeights(0, 0, heightmap);
        return terrainData;
    }
    TerrainData Perlin(TerrainData terrainData) {
        terrainData.heightmapResolution = mapWidth + 1;
        terrainData.size = new Vector3(mapWidth, mapDepth, mapHeight);
        float[,] heightmap = new float[mapWidth, mapHeight];
        for (int x = 0; x < mapHeight; x++)
        {
            for (int y = 0; y < mapWidth; y++)
            {
                heightmap[x, y] = calcHeight(x,y);
            }
        }
        terrainData.SetHeights(0, 0, heightmap);
        return terrainData;
    }

    public void ResizeTerrain() { 
    Terrain terrain = GetComponent<Terrain>();
    terrain.terrainData = GenerateTerrain(terrain.terrainData);
    }
    public void SinglePerlin() {
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = Perlin(terrain.terrainData);
    }

    float calcHeight(int x, int y) {
        float xCoord = (float)x / mapWidth * scale + xOffset;
        float yCoord = (float)y / mapHeight * scale+ yOffset;

        return Mathf.PerlinNoise(xCoord, yCoord);
    }
}
