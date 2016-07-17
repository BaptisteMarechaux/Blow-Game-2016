using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ForestLevelStartEvent : MonoBehaviour {
    public Transform startCamera;

    public GameObject gameplayGroup;

    public Text zoneText;

    public Image blackImage;


    float time = 0;
	// Use this for initialization
	void Start () {
        zoneText.CrossFadeAlpha(0, 0, true);
        StartCoroutine("StartEventProcess");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator StartEventProcess()
    {
        zoneText.CrossFadeAlpha(1, 2, true);
        blackImage.CrossFadeAlpha(0, 2, true);

        while(time < 2)
        {
            startCamera.transform.Translate(Vector3.left * 0.02f * 10);
            time += 0.02f;
            Debug.Log(time);
            yield return new WaitForSeconds(0.02f);
        }
        zoneText.CrossFadeAlpha(0, 1, true);
        yield return new WaitForSeconds(1);
        blackImage.CrossFadeAlpha(1, 1, true);
        yield return new WaitForSeconds(1);
        gameplayGroup.SetActive(true);
        blackImage.CrossFadeAlpha(0, 1, true);
        startCamera.gameObject.SetActive(false);
        yield return null;
    }
}
