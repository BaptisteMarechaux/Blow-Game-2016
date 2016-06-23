using UnityEngine;
using System.Collections;

public class UnscaledTimeParticle : MonoBehaviour {
    public bool simulateOnlyOnPause;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.timeScale < 0.01f)
        {
            GetComponent<ParticleSystem>().Simulate(Time.unscaledDeltaTime, true, false);
            //particleSystem.Simulate(Time.unscaledDeltaTime, true, false);
        }
    }
}
