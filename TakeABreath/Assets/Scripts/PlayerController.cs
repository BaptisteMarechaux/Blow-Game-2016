using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    [SerializeField]
    Camera playerCamera;
    [SerializeField]
    Transform playerTransform;

    [SerializeField]
    float speed = 10;

    float v, h;
    Vector3 translateVector=new Vector3();

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        translateVector.x = h*speed;
        translateVector.y = 0;
        translateVector.z = v*speed;
        playerTransform.Translate(translateVector*Time.deltaTime);
    }
}
