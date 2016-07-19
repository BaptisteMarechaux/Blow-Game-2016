using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.CinematicEffects;
using UnityEngine.SceneManagement;

public class SceneSoutenanceEvent : MonoBehaviour {
    public Transform introCamera;
    public Transform planet;

    public Image blackImage;

    public GameObject gameplayGroup;

    public Text mainCenterText;
    public Image titleImage;

    public Text teamLabel;

    public Text tName;
    public Text tSub;

    public Text jName;
    public Text jSub;

    public Text bName;
    public Text bSub;

    public Text cName;
    public Text cSub;

    public Graphic presentationPanel;

    float time=0;

    bool titleDisplayed;
    public bool canShowNames;

    public bool canPressForTerrain;
    public bool canShowTerrain;

    public AmbientOcclusion ambiantOcclusion;

    public SimpleShpereGeneration sphereGenerator;

    public Transform belowCamera;

    public GameObject changePlanetButton;

    public int demoIndex=0;

    Vector3 initialCameraPos;
    Quaternion initialCameraRot;

    public int speed = 1;

	// Use this for initialization
	void Start () {
        initialCameraPos = introCamera.transform.position;
        initialCameraRot = introCamera.transform.rotation;
        blackImage.gameObject.SetActive(true);
        titleImage.gameObject.SetActive(true);
        titleImage.CrossFadeAlpha(0, 0, true);

        presentationPanel.gameObject.SetActive(false);

        tName.gameObject.SetActive(true);
        tName.CrossFadeAlpha(0, 0, true);
        tSub.gameObject.SetActive(true);
        tSub.CrossFadeAlpha(0, 0, true);
        jName.gameObject.SetActive(true);
        jName.CrossFadeAlpha(0, 0, true);
        jSub.gameObject.SetActive(true);
        jSub.CrossFadeAlpha(0, 0, true);
        bName.gameObject.SetActive(true);
        bName.CrossFadeAlpha(0, 0, true);
        bSub.gameObject.SetActive(true);
        bSub.CrossFadeAlpha(0, 0, true);
        cName.gameObject.SetActive(true);
        cName.CrossFadeAlpha(0, 0, true);
        cSub.gameObject.SetActive(true);
        cSub.CrossFadeAlpha(0, 0, true);

        teamLabel.gameObject.SetActive(true);
        teamLabel.CrossFadeAlpha(0, 0, true);

        mainCenterText.gameObject.SetActive(true);
        mainCenterText.CrossFadeAlpha(0, 0, true);

        changePlanetButton.gameObject.SetActive(false);

        StartCoroutine("IntroProcess");
        
	}
	
	// Update is called once per frame
	void Update () {

        if(canShowNames)
        {
            introCamera.RotateAround(planet.position, Vector3.up, Time.deltaTime * 2 * speed);
        }

        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            demoIndex -= 1;
            if (demoIndex < 0)
                demoIndex = 0;
            switch(demoIndex)
            {
                case 0:
                    StopAllCoroutines();
                    StartCoroutine("IntroProcess");
                    break;

                case 1:
                    StopAllCoroutines();
                    StartCoroutine("ShowStaff");
                    break;
                case 2:
                    StopAllCoroutines();
                    StartCoroutine("ShowProjectPitch");
                    break;
                case 3:
                    StopAllCoroutines();
                    StartCoroutine("ShowPlanet");
                    break;


            }
        }

        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            demoIndex += 1;
            if (demoIndex > 3)
                demoIndex = 3;
            switch (demoIndex)
            {
                case 0:
                    StopAllCoroutines();
                    StartCoroutine("IntroProcess");
                    break;

                case 1:
                    StopAllCoroutines();
                    StartCoroutine("ShowStaff");
                    break;
                case 2:
                    StopAllCoroutines();
                    StartCoroutine("ShowProjectPitch");
                    break;
                case 3:
                    StopAllCoroutines();
                    StartCoroutine("ShowPlanet");
                    break;

            }
        }


        if (sphereGenerator.drawnMesh)
        {
            if(Input.GetKeyDown(KeyCode.A))
            {
                sphereGenerator.seed -= 1;
                sphereGenerator.OnValidate();
            }

            if(Input.GetKeyDown(KeyCode.Z))
            {
                sphereGenerator.seed += 1;
                sphereGenerator.OnValidate();
            }
        }

        if(Input.GetKey(KeyCode.Space))
        {
            speed = 10;
        }
        else
        {
            speed = 2;
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

    IEnumerator IntroProcess()
    {
        titleImage.CrossFadeAlpha(0, 0, true);
        blackImage.CrossFadeAlpha(1, 0, true);
        tName.CrossFadeAlpha(0, 0, true);
        tSub.CrossFadeAlpha(0, 0, true);
        jName.CrossFadeAlpha(0, 0, true);
        jSub.CrossFadeAlpha(0, 0, true);
        bName.CrossFadeAlpha(0, 0, true);
        bSub.CrossFadeAlpha(0, 0, true);
        cName.CrossFadeAlpha(0, 0, true);
        cSub.CrossFadeAlpha(0, 0, true);
        teamLabel.CrossFadeAlpha(0, 0, true);
        canShowNames = false;
        demoIndex = 0;
        time = 0;
        introCamera.position = initialCameraPos;
        introCamera.rotation = initialCameraRot;
        yield return new WaitForSeconds(1);
        
        blackImage.CrossFadeAlpha(0, 2, true);
        while(time < 10)
        {
            time += Time.fixedDeltaTime;
            introCamera.transform.Translate(Vector3.forward * 5*time/5 * Time.fixedDeltaTime);
            if(5 - time < 0.1f)
            {
                titleImage.CrossFadeAlpha(1, 3, true);
            }

            if(9 - time < 0.1f)
            {
                blackImage.CrossFadeAlpha(1, 1, true);
                
            }
            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForSeconds(3);

        yield return null;
    }

    IEnumerator ShowStaff()
    {
        introCamera.position = initialCameraPos;
        introCamera.rotation = initialCameraRot;
        demoIndex = 1;
        canShowNames = true;
        titleImage.CrossFadeAlpha(0, 0, true);
        ambiantOcclusion.settings.intensity = 1.5f;
        ambiantOcclusion.settings.radius = 0.3f;
        blackImage.CrossFadeAlpha(0, 1, true);
        presentationPanel.gameObject.SetActive(false);


        yield return new WaitForSeconds(1);

        teamLabel.CrossFadeAlpha(1, 1, true);

        yield return new WaitForSeconds(1);

        tName.CrossFadeAlpha(1, 1, true);
        tSub.CrossFadeAlpha(1, 1, true);

        yield return new WaitForSeconds(1);

        jName.CrossFadeAlpha(1, 1, true);
        jSub.CrossFadeAlpha(1, 1, true);

        yield return new WaitForSeconds(1);

        bName.CrossFadeAlpha(1, 1, true);
        bSub.CrossFadeAlpha(1, 1, true);

        yield return new WaitForSeconds(1);

        cName.CrossFadeAlpha(1, 1, true);
        cSub.CrossFadeAlpha(1, 1, true);

        yield return null;
    }

    IEnumerator ShowProjectPitch()
    {
        tName.CrossFadeAlpha(0, 1, true);
        tSub.CrossFadeAlpha(0, 1, true);
        jName.CrossFadeAlpha(0, 1, true);
        jSub.CrossFadeAlpha(0, 1, true);
        bName.CrossFadeAlpha(0, 1, true);
        bSub.CrossFadeAlpha(0, 1, true);
        cName.CrossFadeAlpha(0, 1, true);
        cSub.CrossFadeAlpha(0, 1, true);
        teamLabel.CrossFadeAlpha(0, 1, true);
        canShowNames = false;
        changePlanetButton.gameObject.SetActive(false);

        while (Vector3.Distance(introCamera.position, belowCamera.position) > 0.1f)
        {
            introCamera.position = Vector3.Lerp(introCamera.transform.position, belowCamera.position, 6 * Time.fixedDeltaTime);
            introCamera.rotation = Quaternion.Lerp(introCamera.rotation, belowCamera.rotation, 6 * Time.fixedDeltaTime);

            yield return new WaitForFixedUpdate();
        }
        presentationPanel.gameObject.SetActive(true);
        yield return null;
    }

    IEnumerator ShowPlanet()
    {
        presentationPanel.gameObject.SetActive(false);
        while (Vector3.Distance(introCamera.position, initialCameraPos) > 0.1f)
        {
            introCamera.position = Vector3.Lerp(introCamera.transform.position, initialCameraPos, 6 * Time.fixedDeltaTime);
            introCamera.rotation = Quaternion.Lerp(introCamera.rotation, initialCameraRot, 6 * Time.fixedDeltaTime);

            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForSeconds(1);
        canShowNames = true;
        changePlanetButton.gameObject.SetActive(true);
        yield return null;
    }
    
    public void ChangePlanet()
    {
        sphereGenerator.seed++;
        sphereGenerator.OnValidate();
    }
}
