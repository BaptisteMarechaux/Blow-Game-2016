using UnityEngine;
using System.Collections;

public class InTreeGenerator : MonoBehaviour {

    public InTerrainGenerator terrainGenerator;

    public GameObject[] trees;

    public float maxTreeNumber;
    public float density;

    public float terrainSize;

    float[,] noiseMap;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void GenerateTrees()
    {
        float[,] noiseMap = InNoise.GenerateNoiseMap(terrainGenerator.mapChunkS, terrainGenerator.mapChunkS, terrainGenerator.seed, terrainGenerator.noiseScale, terrainGenerator.octaves, terrainGenerator.persistance, terrainGenerator.lacunarity, terrainGenerator.offset, terrainGenerator.normalizeMode);

        float [,] fallOffMap = FalloffGenerator.GenerateFalloffMap(terrainGenerator.mapChunkS);

        for (int y = 0; y < terrainGenerator.mapChunkS; y++)
        {
            for (int x = 0; x < terrainGenerator.mapChunkS; x++)
            {
                if (terrainGenerator.useFalloff)
                {
                    noiseMap[x, y] = noiseMap[x, y] - fallOffMap[x, y];
                }
                float currentHeight = noiseMap[x, y];
            }
        }
    }
}
