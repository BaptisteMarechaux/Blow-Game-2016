using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InEndlessTerrain : MonoBehaviour {

    
    public LODInfo[] detailLevels;
    public static float maxViewDist;
    static InTerrainGenerator terrainGenerator;
    public Transform viewer;
    public Material terrainMaterial;

    public static Vector2 viewerPosition;
    int chunkSize;
    int chunksVisibleInViewDist;

    Dictionary<Vector2, TerrainChunk> terrainChunkDictonary = new Dictionary<Vector2, TerrainChunk>();
    List<TerrainChunk> terrainChunksVisibleLastUpdate = new List<TerrainChunk>();

    void Start()
    {
        terrainGenerator = FindObjectOfType<InTerrainGenerator>();
        maxViewDist = detailLevels[detailLevels.Length - 1].visibleDistThreshold;
        chunkSize = InTerrainGenerator.mapChunkSize - 1;
        chunksVisibleInViewDist = Mathf.RoundToInt(maxViewDist / chunkSize);
<<<<<<< HEAD

=======
>>>>>>> MultiPart
    }

    void Update()
    {
        viewerPosition = new Vector2(viewer.position.x, viewer.position.z);
        UpdateVisibleChunks();
    }

    void UpdateVisibleChunks()
    {
        for (int i = 0; i < terrainChunksVisibleLastUpdate.Count;i++) {
            terrainChunksVisibleLastUpdate[i].SetVisible(false);
        }

        int currentChunkCoordX = Mathf.RoundToInt(viewerPosition.x / chunkSize);
        int currentChunkCoordY = Mathf.RoundToInt(viewerPosition.y / chunkSize);

        for(int yOffset = -chunksVisibleInViewDist; yOffset <= chunksVisibleInViewDist;yOffset++)
        {
            for (int xOffset = -chunksVisibleInViewDist; xOffset <= chunksVisibleInViewDist; xOffset++)
            {
                Vector2 viewedChunkCoord = new Vector2(currentChunkCoordX + xOffset, currentChunkCoordY + yOffset);

                if(terrainChunkDictonary.ContainsKey (viewedChunkCoord))
                {
                    terrainChunkDictonary[viewedChunkCoord].UpdateTerrainChunk();
                    if(terrainChunkDictonary[viewedChunkCoord].IsVisible())
                    {
                        terrainChunksVisibleLastUpdate.Add(terrainChunkDictonary[viewedChunkCoord]);
                    }
                }
                else
                {
                    terrainChunkDictonary.Add(viewedChunkCoord, new TerrainChunk(viewedChunkCoord, chunkSize, detailLevels, transform, terrainMaterial));
                }

            }
        }
    }

    public class TerrainChunk
    {
        GameObject meshObject;
        Vector2 position;
        Bounds bounds;

        MeshRenderer meshRenderer;
        MeshFilter meshFilter;

        LODInfo[] detailLevels;
        LODMesh[] lodMeshes;

        TerrainData terrainData;
        bool terrainDataReceived;
        int previousLODIndex = -1;

        public TerrainChunk(Vector2 coord, int size, LODInfo[] detailLevels, Transform parent, Material material)
        {
            this.detailLevels = detailLevels;
            position = coord * size;
            bounds = new Bounds(position, Vector2.one * size);
            Vector3 positionV3 = new Vector3(position.x, 0, position.y);

            meshObject = new GameObject("Terrain Chunk");
            meshRenderer = meshObject.AddComponent<MeshRenderer>();
            meshFilter = meshObject.AddComponent<MeshFilter>();
            meshRenderer.material = material;
            

            meshObject.transform.position = positionV3;
            
            meshObject.transform.parent = parent;
            SetVisible(false);

            lodMeshes = new LODMesh[detailLevels.Length];
            for(int i=0;i<lodMeshes.Length;i++)
            {
                lodMeshes[i] = new LODMesh(detailLevels[i].lod);
            }
            
            terrainGenerator.RequestTerrainData(OnTerrainDataReceived);
        }

        void OnTerrainDataReceived(TerrainData terrainData)
        {
            this.terrainData = terrainData;
            terrainDataReceived = true;
<<<<<<< HEAD
            //meshRenderer.material.mainTexture = InTextureGenerator.TextureFromColorMap(terrainData.colorMap, 240, 240);
=======
            meshRenderer.material.mainTexture = InTextureGenerator.TextureFromColorMap(terrainData.colorMap, 240, 240);
>>>>>>> MultiPart
            //terrainGenerator.RequestMeshData(terrainData, LODInfo,  OnMeshDataReceived);
        }

        /*
        void OnMeshDataReceived(MeshData meshData)
        {
            Debug.Log("meshReceived");
            meshFilter.mesh = meshData.CreateMesh();
        }
        */
        public void UpdateTerrainChunk()
        {
            if(terrainDataReceived)
            {
                float viewerDistFromNearestEdge = Mathf.Sqrt(bounds.SqrDistance(viewerPosition));
                bool visible = viewerDistFromNearestEdge <= maxViewDist;

                if (visible)
                {
                    int lodIndex = 0;
                    for (int i = 0; i < detailLevels.Length - 1; i++)
                    {
                        if (viewerDistFromNearestEdge > detailLevels[i].visibleDistThreshold)
                        {
                            lodIndex = i + 1;
                        }
                        else
                        {
                            break;
                        }
                    }

                    if (lodIndex != previousLODIndex)
                    {
                        LODMesh lodMesh = lodMeshes[lodIndex];
                        if (lodMesh.hasMesh)
                        {
                            previousLODIndex = lodIndex;
                            meshFilter.mesh = lodMesh.mesh;
                        }
                        else if (!lodMesh.hasRequestedMesh)
                        {
                            lodMesh.RequestMesh(terrainData);
                        }
                    }
                }
                SetVisible(visible);
            }
            
        }

        public void SetVisible(bool visible)
        {
            meshObject.SetActive(visible);
        }

        public bool IsVisible()
        {
            return meshObject.activeSelf;
        }
    }

    class LODMesh
    {
        public Mesh mesh;
        public bool hasRequestedMesh;
        public bool hasMesh;
        int lod;

        public LODMesh(int lod)
        {
            this.lod = lod;
        }
        
        void OnMeshDataReceived(MeshData meshData)
        {
            mesh = meshData.CreateMesh();
            
            hasMesh = true;
        }

        public void RequestMesh(TerrainData terrainData)
        {
            hasRequestedMesh = true;
            terrainGenerator.RequestMeshData(terrainData, lod, OnMeshDataReceived);
        }

        
    }

    [System.Serializable]
    public struct LODInfo
    {
        public int lod;
        public float visibleDistThreshold;
    }

}
