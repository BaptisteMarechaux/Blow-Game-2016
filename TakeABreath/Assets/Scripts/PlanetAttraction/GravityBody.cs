using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class GravityBody : MonoBehaviour {

    [SerializeField]
    GravityAttractor planet;
    Rigidbody rigidBody;

	void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.useGravity = false;
        rigidBody.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void FixedUpdate()
    {
        planet.Attract(rigidBody);
    }

    public void setGravityAttractor()
    {

    }
}
