using UnityEngine;
using System . Collections;
using System . Collections . Generic;

public class FakeGenerateTarget : TargetGenerator
{
    [SerializeField]
    private List<Target> _targets;


    public override List<Target> GenerateTargets ( int nbrTargets , bool displayTargets = false )
    {
        return this . _targets;
    }
}
