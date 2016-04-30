using UnityEngine;
using System . Collections;

public class TreeGenerator : MonoBehaviour
{
    [SerializeField]
    private Tree [] _trees;

    public void Start ( )
    {
        float rad = 1f;
        for ( int i = 0 ; i < _trees . Length ; ++i, ++rad )
        {
            this . _trees [ i ] . Init ( new Vector3 ( Mathf . Cos ( i ) * rad , 0 , Mathf . Sin ( i ) * rad ) , .225f , 1f , float . MaxValue , 50 , new Vector3 ( 0 , 0 , 0 ) , 500 , true );
        }
    }

    public void Update ( )
    {

        //if ( Input . GetKeyDown ( KeyCode . Space ) )
        //{
            for ( int i = 0 ; i < _trees . Length ; ++i )
            {
                this . _trees [ i ] . StepByStep ( );
            }
        //}
    }
}
