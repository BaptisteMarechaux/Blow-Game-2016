using UnityEngine;
using System . Collections;
using System . Collections . Generic;

public class McTree : MonoBehaviour
{
    [SerializeField]
    private TargetGenerator _targetGen;
    [SerializeField]
    private Transform _nodeFather;
    [SerializeField]
    private GameObject _node;

    private List<Node> _nodes;
    private List<Target> _targets;
    private Vector3 _g;
    private int _maxGeneration;
    private int _nbrTargetsAlives;
    private int _generation = 0;
    private int _lastNbrNodes = -1;
    private float _distance;
    private float _distanceKill;
    private float _distanceInfluence;

    /// <summary>
    /// Create a tree.
    /// </summary>
    /// <param name="position"></param>
    /// <param name="Origins"></param>
    /// <param name="distance"></param>
    /// <param name="distanceKill"></param>
    /// <param name="distanceInfluence"></param>
    /// <param name="nbrTargets"></param>
    /// <param name="G"></param>
    /// <param name="maxGeneration"></param>
    public McTree ( Vector3 position , float distance , float distanceKill , float distanceInfluence , int nbrTargets , Vector3 G , int maxGeneration , bool displayTargets = false )
    {
        this . Init ( position , distance , distanceKill , distanceInfluence , nbrTargets , G , maxGeneration , displayTargets );
    }

    /// <summary>
    /// Init a tree.
    /// </summary>
    /// <param name="position"></param>
    /// <param name="Origins"></param>
    /// <param name="distance"></param>
    /// <param name="distanceKill"></param>
    /// <param name="distanceInfluence"></param>
    /// <param name="nbrTargets"></param>
    /// <param name="G"></param>
    /// <param name="maxGeneration"></param>
    public void Init ( Vector3 position , float distance , float distanceKill , float distanceInfluence , int nbrTargets , Vector3 G , int maxGeneration , bool displayTargets = false )
    {
        this . _nodes = new List<Node> ( );
        this . _targets = new List<Target> ( );
        this . transform . localPosition = position;
        this . _nodes . Add ( CreateNode ( new Vector3 ( 0 , 0 , 0 ) ) );
        this . _targets . AddRange ( this . _targetGen . GenerateTargets ( nbrTargets , displayTargets ) );
        this . _g = G;
        this . _maxGeneration = maxGeneration;
        this . _nbrTargetsAlives = nbrTargets;
        this . _distance = distance;
        this . _distanceInfluence = distanceInfluence;
        this . _distanceKill = distanceKill;
    }

    /// <summary>
    /// Launch the calculation of the tree.
    /// </summary>
    public void LaunchCalculation ( )
    {
        while ( this . _nbrTargetsAlives > 0 && this . _lastNbrNodes != this . _nodes . Count && this . _generation < _maxGeneration )
        {
            Loop ( );
        }
    }

    /// <summary>
    /// Step by step calculation.
    /// </summary>
    public void StepByStep ( )
    {
        if ( this . _nbrTargetsAlives > 0 && this . _lastNbrNodes != this . _nodes . Count && this . _generation < _maxGeneration )
        {
            Loop ( );
        }
    }

    /// <summary>
    /// Main loop.
    /// </summary>
    private void Loop ( )
    {
        //Debug . LogFormat ( "Time : {0} ; Step : {1}" , Time . realtimeSinceStartup , this . _generation );

        AssignTargets ( );

        CreateNodes ( );

        KillTargets ( );

        ++this . _generation;
    }

    /// <summary>
    /// Assign the targets to the nodes.
    /// </summary>
    private void AssignTargets ( )
    {
        for ( int first = 0 ; first < this . _targets . Count ; ++first )
        {
            if ( this . _targets [ first ] . Alive )
            {
                Node theChosen = null;
                float minDist = this._distanceInfluence;
                for ( int second = 0 ; second < this . _nodes . Count ; ++second )
                {
                    float tmp = Vector3.Distance ( this._nodes [ second ].Origin, this._targets [ first ].Origin );
                    if ( tmp < minDist )
                    {
                        minDist = tmp;
                        theChosen = _nodes [ second ];
                    }
                }
                if ( theChosen != null )
                    theChosen . AddTarget ( this . _targets [ first ] );
            }
        }
        //Debug . LogFormat ( "Time : {0} ; Assign targets" , Time . realtimeSinceStartup );
    }

    /// <summary>
    /// Creates the new nodes.
    /// </summary>
    private void CreateNodes ( )
    {
        int nbrNode = this._nodes.Count;
        for ( int first = 0 ; first < nbrNode ; ++first )
        {
            if ( this . _nodes [ first ] . HasTargets )
            {
                Vector3 vec = this._nodes [ first ].CalculateSon ( this._distance );
                if ( vec != Vector3 . zero )
                {
                    Node n = CreateNode ( vec );
                    this . _nodes [ first ] . AddSon ( n );
                    n . Father = this . _nodes [ first ];
                    this . _nodes . Add ( n );
                }
            }
        }
        //Debug . LogFormat ( "Time : {0} ; Create nodes" , Time . realtimeSinceStartup );
    }

    /// <summary>
    /// Kill the targets.
    /// </summary>
    private void KillTargets ( )
    {
        for ( int first = 0 ; first < this . _targets . Count ; ++first )
        {
            if ( this . _targets [ first ] . Alive )
            {
                for ( int second = 0 ; second < _nodes . Count ; ++second )
                {
                    if ( Vector3 . Distance ( this . _nodes [ second ] . Origin , this . _targets [ first ] . Origin ) < this . _distanceKill )
                    {
                        this . _targets [ first ] . Kill ( );
                        --this . _nbrTargetsAlives;
                    }
                }
            }
        }
        //Debug . LogFormat ( "Time : {0} ; Kill targets" , Time . realtimeSinceStartup );
    }

    /// <summary>
    /// Create a node.
    /// </summary>
    /// <param name="origin"></param>
    private Node CreateNode ( Vector3 origin )
    {
        GameObject go = ( GameObject ) Instantiate ( this._node, origin, Quaternion.identity );
        go . transform . SetParent ( this . _nodeFather , false );
        Node node = go.GetComponent<Node> ();

        go . name = go . name + "_" + this . _generation + "_" + go . GetHashCode ( );

        return node;
    }
}
