using UnityEngine;
using System.Collections;

public class CameraFacingScript : MonoBehaviour {
    [SerializeField]
    Camera mainCamera;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.forward = mainCamera.transform.forward;
	}
}
