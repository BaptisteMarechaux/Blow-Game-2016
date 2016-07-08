using UnityEngine;
using System.Collections;

public class Spell : MonoBehaviour {

    [SerializeField]
    protected string spellName;
    [SerializeField]
    protected string spellDescription;
    [SerializeField]
    public int power;
    [SerializeField]
    protected float cooldown; //value in seconds

    [SerializeField]
    public GameObject owner;

    public virtual void LaunchSpell()
    {
        
    }

    public virtual void Initialize(GameObject _owner)
    {

    }
}
