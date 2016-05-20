using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIStatus : MonoBehaviour {

    public GameObject targetCanvas;

    [SerializeField]
    Text playerNameText;
    [SerializeField]
    Text playerLevelText;
    [SerializeField]
    Text playerExpText;
    [SerializeField]
    Image playerExpImage;

    [SerializeField]
    Text playerHPText;
    [SerializeField]
    Image playerHPImage;

    // Use this for initialization
    void Start () {
        UpdateStatus();
        
    }
	
	// Update is called once per frame
	void FixedUpdate()
    {
        playerHPText.text = UIManager.instance.playerStatus.Sante + "/" + UIManager.instance.playerStatus.SanteMax;
        
        playerHPImage.fillAmount = Mathf.Lerp(playerHPImage.fillAmount, (float)UIManager.instance.playerStatus.Sante / (float)UIManager.instance.playerStatus.SanteMax, 5 * Time.deltaTime);
        playerExpImage.fillAmount = Mathf.Lerp(playerExpImage.fillAmount, (float)UIManager.instance.playerStatus.Exp / (float)UIManager.instance.playerStatus.ExpToLvlUp, 5 * Time.deltaTime);
    }

    public void UpdateStatus()
    {
        playerNameText.text = UIManager.instance.playerStatus.Name;
        playerLevelText.text = "Niv" + UIManager.instance.playerStatus.Level;
        playerExpText.text = UIManager.instance.playerStatus.Exp + "/" + UIManager.instance.playerStatus.ExpToLvlUp;
        
    }

    public void UpdateLevel()
    {
        playerLevelText.text = "Niv" + UIManager.instance.playerStatus.Level;
    }

    public void UpdateExp()
    {
        playerExpText.text = UIManager.instance.playerStatus.Exp + "/" + UIManager.instance.playerStatus.ExpToLvlUp;
    }

}
