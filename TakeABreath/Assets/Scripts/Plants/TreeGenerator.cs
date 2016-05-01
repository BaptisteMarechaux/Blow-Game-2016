using UnityEngine;
using System . Collections;
using System . Collections . Generic;

public class TreeGenerator : MonoBehaviour
{
    [Header("Arbres et taux d'appartion")]
    [SerializeField]
    private McTree [] _trees;
    [SerializeField]
    private float [] _percentTrees;
    [SerializeField]
    int _nbrTrees = 50;
    [Header("Paramètres d'initialisation")]
    [Tooltip("Distance entre les noeuds")]
    [SerializeField]
    private float _distance = 0.225f;
    [Tooltip("Distance d'influence des targets")]
    [SerializeField]
    private float _distanceInfluence = float.MaxValue;
    [Tooltip("Distance de kill des targets")]
    [SerializeField]
    private float _distanceKill = 0.5f;
    [Tooltip("Nombre max d'itération de l'algo")]
    [SerializeField]
    private int _maxIteration = 250;
    [Tooltip("Nombre de targets minimum")]
    [SerializeField]
    private int _minNbrTargets = 50;
    [Tooltip("Nombre de targets maximum")]
    [SerializeField]
    private int _maxNbrTargets = 200;

    private List<McTree> _instanciatesTrees = new List<McTree> ( );
    /// <summary>
    /// 
    /// </summary>
    public void Start ( )
    {
        if ( this . _trees . Length >= this . _percentTrees . Length )
        {
            CreateTrees ( );

            InitialiseTrees ( );
        }
    }

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
    /// 
    /// </summary>
    private void CreateTrees ( )
    {
        float proba = 0.0f;
        float percent;
        for ( int i = 0 ; i < this . _nbrTrees ; ++i )
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
    private McTree CreateTree ( McTree tree , int id , float percent )
    {
        McTree t = (( GameObject ) Instantiate ( tree.gameObject, new Vector3 (0,0,0), Quaternion.identity )).GetComponent<McTree>();

        t . gameObject . name = t . gameObject . name + "_" + id + "_" + percent;

        return t;
    }
    /// <summary>
    /// 
    /// </summary>
    private void InitialiseTrees ( )
    {
        float rad = 2f;
        for ( int i = 0 ; i < this . _instanciatesTrees . Count ; ++i, ++rad )
        {
            this . _instanciatesTrees [ i ] . Init ( new Vector3 ( Mathf . Cos ( i ) * rad , 0 , Mathf . Sin ( i ) * rad ) , this . _distance , this . _distanceKill , this . _distanceInfluence , Random . Range ( this . _minNbrTargets , this . _maxNbrTargets ) , new Vector3 ( 0 , 0 , 0 ) , this . _maxIteration , true );
        }
    }
}
