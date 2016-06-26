using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SimpleDialogManager : MonoBehaviour {
    public Text centralText;

    [SerializeField]
    AudioSource audio;
    [SerializeField]
    AudioClip clip;

    public string[] textList;

    public int readIndex = 0;

    // Use this for initialization
    void Start () {
        centralText.text = "";
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void StartDialogue()
    {
        StartCoroutine("DisplayText", textList[readIndex]);
    }

    public int ReadNextText()
    {
        if (readIndex + 1 < textList.Length)
        {
            readIndex += 1;
            StopCoroutine("DisplayText");
            StartCoroutine("DisplayText", textList[readIndex]);
        }
        else
        {
            readIndex += 1;
        }

        return readIndex;
    }

    IEnumerator DisplayText(string readString)
    {
        centralText.text = "";
        for (int i = 0; i < readString.Length; i++)
        {
            centralText.text += readString[i];
            audio.PlayOneShot(clip);
            yield return new WaitForSeconds(0.03f);
        }
    }
    [System.Serializable]
    public struct SimpleDialog
    {
        public string[] text;
    }
}
