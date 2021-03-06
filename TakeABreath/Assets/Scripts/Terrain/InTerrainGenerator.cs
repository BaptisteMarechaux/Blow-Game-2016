﻿using UnityEngine;
using System.Collections;
using System;
using System.Threading;
using System.Collections.Generic;


public class InTerrainGenerator : MonoBehaviour {

    public enum DrawMode
    {
        NoiseMap, 
        ColorMap,
        Mesh,
        FalloffMap
    }

    public DrawMode drawMode;

    public const int mapChunkSize = 241;
    public int mapChunkS = 241;
    [Range(0,6)]
    public int editorLevelOfDetail;
    public float noiseScale=1;

    public int octaves=1;
    [Range(0f, 1f)]
    public float persistance=0;
    public float lacunarity=0;

    public int seed;
    public Vector2 offset;
    public float meshHeightMultiplier=1;
    public AnimationCurve meshHeightCurve;

    public bool autoUpdate;

    public InNoise.NormalizeMode normalizeMode;

    public TerrainType[] regions;

    public bool useFalloff;

    float[,] fallOffMap;

    Queue<TerrainThreadInfo<TerrainData>> terrainDataThreadInfoQueue = new Queue<TerrainThreadInfo<TerrainData>>();
    Queue<TerrainThreadInfo<MeshData>> meshDataThreadInfoQueue = new Queue<TerrainThreadInfo<MeshData>>();

    public void DrawTerrainInEditor()
    {
        TerrainData terrainData = GenerateTerrainData();

        InTerrainDisplay display = FindObjectOfType<InTerrainDisplay>();
        if (drawMode == DrawMode.NoiseMap)
            display.DrawTexture(InTextureGenerator.TextureFromHeightMap(terrainData.heightMap));
        else if (drawMode == DrawMode.ColorMap)
            display.DrawTexture(InTextureGenerator.TextureFromColorMap(terrainData.colorMap, mapChunkS, mapChunkS));
        else if (drawMode == DrawMode.Mesh)
            display.DrawMesh(InMeshGenerator.GenerateTerrainMesh(terrainData.heightMap, meshHeightMultiplier, meshHeightCurve, editorLevelOfDetail), InTextureGenerator.TextureFromColorMap(terrainData.colorMap, mapChunkS, mapChunkS));
        else if (drawMode == DrawMode.FalloffMap)
            display.DrawTexture(InTextureGenerator.TextureFromHeightMap(FalloffGenerator.GenerateFalloffMap(mapChunkSize)));
    }

    public void RequestTerrainData(Action<TerrainData> callback)
    {
        ThreadStart threadStart = delegate
        {
            TerrainDataThread(callback);
        };

        new Thread(threadStart).Start();
    }

    void TerrainDataThread(Action<TerrainData> callback)
    {
        TerrainData terrainData = GenerateTerrainData();
        lock (terrainDataThreadInfoQueue) //Pour attendre la fin de l'exécution 
        {
            terrainDataThreadInfoQueue.Enqueue(new TerrainThreadInfo<TerrainData>(callback, terrainData));
        }
        
    }

    public void RequestMeshData(TerrainData terrainData, int lod, Action<MeshData> callback)
    {
        ThreadStart threadStart = delegate
        {
            MeshDataThread(terrainData, lod, callback);
        };

        new Thread(threadStart).Start();
    } 

    void MeshDataThread(TerrainData terrainData, int lod, Action<MeshData> callback)
    {
        MeshData meshData = InMeshGenerator.GenerateTerrainMesh(terrainData.heightMap, meshHeightMultiplier, meshHeightCurve, lod);
        lock(meshDataThreadInfoQueue)
        {
            meshDataThreadInfoQueue.Enqueue(new TerrainThreadInfo<MeshData>(callback, meshData));
        }
    }

    void Update()
    {
        if(terrainDataThreadInfoQueue.Count > 0)
        {
            for (int i = 0; i < terrainDataThreadInfoQueue.Count;i++) {
                TerrainThreadInfo<TerrainData> threadInfo = terrainDataThreadInfoQueue.Dequeue();
                threadInfo.callback(threadInfo.parameter);
            }
        }

        if(meshDataThreadInfoQueue.Count > 0)
        {
            for(int i=0;i<meshDataThreadInfoQueue.Count;i++)
            {
                TerrainThreadInfo<MeshData> threadInfo = meshDataThreadInfoQueue.Dequeue();
                threadInfo.callback(threadInfo.parameter);
            }
        }
    }

    TerrainData GenerateTerrainData()
    {
        float[,] noiseMap = InNoise.GenerateNoiseMap(mapChunkS, mapChunkS,seed, noiseScale, octaves, persistance, lacunarity, offset, normalizeMode);

        Color[] colorMap = new Color[mapChunkS * mapChunkS];

        for(int y = 0; y < mapChunkS; y++)
        {
            for(int x=0;x<mapChunkS;x++)
            {
                if (useFalloff)
                {
                    noiseMap[x, y] = noiseMap[x, y] - fallOffMap[x, y];
                }
                float currentHeight = noiseMap[x, y];
                for(int i=0;i<regions.Length;i++)
                {
                    if(currentHeight <= regions[i].height)
                    {
                        colorMap[y * mapChunkS + x] = regions[i].color;
                        break;
                    }
                }
            }
        }

        return new TerrainData(noiseMap, colorMap);

    }

    void OnValidate()
    {
        if (lacunarity < 1)
            lacunarity = 1;
        if (octaves < 0)
            octaves = 0;

        fallOffMap = FalloffGenerator.GenerateFalloffMap(mapChunkSize);
        
    }

    struct TerrainThreadInfo<T>
    {
        public readonly Action<T> callback;
        public readonly T parameter;

        public TerrainThreadInfo(Action<T> callback, T parameter)
        {
            this.callback = callback;
            this.parameter = parameter;
        }

    }

}

[System.Serializable]
public struct TerrainType
{
    public string name;
    public float height;
    public Color color;

}

public struct TerrainData
{
    public readonly float[,] heightMap;
    public readonly Color[] colorMap;

    public TerrainData(float[,] heightMap, Color[] colorMap)
    {
        this.heightMap = heightMap;
        this.colorMap = colorMap;
    }
}
