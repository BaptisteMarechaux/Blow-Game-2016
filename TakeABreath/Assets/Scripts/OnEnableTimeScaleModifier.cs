using UnityEngine;
using System.Collections;

public class OnEnableTimeScaleModifier : MonoBehaviour {
    float initialTimeScale;
    void Start()
    {
        
    }
	void OnEnable()
    {
        initialTimeScale = Time.timeScale;
        Time.timeScale = 0;
    }

    void OnDisable()
    {
        Time.timeScale = initialTimeScale;
    }
}
