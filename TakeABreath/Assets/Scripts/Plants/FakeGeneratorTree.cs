using UnityEngine;
using System . Collections;
using System . Collections . Generic;

public class FakeGeneratorTree : TreeGenerator
{
    [SerializeField]
    int _nbrTrees = 50;

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
    protected override void CreateTrees ( )
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
    protected override void InitialiseTrees ( )
    {
        float rad = 2f;
        for ( int i = 0 ; i < this . _instanciatesTrees . Count ; ++i, ++rad )
        {
            this . _instanciatesTrees [ i ] . Init ( new Vector3 ( Mathf . Cos ( i ) * rad , 0 , Mathf . Sin ( i ) * rad ) , Quaternion . identity , this . _distance , this . _distanceKill , this . _distanceInfluence , Random . Range ( this . _minNbrTargets , this . _maxNbrTargets ) , new Vector3 ( 0 , 0 , 0 ) , this . _maxIteration , true );
        }
    }
}
