using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class ServerInit : NetworkBehaviour {

	// Use this for initialization
	void Start () {
        if (isServer)
            GameObject.Find("Camera").SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
