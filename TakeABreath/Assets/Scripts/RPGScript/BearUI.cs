using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BearUI : MonoBehaviour {

    [SerializeField]
    PlayerManager playerManager;

    [SerializeField]
    Text uiLevel;
    [SerializeField]
    Text uiExp;
    [SerializeField]
    Image uiExpImage;

    [SerializeField]
    Text uiHP;
    [SerializeField]
    Image uiHPImage;

    void OnEnable()
    {
        PlayerManager.OnMonsterPossessed += PossessMonster;
        PlayerManager.OnMonsterReleased += ReleaseMonster;
    }

    void OnDisable()
    {
        PlayerManager.OnMonsterPossessed -= PossessMonster;
        PlayerManager.OnMonsterReleased -= ReleaseMonster;
    }

    // Use this for initialization
    void Start () {
        uiHPImage.CrossFadeAlpha(0.0f, 0.1f, true);
        uiLevel.text = "Niv" + playerManager.Me.Level;
    }
	
	// Update is called once per frame
	void Update () {
        
        uiExpImage.fillAmount = Mathf.Lerp(uiExpImage.fillAmount, (float)playerManager.Me.Exp / (float)playerManager.Me.ExpToLvlUp, 5*Time.deltaTime);
        if(playerManager.MonstrePossede != null)
        {
            uiHPImage.fillAmount = Mathf.Lerp(uiHPImage.fillAmount, (float)playerManager.MonstrePossede.Sante / (float)playerManager.MonstrePossede.SanteMax, 5 * Time.deltaTime);
            uiHP.text = playerManager.MonstrePossede.Sante + "/" + playerManager.MonstrePossede.SanteMax;
        }
        else
        {
            uiHP.text = "--/--";
        }
       

    }

    void FixedUpdate()
    {
        uiLevel.text = "Niv" + playerManager.Me.Level;
    }

    void PossessMonster()
    {
        uiHPImage.CrossFadeAlpha(1.0f, 0.5f, true);
    }

    void ReleaseMonster()
    {
        uiHPImage.CrossFadeAlpha(0.0f, 0.5f, true);
    }
}
