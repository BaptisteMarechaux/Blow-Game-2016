using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimpleShpereGeneration : MonoBehaviour {
    public float radius = 10f;
    // Longitude |||
    public int nbLong = 240;
    // Latitude ---
    public int nbLat = 240;

    public float[] perlinValues;

    Vector3[] vertices;

    [Range(0, 6)]
    public int editorLevelOfDetail;
    public float noiseScale = 1;

    public int octaves = 1;
    [Range(0f, 1f)]
    public float persistance = 0;
    public float lacunarity = 0;

    public int seed;
    public Vector2 offset;
    public float meshHeightMultiplier = 1;
    public AnimationCurve meshHeightCurve;

    bool drawnMesh;

    void Start()
    {

        DrawSphere();

        NoiseSphere();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.U))
        {
            UpdateMesh();
        }
    }

    void OnValidate()
    {
        if (drawnMesh)
        {
            UpdateMesh();
        }
    }

    public void DrawSphere ()
    {
        float[,] NoiseMap = new float[nbLat + 1 + nbLat / 2, nbLong + 1 + nbLong / 2];

        System.Random pnrg = new System.Random(seed);
        Vector2[] octaveOffsets = new Vector2[octaves];
        for (int a = 0; a < octaves; a++)
        {
            float offsetX = pnrg.Next(-100000, 100000) + offset.x;
            float offsetY = pnrg.Next(-100000, 100000) + offset.y;
            octaveOffsets[a] = new Vector2(offsetX, offsetY);
        }

        noiseScale = noiseScale < 0 ? 0.0001f : noiseScale;

        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        float halfWidth = nbLat / 2f;
        float halfHeight = nbLong / 2f;

        for (int y = 0; y < nbLat + 1 + nbLat/2; y++)
        {
            for (int x = 0; x < nbLong + 1 + nbLong/2; x++)
            {
                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;

                for (int a = 0; a < octaves; a++)
                {
                    float sampleX = (x - halfWidth) / noiseScale * frequency + octaveOffsets[a].x;
                    float sampleY = (y - halfHeight) / noiseScale * frequency + octaveOffsets[a].y;

                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                    noiseHeight += perlinValue * amplitude;
                    //NoiseMap[x, y] = perlinValue;

                    amplitude *= persistance;
                    frequency *= lacunarity;
                }

                if (noiseHeight > maxNoiseHeight)
                {
                    maxNoiseHeight = noiseHeight;
                }
                else if (noiseHeight < minNoiseHeight)
                {
                    minNoiseHeight = noiseHeight;
                }

                NoiseMap[x, y] = noiseHeight;

            }
        }

        for (int y = 0; y < nbLat + 1 + nbLat / 2; y++)
        {
            for (int x = 0; x < nbLong + 1 + nbLong / 2; x++)
            {
                NoiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, NoiseMap[x, y]);
            }
        }

        vertices = new Vector3[(nbLong + 1) * nbLat + 2];
        MeshFilter filter = gameObject.AddComponent<MeshFilter>();
        Mesh mesh = filter.mesh;
        mesh.Clear();


        float _pi = Mathf.PI;
        float _2pi = _pi * 2f;
        #region Vertices
        vertices[0] = Vector3.up * radius;

        for (int lat = 0; lat < nbLat; lat++)
        {
            float a1 = _pi * (float)(lat + 1) / (nbLat + 1);
            float sin1 = Mathf.Sin(a1);
            float cos1 = Mathf.Cos(a1);

            for (int lon = 0; lon <= nbLong; lon++)
            {
                float a2 = _2pi * (float)(lon == nbLong ? 0 : lon) / nbLong;
                float sin2 = Mathf.Sin(a2);
                float cos2 = Mathf.Cos(a2);
                
                vertices[lon + lat * (nbLong + 1) + 1] = new Vector3(sin1 * cos2, cos1, sin1 * sin2) * (radius + NoiseMap[lon, lat] * meshHeightMultiplier * meshHeightCurve.Evaluate(NoiseMap[lon, lat]));
            }
        }
        vertices[vertices.Length - 1] = Vector3.up * -radius;
        #endregion

        #region Normales		
        Vector3[] normales = new Vector3[vertices.Length];
        for (int n = 0; n < vertices.Length; n++)
            normales[n] = vertices[n].normalized;
        #endregion

        #region UVs
        Vector2[] uvs = new Vector2[vertices.Length];
        uvs[0] = Vector2.up;
        uvs[uvs.Length - 1] = Vector2.zero;
        for (int lat = 0; lat < nbLat; lat++)
            for (int lon = 0; lon <= nbLong; lon++)
                uvs[lon + lat * (nbLong + 1) + 1] = new Vector2((float)lon / nbLong, 1f - (float)(lat + 1) / (nbLat + 1));
        #endregion

        #region Triangles
        int nbFaces = vertices.Length;
        int nbTriangles = nbFaces * 2;
        int nbIndexes = nbTriangles * 3;
        int[] triangles = new int[nbIndexes];

        //Top Cap
        int i = 0;
        for (int lon = 0; lon < nbLong; lon++)
        {
            triangles[i++] = lon + 2;
            triangles[i++] = lon + 1;
            triangles[i++] = 0;
        }

        //Middle
        for (int lat = 0; lat < nbLat - 1; lat++)
        {
            for (int lon = 0; lon < nbLong; lon++)
            {
                int current = lon + lat * (nbLong + 1) + 1;
                int next = current + nbLong + 1;

                triangles[i++] = current;
                triangles[i++] = current + 1;
                triangles[i++] = next + 1;

                triangles[i++] = current;
                triangles[i++] = next + 1;
                triangles[i++] = next;
            }
        }

        //Bottom Cap
        for (int lon = 0; lon < nbLong; lon++)
        {
            triangles[i++] = vertices.Length - 1;
            triangles[i++] = vertices.Length - (lon + 2) - 1;
            triangles[i++] = vertices.Length - (lon + 1) - 1;
        }
        #endregion

        mesh.vertices = vertices;
        mesh.normals = normales;
        mesh.uv = uvs;
        mesh.triangles = triangles;

        mesh.RecalculateBounds();
        mesh.Optimize();

        drawnMesh = true;
    }

    public void NoiseSphere(/*float[,] heightMap, AnimationCurve heightCurve, float heightMultiplier*/)
    {
        /*
        MeshFilter filter = GetComponent<MeshFilter>();

        for(int i=0;i< filter.mesh.vertexCount; i++)
        {
            vertices[i].y = Mathf.PerlinNoise(i%16, i % 24);//heightCurve.Evaluate(heightMap[x, y]) * heightMultiplier;
        }
        */

        Material m = GetComponent<MeshRenderer>().materials[0];

        MeshCollider collider = gameObject.AddComponent<MeshCollider>();

        collider.sharedMesh = GetComponent<MeshFilter>().mesh;
    }

    void UpdateMesh()
    {
        float[,] NoiseMap = new float[nbLat + 1 + nbLat / 2, nbLong + 1 + nbLong / 2];

        System.Random pnrg = new System.Random(seed);
        Vector2[] octaveOffsets = new Vector2[octaves];
        for (int a = 0; a < octaves; a++)
        {
            float offsetX = pnrg.Next(-100000, 100000) + offset.x;
            float offsetY = pnrg.Next(-100000, 100000) + offset.y;
            octaveOffsets[a] = new Vector2(offsetX, offsetY);
        }

        noiseScale = noiseScale < 0 ? 0.0001f : noiseScale;

        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        float halfWidth = nbLat / 2f;
        float halfHeight = nbLong / 2f;

        for (int y = 0; y < nbLat + 1 + nbLat / 2; y++)
        {
            for (int x = 0; x < nbLong + 1 + nbLong / 2; x++)
            {
                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;

                for (int a = 0; a < octaves; a++)
                {
                    float sampleX = (x - halfWidth) / noiseScale * frequency + octaveOffsets[a].x;
                    float sampleY = (y - halfHeight) / noiseScale * frequency + octaveOffsets[a].y;

                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                    noiseHeight += perlinValue * amplitude;
                    //NoiseMap[x, y] = perlinValue;

                    amplitude *= persistance;
                    frequency *= lacunarity;
                }

                if (noiseHeight > maxNoiseHeight)
                {
                    maxNoiseHeight = noiseHeight;
                }
                else if (noiseHeight < minNoiseHeight)
                {
                    minNoiseHeight = noiseHeight;
                }

                NoiseMap[x, y] = noiseHeight;

            }
        }

        for (int y = 0; y < nbLat + 1 + nbLat / 2; y++)
        {
            for (int x = 0; x < nbLong + 1 + nbLong / 2; x++)
            {
                NoiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, NoiseMap[x, y]);
            }
        }

        float _pi = Mathf.PI;
        float _2pi = _pi * 2f;
        vertices[0] = Vector3.up * radius;

        for (int lat = 0; lat < nbLat; lat++)
        {
            float a1 = _pi * (float)(lat + 1) / (nbLat + 1);
            float sin1 = Mathf.Sin(a1);
            float cos1 = Mathf.Cos(a1);

            for (int lon = 0; lon <= nbLong; lon++)
            {
                float a2 = _2pi * (float)(lon == nbLong ? 0 : lon) / nbLong;
                float sin2 = Mathf.Sin(a2);
                float cos2 = Mathf.Cos(a2);

                vertices[lon + lat * (nbLong + 1) + 1] = new Vector3(sin1 * cos2, cos1, sin1 * sin2) * (radius + NoiseMap[lon, lat] * meshHeightMultiplier * meshHeightCurve.Evaluate(NoiseMap[lon, lat]));
            }
        }
        vertices[vertices.Length - 1] = Vector3.up * -radius;

        List<Vector3> vList = new List<Vector3>();
        for (int i = 0; i < vertices.Length; i++)
        {
            vList.Add(vertices[i]);
        }
        GetComponent<MeshFilter>().mesh.SetVertices(vList);
    }
}
