using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class introCameraBehaviour : MonoBehaviour {
    [SerializeField]
    Text mainText;

    [SerializeField]
    Transform mainCamera;

    [SerializeField]
    Transform[] transitionCameras;

    public Image blackImage;
    public Image soulImage;
    public Image judgmentimage;
    public Image destinyImage;
    public Image worldImage;
    public Image titleImage;
    public Text pressButtonText;

    public bool canPressButton;

    float time = 0;
	// Use this for initialization
	void Start () {
        soulImage.CrossFadeAlpha(0, 0, true);
        titleImage.CrossFadeAlpha(0, 0, true);
        pressButtonText.CrossFadeAlpha(0, 0, true);
        StartCoroutine("IntroProcess");
	}
	
	// Update is called once per frame
	void Update () {
	    if(canPressButton)
        {
            if(Input.anyKeyDown)
            {
                SceneManager.LoadScene(2);
            }
        }
	}

    IEnumerator IntroProcess()
    {
        blackImage.CrossFadeAlpha(0, 2.0f, true);
        mainText.text = "Dans un monde lointain";
        while(time < 720)
        {
            time += 1;
            if(time == 180)
            {
                mainText.text = "Les esprits sont les rois de la nature";
            }

            if(time == 360)
            {
                mainText.text = "Ils suivent à la lettre les ordres de leur dvinité";
            }

            if(time == 540)
            {
                mainText.text = "Cependant, ils ne sont pas tous fidèles";
            }

            if(time == 720)
            {
                mainText.text = "Tout cela a commencé avec vous...";
            }
            
            mainCamera.position = Vector3.Lerp(mainCamera.position, transitionCameras[0].position, 0.3f * 0.0167f);
            yield return new WaitForFixedUpdate();
        }
        time = 0;
        blackImage.CrossFadeAlpha(1, 1, true);
        yield return new WaitForSeconds(4.0f);
        mainText.text = "";
        soulImage.CrossFadeAlpha(1.0f, 1.0f, true);
        yield return new WaitForSeconds(2.0f);
        blackImage.CrossFadeAlpha(0, 1.0f, true);
        mainText.text = "Vous possédez une capacité exceptionnelle : ";

        yield return new WaitForSeconds(4.0f);
        mainText.text = "La possession spirituelle";

        yield return new WaitForSeconds(4.0f);
        mainText.text = "Grâce à ce pouvoir, vous pouvez donc contrôler les êtres vivants";

        yield return new WaitForSeconds(4.0f);
        mainText.text = "Vous avez utilisé votre pouvoir pour prendre possession des dieux";

        yield return new WaitForSeconds(4.0f);
        blackImage.CrossFadeAlpha(1, 1, true);
        yield return new WaitForSeconds(1.0f);
        mainText.text = "Ayant échoué dans cette tentative vous vous êtes fait juger par les dieux";
        blackImage.CrossFadeAlpha(0, 1, true);
        soulImage.gameObject.SetActive(false);
        judgmentimage.gameObject.SetActive(true);
        yield return new WaitForSeconds(4.0f);
        mainText.text = "Vous êtes donc condamnés a ramper sur le monde tel qu'une âme errante";

        yield return new WaitForSeconds(4.0f);
        blackImage.CrossFadeAlpha(1, 1, true);
        yield return new WaitForSeconds(4.0f);
        mainText.text = "Choisissez la voir de la rédemption ou de la vengeance";
        destinyImage.gameObject.SetActive(true);
        judgmentimage.gameObject.SetActive(false);
        yield return new WaitForSeconds(4.0f);
        blackImage.CrossFadeAlpha(0, 1, true);
        yield return new WaitForSeconds(4.0f);
        mainText.text = "Cherchez les dieux pour accomplir votre destinée";
        blackImage.CrossFadeAlpha(1.0f, 1.0f, true);
        yield return new WaitForSeconds(4.0f);
        destinyImage.gameObject.SetActive(false);
        worldImage.gameObject.SetActive(true);
        mainText.text = "De vastes mondes vous attendent";
        blackImage.CrossFadeAlpha(0, 2, true);
        yield return new WaitForSeconds(4.0f);
        mainText.text = "Aurez la force d'explorer l'univers à la recherche des dieux ?";
        yield return new WaitForSeconds(4.0f);
        mainText.CrossFadeAlpha(0, 1, true);
        titleImage.CrossFadeAlpha(1, 3, true);
        yield return new WaitForSeconds(4.0f);
        pressButtonText.CrossFadeAlpha(1, 1, true);
        canPressButton = true;
        PlayerPrefs.SetInt("First Play", 1);
        PlayerPrefs.Save();
        yield return null;
    }
}
