using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class TerrainObjectsEditor : EditorWindow {
    GameObject _prefab, _mountainprefab, _treePrefab; //Prefab d'arbre, Prefab de bout de montagne
    int _popRate = 5;
    int _width = 129;
	int _depth = 129;
    int _mountainsCount = 0;
    int _mountainPopRate = 5;
    [SerializeField]
    float _treeDensity = 1;
    [SerializeField]
    int _treeCount = 1;
    Object[] _mountainTypes = new Object[10];
    [SerializeField]
    Object[] _treeTypes = new Object[20];
    GameObject _treeNode;
    
    //For placing trees
    [SerializeField]
    int terrainSize;
    [SerializeField]
    float terrainScale = 1;
    float placementScale;
    float[,] noiseMap = new float[241,241];
    [SerializeField]
    AnimationCurve heightCurve = new AnimationCurve();

    [SerializeField]
    float minimumHeight;

    //Noise Parameters
    [SerializeField]
    int seed;
    [SerializeField]
    int octaves;
    [SerializeField]
    float lacunarity, noiseScale, persistance, heightMultiplier;
    [SerializeField]
    Vector2 offset;
    InNoise.NormalizeMode normalizeMode = InNoise.NormalizeMode.Global;

    List<Transform> instanciatedObjects = new List<Transform>();
    List<Transform> instanciatedTrees = new List<Transform>();

    bool editionActivated;

    bool[,] nodes;

    Vector3[] verts;

    public Vector2 scrollPosition = Vector2.zero;

    float terrainMaxHeight = 300;
    float treeMaxHeight=300;
    public void OnGUI()
    {
       
        EditorGUILayout.BeginVertical();
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Terrain Editor");
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        terrainSize = EditorGUILayout.IntSlider("Terrain Size", terrainSize, 0, 241);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        terrainScale = EditorGUILayout.FloatField("Terrain Scale", terrainScale);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        terrainMaxHeight = EditorGUILayout.FloatField("Terrain Max Height", terrainMaxHeight);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        treeMaxHeight = EditorGUILayout.FloatField("Trees Max Height", treeMaxHeight);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("TerrainNoiseParameters");
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        seed = EditorGUILayout.IntField("Seed", seed);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        noiseScale = EditorGUILayout.FloatField("Noise Scale", noiseScale);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        octaves = EditorGUILayout.IntField("Octaves", octaves);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        persistance = EditorGUILayout.Slider("Persistance", persistance, 0, 1);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        lacunarity = EditorGUILayout.FloatField("Lacunarity", lacunarity);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        offset = EditorGUILayout.Vector2Field("Offset", offset);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        heightMultiplier = EditorGUILayout.FloatField("Height Multiplier", heightMultiplier);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        heightCurve =  EditorGUILayout.CurveField("Height Curve", heightCurve);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        minimumHeight = EditorGUILayout.FloatField("MinimumHeight", minimumHeight);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Trees Node");
        _treeNode = (GameObject)EditorGUILayout.ObjectField(_treeNode, typeof(GameObject), true);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Trees");
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Tree Density");
        _treeDensity = EditorGUILayout.FloatField(_treeDensity);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Tree prefab");
        _treeCount = EditorGUILayout.IntSlider("Size", _treeCount, 1, 20);
        EditorGUILayout.EndHorizontal();
        for (int i = 0; i < _treeCount; i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Tree " + i.ToString());
            _treeTypes[i] = (GameObject)EditorGUILayout.ObjectField(_treeTypes[i], typeof(GameObject), true);
            EditorGUILayout.EndHorizontal();
        }


        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Mountains");
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("High Montain prefab");
        _mountainprefab = (GameObject)EditorGUILayout.ObjectField(_mountainprefab, typeof(GameObject), true);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Mountains  Pop Rate");
        _mountainPopRate = EditorGUILayout.IntSlider("Mountains Pop Rate", _mountainPopRate, 0, 100);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Mountain Types Size");
        _mountainsCount = EditorGUILayout.IntSlider("Mountain Types Size", _mountainsCount, 0, 10);
        EditorGUILayout.EndHorizontal();

        for (int i = 0; i < _mountainsCount;i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Moutain " + i.ToString());
            _mountainTypes[i] = (GameObject)EditorGUILayout.ObjectField(_mountainTypes[i], typeof(GameObject), true);
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Trees Pop Rate");
        _popRate = EditorGUILayout.IntSlider("Trees Pop Rate", _popRate, 0, 10);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Terrain Width");
        _width = EditorGUILayout.IntSlider(_width, 0, 513);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Terrain Depth");
        _depth = EditorGUILayout.IntSlider(_depth, 0, 513);
        EditorGUILayout.EndHorizontal();
        

        /*if (GUILayout.Button("Place Objects"))
        {

            CreateTerrain();
               
        }*/

        if(GUILayout.Button("ComputeHeight"))
        {
            ComputeHeight();
        }

        if(GUILayout.Button("Place Trees"))
        {
            PlaceTrees();
        }

        if(GUILayout.Button("Erase Last Trees"))
        {
            EraseTrees();
        }
        /*
        if (GUILayout.Button("Suppress All"))
        {

            EraseScene();

        }

        if (GUILayout.Button("Create Other Plane"))
        {

            CreateMeshPlane();

        }
        */
        EditorGUILayout.EndScrollView();
        EditorGUILayout.EndVertical();

        
    }

    [MenuItem("MyTerrainEditor/Terrain Objects Editor")]
    public static void CreateMyTerrainOEditor()
    {
        var window = new TerrainObjectsEditor();
        window.Show();
    }

    void CreateTerrain()
    {
        instanciatedObjects.Clear();
        GameObject groundPlane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        groundPlane.transform.position = new Vector3(0, 0, 0);
        instanciatedObjects.Add(groundPlane.transform);
        groundPlane.transform.localScale *= 10;

        if(_mountainsCount == 0)
        {
            
        }
        else
        {
            for(int i = 0; i < 100; i++)
            {
                for(int j = 0;  j< 100; j++)
                {
                    if (Random.Range(0, 1000) <= _mountainPopRate)
                    {
                        GameObject pref = (GameObject)Instantiate(_mountainTypes[Random.Range(0, _mountainsCount)], new Vector3(i -45 , -0.1f, j -45 ), Quaternion.identity);
                        instanciatedObjects.Add(pref.transform);
                    }                   
                }
            }
        }

        if (_prefab != null)
        {
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    if (Random.Range(0,100) < _popRate)
                    {
                        GameObject pref = (GameObject)Instantiate(_prefab, new Vector3(i-50, 0, j-50), Quaternion.identity);
                        instanciatedObjects.Add(pref.transform);
                    }
                }       
            }
        }
    }

    void EraseScene()
    {
        if (instanciatedObjects[0] == null)
            return;
        for (int i = 0; i < instanciatedObjects.Count; i++)
        {
            if (Application.isEditor)
                GameObject.DestroyImmediate(instanciatedObjects[i].gameObject);
        }

        instanciatedObjects.Clear();
    }

    void EraseTrees()
    {
        if (instanciatedTrees[0] == null)
            return;
        for (int i = 0; i < instanciatedTrees.Count; i++)
        {
            if (Application.isEditor)
                GameObject.DestroyImmediate(instanciatedTrees[i].gameObject);
        }

        instanciatedTrees.Clear();
    }

    void CreateMeshPlane()
    {
        int sizeX = _width * _depth - _depth + 1;
        int sizeY = _depth;

        MeshFilter mf = new MeshFilter();

        var m = new Mesh();
        verts = new Vector3[_width*_depth];
        m.vertices = new Vector3[_width*_depth];
        var uvs = new Vector2[_width * _depth];

        int ind = 0;
        for(int i = 0;i< _width;i++)
        {
            for (int j = 0; j < _depth; j++)
            {
                verts[ind] = new Vector3(i, 0, j);
                uvs[ind] = (new Vector2(i,j));
                ind++;
            }
        }
        /*
        verts[0].Set(verts[0].x, Random.Range(0, 2), verts[0].z);
        verts[_depth - 1].Set(0,0,0);
        verts[verts.Length - _depth].Set(0,0,0);
        verts[verts.Length - 1].Set(0,0,0);
        */

        int spacing = _width + _depth;

        int spacingX = _width * _depth - _depth;

        int spacingY = _depth - 1;

        int nI = 0; int nJ = 0;

        while (spacingY > 1)
        {
            int halfSpacingX = spacingX / 2;
            int halfSpacingY = spacingY / 2;

            //Diamond Step
            while (nI < sizeX)
            {
                while (nJ < sizeY)
                {
                    verts[nI + nJ] = DiamondStep(nI + nJ, nI, nJ, halfSpacingX, halfSpacingY);
                    nJ += spacingY;
                }
                nI += spacingX;
                nJ = halfSpacingY;
            }

            //Square Step
            nI = 0;nJ = 0;
            int JStart = 0;

            while (nI < sizeX)
            {
                if ((nI / halfSpacingX) % 2 == 0)
                {
                    JStart = halfSpacingX;
                }
                else
                {
                    JStart = 0;
                }
                nJ = JStart;
                while (nJ < sizeY)
                {
                    verts[nI + nJ] = SquareStep(nI + nJ, nI, nJ, halfSpacingX, halfSpacingY);
                    nJ += spacingY;
                }
                nI += halfSpacingX;

            }

            spacingX = halfSpacingX;
            spacingY = halfSpacingY;

        }

        int[] indices = new int[(_width-1) * (_depth-1) * 2 * 3];
        int indexi = 0;

        for (int yPoint = 0; yPoint < _depth-1; yPoint++)
        {
            for(int xPoint = yPoint; xPoint < yPoint + _depth-1; xPoint+=5)
            {
                indices[indexi] = xPoint + yPoint;
                indices[indexi + 1] = xPoint + 1 + yPoint;
                indices[indexi + 2] = xPoint + 5 + yPoint;
                indexi += 3;
            }
        }

        m.SetIndices(indices , MeshTopology.Triangles, 0);

        //var newPlane = GameObject.CreatePrimitive(PrimitiveType.Plane);

        //m.uv = new Vector2[]{new Vector2 (0, 0), new Vector2 (0, 1), new Vector2(1, 1), new Vector2 (1, 0)};

        var newPlane = (GameObject)Instantiate(_mountainprefab);
        m.uv = uvs;
        m.RecalculateBounds();
        m.RecalculateNormals();
        newPlane.name = "TerrainPlane";

        newPlane.GetComponent<MeshFilter>().mesh = m;

        Debug.Log(m.vertices.Length);

    }


    Vector3 DiamondStep(int index, float nI, float nJ, float HalfX, float HalfY)
    {
        float sum = 0;
        int n = 0;

        if (nI >= HalfX && nJ >= HalfY)
        {
            var s = (nI-HalfX)+(nJ-HalfY);
            sum += verts[Mathf.RoundToInt(s)].y;
            n++;
        }

        if (nI >= HalfX && (nJ + HalfY) < _depth)
        {
            var s = (nI - HalfX) + (nJ + HalfY);
            sum += verts[Mathf.RoundToInt(s)].y;
            n++;
        }

        if((nI + HalfY) < _width && (nJ + HalfY +1 ) < _depth) 
        {
            var s = (nI + HalfX) + (nJ - HalfY);
            sum += verts[Mathf.RoundToInt(s)].y;
            n++;
        }

        if ((nI + HalfX) < _width && (nJ + HalfX + 1) < _depth)
        {
            var s = (nI + HalfX) + (nJ - HalfY);
            sum += verts[Mathf.RoundToInt(s)].y;
            n++;
        }

        return new Vector3(verts[index].x, Random.Range(0,100) * 0.01f * 2f * HalfY + sum/n, verts[index].z);
    }

    Vector3 SquareStep(int index, int nI, int nJ, float HalfX, float HalfY)
    {
        float sum = 0;
        int n = 0;

        if (nI >= HalfX)
        {
            var s = nI - HalfX + nJ;
            sum += verts[Mathf.RoundToInt(s)].y;
            n++;
        }

        if (nI + HalfX < _width)
        {
            var s = nI + HalfX + nJ;
            sum += verts[Mathf.RoundToInt(s)].y;
            n++;
        }

        if (nJ > HalfY)
        {
            var s = nI - HalfY + nJ;
            sum += verts[Mathf.RoundToInt(s)].y;
            n++;
        }

        if (nJ + HalfY < _depth)
        {
            var s = nI + HalfY + nJ;
            sum += verts[Mathf.RoundToInt(s)].y;
            n++;
        }

        return new Vector3(verts[index].x, Random.Range(0, 100) * 0.01f * 2f * HalfY + sum/n, verts[index].z);
    }

    public void ComputeHeight()
    {
        noiseMap = InNoise.GenerateNoiseMap(terrainSize, terrainSize, seed, noiseScale, octaves, persistance, lacunarity, offset,normalizeMode);

        bool useFalloff = true;
        /*
        for (int y = 0; y < terrainSize; y++)
        {
            for (int x = 0; x < terrainSize; x++)
            {
                if (useFalloff)
                {
                    float[,] fallOffMap = FalloffGenerator.GenerateFalloffMap(terrainSize);
                    
                    noiseMap[x, y] = noiseMap[x, y] - fallOffMap[x, y];

                    
                }

                //noiseMap[x, y] *= heightCurve.Evaluate(noiseMap[x, y]) * heightMultiplier;
                float currentHeight = noiseMap[x, y];
            }
        }
        */
    }

    public void PlaceTrees()
    {
        //ComputeHeight();
        
        if (_treeCount > 0)
        {
            for (int i = 0; i < terrainSize * terrainScale; i++)
            {
                for (int j = 0; j < terrainSize * terrainScale; j++)
                {
                    if (Random.Range(0, 1000) <= _treeDensity)
                    {
                        
                        RaycastHit hit;
                        Vector3 pos = new Vector3(j - terrainSize * terrainScale / 2, terrainMaxHeight, i - (terrainSize * terrainScale) / 2);
                        if (Physics.Raycast(pos, -Vector3.up * terrainMaxHeight*2, out hit))
                        {
                            if(pos.y - hit.distance > minimumHeight && pos.y - hit.distance < treeMaxHeight)
                            {
                                Debug.Log(hit.distance);
                                GameObject pref = (GameObject)GameObject.Instantiate(_treeTypes[Random.Range(0, _treeCount)], pos, Quaternion.identity);
                                pref.transform.position = new Vector3(pref.transform.position.x, pref.transform.position.y - hit.distance, pref.transform.position.z);

                                if (_treeNode != null)
                                    pref.transform.parent = _treeNode.transform;
                                instanciatedTrees.Add(pref.transform);
                            }
                           
                        }   
                        
                    }
                    
                }
            }
        }
        
    }


}
