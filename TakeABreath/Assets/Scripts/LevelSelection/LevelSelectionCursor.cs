using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class LevelSelectionCursor : MonoBehaviour {

    [SerializeField]
    LevelDescription levelDes;

    [SerializeField]
    List<Transform> LevelTarget;

    Vector3 usedTarget;

    [SerializeField]
    List<Camera> transCameras;

    [SerializeField]
    Camera mainCam;

    bool firstInput;

    public int usedIndex;

    public int[] levels;

    [SerializeField]
    GameObject notAvailText;

    public Image startBlackImage;
    public Text startBlackImageText;
    
	void Start () {
        levelDes.ChangeLevelInfos(0);
        StartCoroutine("StartSceneRoutine");
        
    }
	
	// Update is called once per frame
	void Update () {
        SelectedLevelManagement();
        if(firstInput)
        {
            transform.position = Vector3.Lerp(transform.position, usedTarget, 8 * Time.deltaTime);
            mainCam.transform.position = Vector3.Lerp(mainCam.transform.position, transCameras[usedIndex].transform.position, 5 * Time.deltaTime);
            mainCam.transform.rotation = Quaternion.Lerp(mainCam.transform.rotation, transCameras[usedIndex].transform.rotation, 5 * Time.deltaTime);
        }
      
	}

    void SelectedLevelManagement()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            usedIndex--;
            if(usedIndex < 0)
            {
                usedIndex = LevelTarget.Count - 1;
                
            }
            usedTarget = LevelTarget[usedIndex].position;
            levelDes.ChangeLevelInfos(usedIndex);
            if (!firstInput) firstInput = true;
        }

        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            usedIndex++;
            if (usedIndex > LevelTarget.Count-1)
            {
                usedIndex = 0;
               
            }
            usedTarget = LevelTarget[usedIndex].position;
            levelDes.ChangeLevelInfos(usedIndex);
            if (!firstInput) firstInput = true;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            SelectLevel(usedIndex);
        }
    }

    public void SelectLevel(int sel)
    {
        if(levels[sel] == 0)
        {
            notAvailText.SetActive(false);
            notAvailText.SetActive(true);
        }
        else
        {
            if(PlayerPrefs.GetInt("VisitedLevels") < sel)
            {
                notAvailText.SetActive(false);
                notAvailText.SetActive(true);
            }
            else
            {
                SceneManager.LoadScene(levels[sel]);
            }
            
            
        }
    }

    public void SelectActiveIndexLevel()
    {
        SelectLevel(usedIndex);
    }

    IEnumerator StartSceneRoutine()
    {
        yield return new WaitForSeconds(1.0f);
        startBlackImage.CrossFadeAlpha(0, 1.5f, true);
        startBlackImageText.CrossFadeAlpha(0, 1.5f, true);
        yield return null;
    }
}
