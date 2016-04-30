using UnityEngine;
using System . Collections;

public class Target : MonoBehaviour
{
    [SerializeField]
    private Renderer _renderer;
    private Vector3 _origin;
    private bool _alive = true;


    /// <summary>
    /// Gets a value indicating whether this <see cref="Target"/> is alive.
    /// </summary>
    /// <value><c>true</c> if alive; otherwise, <c>false</c>.</value>
    public bool Alive
    {
        get
        {
            return _alive;
        }
    }


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
    /// Start.
    /// </summary>
    public void Start ( )
    {
        this . _alive = true;
        this . _origin = this . transform . localPosition;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="origin"></param>
    public Target ( Vector3 origin )
    {
        this . _origin = origin;
    }


    /// <summary>
    /// Kill this instance.
    /// </summary>
    public void Kill ( )
    {
        _alive = false;
        if ( this . _renderer != null )
            this . _renderer . material . color = new Color ( 0f , 1f , 0f , 0f );
    }
}
