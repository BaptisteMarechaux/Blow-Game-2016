using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    public GameManager instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void CameraFocusOnTransform(Transform cam, Transform target)
    {
        cam.LookAt(target);
        
    }
}
