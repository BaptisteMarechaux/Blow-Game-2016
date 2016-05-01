using UnityEngine;
using System . Collections;
using System . Collections . Generic;

public static class GenerateTreeProto
{
    public class Node
    {
        public Vector3 _node;
        public List<Vector3> _influencePoint;
        public Node _father;
        public bool _hasSons;

        public Node ( Vector3 origin )
        {
            _node = origin;
            _influencePoint = new List<Vector3> ( );
        }
    }

    public static float di = 8.0f;
    public static float dk = 1.0f;
    public static float D = 1.0f;

    public static List<Node> test ( )
    {
        List<Vector3> influences = new List<Vector3> ();
        Vector3 center = new Vector3 (0,5,0);
        while ( influences . Count < 50 )
        {
            Vector3 vec = new Vector3 (Random.Range ( -3, 3 ), Random.Range(2,8), Random.Range ( -3, 3 ));
            if ( Vector3 . Distance ( vec , center ) < 3 )
            {
                if ( !influences . Contains ( vec ) )
                {
                    influences . Add ( vec );
                }
            }
        }
        int u = 0;
        return Calculation ( new Vector3 ( 0 , 0 , 0 ) , influences );
    }

    public static List<Node> Calculation ( Vector3 origin , List<Vector3> influencesPoints )
    {
        List<Node> _nodes = new List<Node> ( );
        List<Node> _newNodes = new List<Node> ( );
        List<Vector3> _deadPoints = new List<Vector3> ( );
        Node first = new Node (origin);
        _nodes . Add ( first );

        int i = 0;

        while ( influencesPoints . Count > 0 )
        {
            Debug . LogFormat ( "Loop Branch step : {0}\nAttraction points : {1}\nNodes : {2} " , i , influencesPoints . Count , _nodes . Count );
            ++i;
            foreach ( Vector3 influencePoint in influencesPoints )
            {
                Node theChosen = null;
                float minDist = di;
                foreach ( Node node in _nodes )
                {
                    float tmp =  Vector3 . Distance ( node . _node , influencePoint );
                    if ( tmp < minDist )
                    {
                        minDist = tmp;
                        theChosen = node;
                    }
                }
                if ( theChosen != null )
                    theChosen . _influencePoint . Add ( influencePoint );
            }

            foreach ( Node node in _nodes )
            {
                if ( node . _influencePoint . Count > 0 )
                {
                    Vector3 n = new Vector3( 0, 0, 0 );
                    foreach ( Vector3 attraction in node . _influencePoint )
                    {
                        n += ( attraction - node . _node ) / ( attraction - node . _node ) . magnitude;
                    }
                    Node newNode =  new Node ( node . _node + D * n . normalized );
                    node . _hasSons = true;
                    newNode . _father = node;
                    _newNodes . Add ( newNode );
                    node . _influencePoint . Clear ( );
                }
            }

            foreach ( Vector3 influencePoint in influencesPoints )
            {
                foreach ( Node node in _nodes )
                {
                    if ( Vector3 . Distance ( node . _node , influencePoint ) < dk && !_deadPoints . Contains ( influencePoint ) )
                    {
                        _deadPoints . Add ( influencePoint );
                    }
                }
            }

            foreach ( Vector3 pt in _deadPoints )
            {
                influencesPoints . Remove ( pt );
            }

            _deadPoints . Clear ( );

            _nodes . AddRange ( _newNodes );
            _newNodes . Clear ( );
        }
        return _nodes;
    }
}
