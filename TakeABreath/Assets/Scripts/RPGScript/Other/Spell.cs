using UnityEngine;
using System.Collections;

public class Spell : MonoBehaviour {

    [SerializeField]
    protected string spellName;
    [SerializeField]
    protected string spellDescription;
    [SerializeField]
    protected int power;
    [SerializeField]
    protected float cooldown; //value in seconds

    [SerializeField]
    protected GameObject owner;
}
