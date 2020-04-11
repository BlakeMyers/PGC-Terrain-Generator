using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PGCTerrain : MonoBehaviour
{
    public int mapWidth = 256;
    public int mapHeight = 256;
    public int mapDepth = 20;
    public int seed = 1;
    public float scale = 1.0f;
    public Vector2 Offsets = new Vector2(100, 100);
    public int numOctaves = 1;
    public float persistance = 0.5f;
    public float lacunarity = 0.5f;

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

    TerrainData MultiPerlin(TerrainData terrainData) {
        if (scale <= 0){
            scale = 0.0001f;
        }
        System.Random prng = new System.Random(seed);
        Vector2[] octaveOffsets = new Vector2[numOctaves];
        for (int i = 0; i < numOctaves; i++)
        {
            float OffsetX = prng.Next(-100000, 100000) +Offsets.x;
            float OffsetY = prng.Next(-100000, 100000) + Offsets.y;
            octaveOffsets[i] = new Vector2(OffsetX, OffsetY);
        }
        float[,] heightmap = new float[mapWidth, mapHeight];
        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        for (int x = 0; x < mapHeight; x++)
        {
            for (int y = 0; y < mapWidth; y++)
            {
                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;

                for (int i = 0; i < numOctaves; i++) {
                    float sampleX = x / scale * frequency + octaveOffsets[i].x;
                    float sampleY = y / scale * frequency+ octaveOffsets[i].y;

                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                    noiseHeight += perlinValue * amplitude;

                    amplitude *= persistance;
                    frequency *= lacunarity;
                }

                if (noiseHeight > maxNoiseHeight)
                {
                    maxNoiseHeight = noiseHeight;
                }
                else if (noiseHeight < minNoiseHeight) {
                    minNoiseHeight = noiseHeight;
                }
                heightmap[x, y] = noiseHeight;
            }
        }
        for (int x = 0; x < mapHeight; x++)
        {
            for (int y = 0; y < mapWidth; y++)
            {
                heightmap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, heightmap[x, y]);
            }
        }

                terrainData.SetHeights(0, 0, heightmap);
        return terrainData;
    }
    TerrainData Perlin(TerrainData terrainData) {
        if (scale <= 0) {
            scale = 0.0001f;
        }
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

    public void RandomTerrain() { 
    Terrain terrain = GetComponent<Terrain>();
    terrain.terrainData = GenerateTerrain(terrain.terrainData);
    }
    public void SinglePerlin() {
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = Perlin(terrain.terrainData);
    }
    public void MultiplePerlin() {
        Terrain terrain = GetComponent<Terrain>();
        terrain.terrainData = MultiPerlin(terrain.terrainData);
    }
    float calcHeight(int x, int y) {
        float xCoord = (float)x / scale + Offsets.x;
        float yCoord = (float)y /scale + Offsets.y;
        //float xCoord = (float)x / mapWidth * scale + xOffset;
        //float yCoord = (float)y / mapHeight * scale + yOffset;

        return Mathf.PerlinNoise(xCoord, yCoord);
    }
}
