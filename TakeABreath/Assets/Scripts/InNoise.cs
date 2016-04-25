using UnityEngine;
using System.Collections;

public static class InNoise {
    public enum NormalizeMode { Local, Global };

    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, int seed, float scale, int octaves, float persistance, float lacunarity, Vector2 offset, NormalizeMode normalizeMode)
    {
        float[,] NoiseMap = new float[mapWidth, mapHeight];

        System.Random pnrg = new System.Random(seed);
        Vector2[] octaveOffsets = new Vector2[octaves];

        float maxPossibleHeight = 0;
        float amplitude = 1;
        float frequency = 1;

        for (int i=0;i<octaves;i++)
        {
            float offsetX = pnrg.Next(-100000, 100000) + offset.x;
            float offsetY = pnrg.Next(-100000, 100000) + offset.y;
            octaveOffsets[i] = new Vector2(offsetX, offsetY);

            maxPossibleHeight += amplitude;
            amplitude *= persistance;
        }

        scale = scale <= 0 ? 0.0001f : scale;

        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        float halfWidth = mapWidth / 2f;
        float halfHeight = mapHeight / 2f;

        for(int y=0;y < mapHeight;y++)
        {
            for(int x = 0;x < mapWidth;x++)
            {
                amplitude = 1;
                frequency = 1;
                float noiseHeight = 0;

                for (int i=0;i<octaves;i++)
                {
                    float sampleX = (x-halfWidth) / scale * frequency + octaveOffsets[i].x;
                    float sampleY = (y-halfHeight) / scale * frequency + octaveOffsets[i].y;

                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 -1;
                    noiseHeight += perlinValue * amplitude;
                    //NoiseMap[x, y] = perlinValue;

                    amplitude *= persistance;
                    frequency *= lacunarity;
                }

                if(noiseHeight > maxNoiseHeight)
                {
                    maxNoiseHeight = noiseHeight;
                } 
                else if(noiseHeight < minNoiseHeight)
                {
                    minNoiseHeight = noiseHeight;
                }

                NoiseMap[x, y] = noiseHeight;

            }
        }

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                if (normalizeMode == NormalizeMode.Local)
                {
                    NoiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, NoiseMap[x, y]);
                }
                else
                {
                    float normalizedHeight = (NoiseMap[x, y] + 1) / (maxPossibleHeight / 0.9f);
                    NoiseMap[x, y] = Mathf.Clamp(normalizedHeight, 0, int.MaxValue);
                }
            }
        }
        /*
        for (int y=0;y<mapHeight;y++)
        {
            for(int x=0;x<mapWidth;x++)
            {
                NoiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, NoiseMap[x, y]);
            }
        }
        */

        return NoiseMap;
    } 
}
