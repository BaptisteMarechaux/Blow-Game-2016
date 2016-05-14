using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum TutorialState
{
    introduction,
    explainations
}

public class TutorialManager : MonoBehaviour {
    [SerializeField]
    Text centralText;

    [SerializeField]
    Image blackBackgroundImage;

    //Liste de ce qui va être dit pendant la petite introduction
    [SerializeField]
    string[] introductionTextList;

    TutorialState state;

    public int readIndex = 0;

    [SerializeField]
    AudioSource audio;
    [SerializeField]
    AudioClip[] clips;

	// Use this for initialization
	void Start () {
        if(introductionTextList.Length>0)
        {
            centralText.text = "";
            StartCoroutine("DisplayText", introductionTextList[0]);
        }
            
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ReadNextText()
    {
        if(state == TutorialState.introduction)
        {
            if(readIndex +1 < introductionTextList.Length)
            {
                readIndex += 1;
                StopCoroutine("DisplayText");
                StartCoroutine("DisplayText", introductionTextList[readIndex]);    
            }
        }
    }

    IEnumerator DisplayText(string readString)
    {
        centralText.text = "";
        for (int i = 0; i < readString.Length; i++)
        {
            centralText.text += readString[i];
            audio.PlayOneShot(clips[Random.Range(0, clips.Length)]);
            yield return new WaitForSeconds(0.06f);
        }
    }
}
