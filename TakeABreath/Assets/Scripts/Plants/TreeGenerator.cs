using UnityEngine;
using System . Collections;
using System . Collections . Generic;

public class TreeGenerator : MonoBehaviour
{
    [Header("Arbres et taux d'appartion")]
    [SerializeField]
    protected McTree [] _trees;
    [SerializeField]
    protected float [] _percentTrees;
    [Header("Paramètres d'initialisation")]
    [Tooltip("Distance entre les noeuds")]
    [SerializeField]
    protected float _distance = 0.225f;
    [Tooltip("Distance d'influence des targets")]
    [SerializeField]
    protected float _distanceInfluence = float.MaxValue;
    [Tooltip("Distance de kill des targets")]
    [SerializeField]
    protected float _distanceKill = 0.5f;
    [Tooltip("Nombre max d'itération de l'algo")]
    [SerializeField]
    protected int _maxIteration = 250;
    [Tooltip("Nombre de targets minimum")]
    [SerializeField]
    protected int _minNbrTargets = 50;
    [Tooltip("Nombre de targets maximum")]
    [SerializeField]
    protected int _maxNbrTargets = 200;

    protected List<McTree> _instanciatesTrees = new List<McTree> ( );
    private List <Vector3> _pos;
    private List <Quaternion> _rot;

    /// <summary>
    /// 
    /// </summary>
    public void Update ( )
    {

        //if ( Input . GetKeyDown ( KeyCode . Space ) )
        //{
        for ( int i = 0 ; i < this . _instanciatesTrees . Count ; ++i )
        {
            this . _instanciatesTrees [ i ] . StepByStep ( );
        }
        //}
    }

    /// <summary>
    /// Initialise le générateur d'arbres.
    /// </summary>
    /// <param name="pos">Positions des arbres</param>
    /// <param name="rot">Rotations des arbres</param>
    public void Init ( List<Vector3> pos , List<Quaternion> rot )
    {
        this . _pos = pos;
        this . _rot = rot;
        if ( this . _trees . Length >= this . _percentTrees . Length )
        {
            CreateTrees ( );

            InitialiseTrees ( );
        }
    }

    /// <summary>
    /// 
    /// </summary>
    protected virtual void CreateTrees ( )
    {
        float proba = 0.0f;
        float percent;
        for ( int i = 0 ; i < this . _pos . Count ; ++i )
        {
            proba = Random . Range ( 0.0f , 1.0f );
            percent = 0f;
            for ( int j = 0 ; j < this . _percentTrees . Length ; ++j )
            {
                if ( proba >= percent && proba < percent + this . _percentTrees [ j ] )
                {
                    this . _instanciatesTrees . Add ( CreateTree ( this . _trees [ j ] , i , percent ) );
                    //Debug . Log ( "Tree ++ " + this . _trees [ j ] . name );
                }
                percent += this . _percentTrees [ j ];
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tree"></param>
    /// <param name="id"></param>
    /// <param name="percent"></param>
    /// <returns></returns>
    protected McTree CreateTree ( McTree tree , int id , float percent )
    {
        McTree t = ( ( GameObject ) Instantiate ( tree.gameObject, new Vector3 ( 0 , 0 , 0 ) , Quaternion.identity ) ).GetComponent<McTree> ( );

        t . gameObject . name = t . gameObject . name + "_" + id + "_" + percent;

        return t;
    }
    /// <summary>
    /// 
    /// </summary>
    protected virtual void InitialiseTrees ( )
    {
        for ( int i = 0 ; i < this . _instanciatesTrees . Count ; ++i )
        {
            this . _instanciatesTrees [ i ] . Init ( this . _pos [ i ] , this . _rot [ i ] , this . _distance , this . _distanceKill , this . _distanceInfluence , Random . Range ( this . _minNbrTargets , this . _maxNbrTargets ) , new Vector3 ( 0 , 0 , 0 ) , this . _maxIteration , true );
        }
    }
}
