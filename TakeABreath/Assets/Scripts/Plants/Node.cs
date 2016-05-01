using UnityEngine;
using System . Collections;
using System . Collections . Generic;

public class Node : MonoBehaviour
{
    private Vector3 _origin;
    private Node _father;
    private List<Node> _sons;
    private List<Target> _targets;
    private float _radius = 0;


    /// <summary>
    /// Gets the origin.
    /// </summary>
    /// <value>The origin.</value>
    public Vector3 Origin
    {
        get
        {
            return _origin;
        }
    }
    /// <summary>
    /// The node's father.
    /// </summary>
    public Node Father
    {
        get
        {
            return _father;
        }

        set
        {
            this . _father = value;
        }
    }
    /// <summary>
    /// Gets a value indicating whether this instance has sons.
    /// </summary>
    /// <value><c>true</c> if this instance has sons; otherwise, <c>false</c>.</value>
    public bool HasSons
    {
        get
        {
            return this . _sons . Count > 0 ? true : false;
        }
    }


    /// <summary>
    /// Gets a value indicating whether this instance has father.
    /// </summary>
    /// <value><c>true</c> if this instance has father; otherwise, <c>false</c>.</value>
    public bool HasFather
    {
        get
        {
            return this . _father != null;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public bool HasTargets
    {
        get
        {
            return this . _targets . Count > 0 ? true : false;
        }
    }


    /// <summary>
    /// Adds the target.
    /// </summary>
    /// <param name="t">T.</param>
    public void AddTarget ( Target t )
    {
        _targets . Add ( t );
    }

    /// <summary>
    /// Add a son.
    /// </summary>
    /// <param name="node"></param>
    public void AddSon ( Node node )
    {
        this . _sons . Add ( node );
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Node"/> class.
    /// </summary>
    /// <param name="origin">Origin.</param>
    public Node ( Vector3 origin )
    {
        this . _origin = origin;
    }
    /// <summary>
    /// 
    /// </summary>
    public void Start ( )
    {
        _sons = new List<Node> ( 10 );
        _targets = new List<Target> ( 100 );
        this . _origin = this . transform . localPosition;
    }

    /// <summary>
    /// Calculates the son.
    /// </summary>
    /// <returns>The son.</returns>
    /// <param name="distance">Distance.</param>
    public Vector3 CalculateSon ( float distance )
    {
        if ( this . _targets . Count == 0 )
            return Vector3 . zero;

        Vector3 n = new Vector3 ( 0, 0, 0 );

        for ( int i = 0 ; i < this . _targets . Count ; ++i )
        {
            n += ( this . _targets [ i ] . Origin - this . _origin ) / ( this . _targets [ i ] . Origin - this . _origin ) . magnitude;
        }

        this . _targets . Clear ( );

        n = this . _origin + distance * n . normalized;

        return n;
    }


}
