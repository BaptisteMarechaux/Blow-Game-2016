using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class UIFadeOut : MonoBehaviour {
    [SerializeField]
    Graphic graphic;
    [SerializeField]
    float preWaitDuration = 2.0f;
    [SerializeField]
    float fadeDuration = 1.0f;
	// Use this for initialization
	void Start () {
        StartCoroutine("wait");
        
	}

    void OnEnable()
    {
        StartCoroutine("wait");
    }

    void StartFade()
    {
        StopAllCoroutines();
        graphic.CrossFadeAlpha(0, fadeDuration, false);
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(preWaitDuration);
        StartFade();
    }
}

