using UnityEngine;
using System . Collections;
using System . Collections . Generic;

public class TargetGenerator : MonoBehaviour
{
    [SerializeField]
    private MeshFilter _shape;
    [SerializeField]
    private GameObject _target;
    [SerializeField]
    private Transform _targetMother;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="nbrTargets"></param>
    /// <param name="displayTargets"></param>
    /// <returns></returns>
    public List<Target> GenerateTargets ( int nbrTargets , bool displayTargets = false )
    {
        List<Target> influences = new List<Target> ( );
        List<Vector3> vectors = new List<Vector3> ( );
        Vector3 center = new Vector3 ( 0, 5, 0 );
        influences . Add ( CreateTarget ( center , displayTargets ) );
        while ( influences . Count < nbrTargets )
        {
            Vector3 vec = new Vector3 ( Random.Range ( -2.5f, 2.5f ), Random.Range ( 1f, 5f ), Random.Range ( -2.5f, 2.5f ) );
            if ( !vectors . Contains ( vec ) )
            {
                vectors . Add ( vec );
                influences . Add ( CreateTarget ( vec , displayTargets ) );
            }
        }

        return influences;
    }

    /// <summary>
    /// Create a target.
    /// </summary>
    /// <param name="origin"></param>
    private Target CreateTarget ( Vector3 origin , bool displayTarget = false )
    {
        Target t;
        if ( displayTarget )
        {
            GameObject go = ( GameObject ) Instantiate ( this._target, origin, Quaternion.identity );
            go . transform . SetParent ( this . _targetMother , false );
            t = go . GetComponent<Target> ( );

            go . name = go . name + "_" + go . GetHashCode ( );
        }
        else
        {
            t = new Target ( origin );
        }
        return t;
    }
}
