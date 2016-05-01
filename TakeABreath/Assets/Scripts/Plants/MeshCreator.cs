using UnityEngine;
using System.Collections;

public class MeshCreator : MonoBehaviour
{

    // Use this for initialization
    void Start ( )
    {
        createSphere ( );
    }
	
    // Update is called once per frame
    void Update ( )
    {
	
    }


    void createTube ( )
    {
        MeshFilter filter = gameObject.AddComponent<MeshFilter> ( );
        Mesh mesh = filter.mesh;
        mesh.Clear ( );
 
        float height = 1f;
        int nbSides = 24;
 
// Outter shell is at radius1 + radius2 / 2, inner shell at radius1 - radius2 / 2
        float bottomRadius1 = .5f;
        float bottomRadius2 = .15f; 
        float topRadius1 = .5f;
        float topRadius2 = .15f;
 
        int nbVerticesCap = nbSides * 2 + 2;
        int nbVerticesSides = nbSides * 2 + 2;
#region Vertices
 
// bottom + top + sides
        Vector3 [] vertices = new Vector3[nbVerticesCap * 2 + nbVerticesSides * 2];
        int vert = 0;
        float _2pi = Mathf.PI * 2f;
 
// Bottom cap
        int sideCounter = 0;
        while ( vert < nbVerticesCap )
        {
            sideCounter = sideCounter == nbSides ? 0 : sideCounter;
 
            float r1 = ( float ) ( sideCounter++ ) / nbSides * _2pi;
            float cos = Mathf.Cos ( r1 );
            float sin = Mathf.Sin ( r1 );
            vertices [ vert ] = new Vector3 ( cos * ( bottomRadius1 - bottomRadius2 * .5f ), 0f, sin * ( bottomRadius1 - bottomRadius2 * .5f ) );
            vertices [ vert + 1 ] = new Vector3 ( cos * ( bottomRadius1 + bottomRadius2 * .5f ), 0f, sin * ( bottomRadius1 + bottomRadius2 * .5f ) );
            vert += 2;
        }
 
// Top cap
        sideCounter = 0;
        while ( vert < nbVerticesCap * 2 )
        {
            sideCounter = sideCounter == nbSides ? 0 : sideCounter;
 
            float r1 = ( float ) ( sideCounter++ ) / nbSides * _2pi;
            float cos = Mathf.Cos ( r1 );
            float sin = Mathf.Sin ( r1 );
            vertices [ vert ] = new Vector3 ( cos * ( topRadius1 - topRadius2 * .5f ), height, sin * ( topRadius1 - topRadius2 * .5f ) );
            vertices [ vert + 1 ] = new Vector3 ( cos * ( topRadius1 + topRadius2 * .5f ), height, sin * ( topRadius1 + topRadius2 * .5f ) );
            vert += 2;
        }
 
// Sides (out)
        sideCounter = 0;
        while ( vert < nbVerticesCap * 2 + nbVerticesSides )
        {
            sideCounter = sideCounter == nbSides ? 0 : sideCounter;
 
            float r1 = ( float ) ( sideCounter++ ) / nbSides * _2pi;
            float cos = Mathf.Cos ( r1 );
            float sin = Mathf.Sin ( r1 );
 
            vertices [ vert ] = new Vector3 ( cos * ( topRadius1 + topRadius2 * .5f ), height, sin * ( topRadius1 + topRadius2 * .5f ) );
            vertices [ vert + 1 ] = new Vector3 ( cos * ( bottomRadius1 + bottomRadius2 * .5f ), 0, sin * ( bottomRadius1 + bottomRadius2 * .5f ) );
            vert += 2;
        }
 
// Sides (in)
        sideCounter = 0;
        while ( vert < vertices.Length )
        {
            sideCounter = sideCounter == nbSides ? 0 : sideCounter;
 
            float r1 = ( float ) ( sideCounter++ ) / nbSides * _2pi;
            float cos = Mathf.Cos ( r1 );
            float sin = Mathf.Sin ( r1 );
 
            vertices [ vert ] = new Vector3 ( cos * ( topRadius1 - topRadius2 * .5f ), height, sin * ( topRadius1 - topRadius2 * .5f ) );
            vertices [ vert + 1 ] = new Vector3 ( cos * ( bottomRadius1 - bottomRadius2 * .5f ), 0, sin * ( bottomRadius1 - bottomRadius2 * .5f ) );
            vert += 2;
        }
#endregion
 
#region Normales
 
// bottom + top + sides
        Vector3 [] normales = new Vector3[vertices.Length];
        vert = 0;
 
// Bottom cap
        while ( vert < nbVerticesCap )
        {
            normales [ vert++ ] = Vector3.down;
        }
 
// Top cap
        while ( vert < nbVerticesCap * 2 )
        {
            normales [ vert++ ] = Vector3.up;
        }
 
// Sides (out)
        sideCounter = 0;
        while ( vert < nbVerticesCap * 2 + nbVerticesSides )
        {
            sideCounter = sideCounter == nbSides ? 0 : sideCounter;
 
            float r1 = ( float ) ( sideCounter++ ) / nbSides * _2pi;
 
            normales [ vert ] = new Vector3 ( Mathf.Cos ( r1 ), 0f, Mathf.Sin ( r1 ) );
            normales [ vert + 1 ] = normales [ vert ];
            vert += 2;
        }
 
// Sides (in)
        sideCounter = 0;
        while ( vert < vertices.Length )
        {
            sideCounter = sideCounter == nbSides ? 0 : sideCounter;
 
            float r1 = ( float ) ( sideCounter++ ) / nbSides * _2pi;
 
            normales [ vert ] = -( new Vector3 ( Mathf.Cos ( r1 ), 0f, Mathf.Sin ( r1 ) ) );
            normales [ vert + 1 ] = normales [ vert ];
            vert += 2;
        }
#endregion
 
#region UVs
        Vector2 [] uvs = new Vector2[vertices.Length];
 
        vert = 0;
// Bottom cap
        sideCounter = 0;
        while ( vert < nbVerticesCap )
        {
            float t = ( float ) ( sideCounter++ ) / nbSides;
            uvs [ vert++ ] = new Vector2 ( 0f, t );
            uvs [ vert++ ] = new Vector2 ( 1f, t );
        }
 
// Top cap
        sideCounter = 0;
        while ( vert < nbVerticesCap * 2 )
        {
            float t = ( float ) ( sideCounter++ ) / nbSides;
            uvs [ vert++ ] = new Vector2 ( 0f, t );
            uvs [ vert++ ] = new Vector2 ( 1f, t );
        }
 
// Sides (out)
        sideCounter = 0;
        while ( vert < nbVerticesCap * 2 + nbVerticesSides )
        {
            float t = ( float ) ( sideCounter++ ) / nbSides;
            uvs [ vert++ ] = new Vector2 ( t, 0f );
            uvs [ vert++ ] = new Vector2 ( t, 1f );
        }
 
// Sides (in)
        sideCounter = 0;
        while ( vert < vertices.Length )
        {
            float t = ( float ) ( sideCounter++ ) / nbSides;
            uvs [ vert++ ] = new Vector2 ( t, 0f );
            uvs [ vert++ ] = new Vector2 ( t, 1f );
        }
#endregion
 
#region Triangles
        int nbFace = nbSides * 4;
        int nbTriangles = nbFace * 2;
        int nbIndexes = nbTriangles * 3;
        int [] triangles = new int[nbIndexes];
 
// Bottom cap
        int i = 0;
        sideCounter = 0;
        while ( sideCounter < nbSides )
        {
            int current = sideCounter * 2;
            int next = sideCounter * 2 + 2;
 
            triangles [ i++ ] = next + 1;
            triangles [ i++ ] = next;
            triangles [ i++ ] = current;
 
            triangles [ i++ ] = current + 1;
            triangles [ i++ ] = next + 1;
            triangles [ i++ ] = current;
 
            sideCounter++;
        }
 
// Top cap
        while ( sideCounter < nbSides * 2 )
        {
            int current = sideCounter * 2 + 2;
            int next = sideCounter * 2 + 4;
 
            triangles [ i++ ] = current;
            triangles [ i++ ] = next;
            triangles [ i++ ] = next + 1;
 
            triangles [ i++ ] = current;
            triangles [ i++ ] = next + 1;
            triangles [ i++ ] = current + 1;
 
            sideCounter++;
        }
 
// Sides (out)
        while ( sideCounter < nbSides * 3 )
        {
            int current = sideCounter * 2 + 4;
            int next = sideCounter * 2 + 6;
 
            triangles [ i++ ] = current;
            triangles [ i++ ] = next;
            triangles [ i++ ] = next + 1;
 
            triangles [ i++ ] = current;
            triangles [ i++ ] = next + 1;
            triangles [ i++ ] = current + 1;
 
            sideCounter++;
        }
 
 
// Sides (in)
        while ( sideCounter < nbSides * 4 )
        {
            int current = sideCounter * 2 + 6;
            int next = sideCounter * 2 + 8;
 
            triangles [ i++ ] = next + 1;
            triangles [ i++ ] = next;
            triangles [ i++ ] = current;
 
            triangles [ i++ ] = current + 1;
            triangles [ i++ ] = next + 1;
            triangles [ i++ ] = current;
 
            sideCounter++;
        }
#endregion
 
        mesh.vertices = vertices;
        mesh.normals = normales;
        mesh.uv = uvs;
        mesh.triangles = triangles;
 
        mesh.RecalculateBounds ( );
        mesh.Optimize ( );
    }


    void createSphere ( )
    {
        MeshFilter filter = gameObject.AddComponent< MeshFilter > ( );
        Mesh mesh = filter.mesh;
        mesh.Clear ( );
 
        float radius = 1f;
// Longitude |||
        int nbLong = 24;
// Latitude ---
        int nbLat = 16;
 
#region Vertices
        Vector3 [] vertices = new Vector3[( nbLong + 1 ) * nbLat + 2];
        float _pi = Mathf.PI;
        float _2pi = _pi * 2f;
 
        vertices [ 0 ] = Vector3.up * radius;
        for ( int lat = 0 ; lat < nbLat ; lat++ )
        {
            float a1 = _pi * ( float ) ( lat + 1 ) / ( nbLat + 1 );
            float sin1 = Mathf.Sin ( a1 );
            float cos1 = Mathf.Cos ( a1 );
 
            for ( int lon = 0 ; lon <= nbLong ; lon++ )
            {
                float a2 = _2pi * ( float ) ( lon == nbLong ? 0 : lon ) / nbLong;
                float sin2 = Mathf.Sin ( a2 );
                float cos2 = Mathf.Cos ( a2 );
 
                vertices [ lon + lat * ( nbLong + 1 ) + 1 ] = new Vector3 ( sin1 * cos2, cos1, sin1 * sin2 ) * radius;
            }
        }
        vertices [ vertices.Length - 1 ] = Vector3.up * -radius;
#endregion
 
#region Normales        
        Vector3 [] normales = new Vector3[vertices.Length];
        for ( int n = 0 ; n < vertices.Length ; n++ )
            normales [ n ] = vertices [ n ].normalized;
#endregion
 
#region UVs
        Vector2 [] uvs = new Vector2[vertices.Length];
        uvs [ 0 ] = Vector2.up;
        uvs [ uvs.Length - 1 ] = Vector2.zero;
        for ( int lat = 0 ; lat < nbLat ; lat++ )
            for ( int lon = 0 ; lon <= nbLong ; lon++ )
                uvs [ lon + lat * ( nbLong + 1 ) + 1 ] = new Vector2 ( ( float ) lon / nbLong, 1f - ( float ) ( lat + 1 ) / ( nbLat + 1 ) );
#endregion
 
#region Triangles
        int nbFaces = vertices.Length;
        int nbTriangles = nbFaces * 2;
        int nbIndexes = nbTriangles * 3;
        int [] triangles = new int[ nbIndexes ];
 
//Top Cap
        int i = 0;
        for ( int lon = 0 ; lon < nbLong ; lon++ )
        {
            triangles [ i++ ] = lon + 2;
            triangles [ i++ ] = lon + 1;
            triangles [ i++ ] = 0;
        }
 
//Middle
        for ( int lat = 0 ; lat < nbLat - 1 ; lat++ )
        {
            for ( int lon = 0 ; lon < nbLong ; lon++ )
            {
                int current = lon + lat * ( nbLong + 1 ) + 1;
                int next = current + nbLong + 1;
 
                triangles [ i++ ] = current;
                triangles [ i++ ] = current + 1;
                triangles [ i++ ] = next + 1;
 
                triangles [ i++ ] = current;
                triangles [ i++ ] = next + 1;
                triangles [ i++ ] = next;
            }
        }
 
//Bottom Cap
        for ( int lon = 0 ; lon < nbLong ; lon++ )
        {
            triangles [ i++ ] = vertices.Length - 1;
            triangles [ i++ ] = vertices.Length - ( lon + 2 ) - 1;
            triangles [ i++ ] = vertices.Length - ( lon + 1 ) - 1;
        }
#endregion
 
        mesh.vertices = vertices;
        mesh.normals = normales;
        mesh.uv = uvs;
        mesh.triangles = triangles;
 
        mesh.RecalculateBounds ( );
        mesh.Optimize ( );
    }
}
