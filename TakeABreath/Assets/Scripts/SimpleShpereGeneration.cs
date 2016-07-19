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

    List<Vector3> vertices;

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

    public InNoise.NormalizeMode normalizeMode;

    public bool drawnMesh;

    void Start()
    {
        vertices = new List<Vector3>();
        var max = (nbLong + 1) * nbLat + 2;
        for (int i=0;i<max;i++)
        {
            vertices.Add(Vector3.zero);
        }
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

    public void OnValidate()
    {
        if (drawnMesh)
        {
            UpdateMesh();
        }
    }

    public void DrawSphere ()
    {
        float[,] NoiseMap = new float[nbLat + 1 + nbLat / 2, nbLong + 1 + nbLong / 2];

        NoiseMap = InNoise.GenerateNoiseMap(nbLat + 1 + nbLat / 2, nbLong + 1 + nbLong / 2, seed, noiseScale, octaves, persistance, lacunarity, offset, normalizeMode);
      

        
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
        vertices[vertices.Count - 1] = Vector3.up * -radius;
        #endregion

        #region Normales		
        Vector3[] normales = new Vector3[vertices.Count];
        for (int n = 0; n < vertices.Count; n++)
            normales[n] = vertices[n].normalized;
        #endregion

        #region UVs
        Vector2[] uvs = new Vector2[vertices.Count];
        uvs[0] = Vector2.up;
        uvs[uvs.Length - 1] = Vector2.zero;
        for (int lat = 0; lat < nbLat; lat++)
            for (int lon = 0; lon <= nbLong; lon++)
                uvs[lon + lat * (nbLong + 1) + 1] = new Vector2((float)lon / nbLong, 1f - (float)(lat + 1) / (nbLat + 1));
        #endregion

        #region Triangles
        int nbFaces = vertices.Count;
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
            triangles[i++] = vertices.Count - 1;
            triangles[i++] = vertices.Count - (lon + 2) - 1;
            triangles[i++] = vertices.Count - (lon + 1) - 1;
        }
        #endregion

        mesh.vertices = vertices.ToArray();
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

    public void UpdateMesh()
    {
        float[,] NoiseMap = new float[nbLat + 1 + nbLat / 2, nbLong + 1 + nbLong / 2];

        NoiseMap = InNoise.GenerateNoiseMap(nbLat + 1 + nbLat / 2, nbLong + 1 + nbLong / 2, seed, noiseScale, octaves, persistance, lacunarity, offset, normalizeMode);

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
        vertices[vertices.Count - 1] = Vector3.up * -radius;

        /*
        List<Vector3> vList = new List<Vector3>();
        for (int i = 0; i < vertices.Count; i++)
        {
            vList.Add(vertices[i]);
        }
        */
        GetComponent<MeshFilter>().mesh.SetVertices(vertices);
    }
}
