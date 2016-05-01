using UnityEngine;
using System . Collections;
using System . Collections . Generic;

public class Proto : MonoBehaviour
{

    [SerializeField]
    GameObject attractionPoint;
    [SerializeField]
    GameObject nodePoint;

    // Use this for initialization
    void Start ( )
    {
        List<Vector3> influences = new List<Vector3> ();
        Vector3 center = new Vector3 ( 0 , 5 , 0 );
        while ( influences . Count < 20 )
        {
            Vector3 vec = new Vector3 (Random.Range ( -3, 3 ), Random.Range(2,8), Random.Range ( -3, 3 ));
            if ( Vector3 . Distance ( vec , center ) < 3 )
            {
                if ( !influences . Contains ( vec ) )
                {
                    influences . Add ( vec );
                    Instantiate ( attractionPoint , vec , Quaternion . identity );
                }
            }
        }
        
        List<GenerateTreeProto.Node> nodes =  GenerateTreeProto.Calculation ( new Vector3(0,0,0), influences );
        foreach ( GenerateTreeProto . Node node in nodes )
        {
            Instantiate ( nodePoint , node . _node , Quaternion . identity );
        }
    }

    // Update is called once per frame
    void Update ( )
    {

    }
}
