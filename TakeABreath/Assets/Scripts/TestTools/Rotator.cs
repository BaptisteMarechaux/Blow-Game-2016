using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour {

    [SerializeField]
    int rotationFactor=1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Rotate(Vector3.forward, Time.deltaTime * 1f * rotationFactor);
	}
}
