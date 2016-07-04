using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Chouette_forest : MonoBehaviour {


    [SerializeField]
    private Transform zoneQuest0;
    [SerializeField]
    private Transform joueur;
    [SerializeField]
    private PlayerManager player;
    [SerializeField]
    private float vitesse = 1f;
    [SerializeField]
    private int questId = 0;
    private int questIdMax = 10;

    [SerializeField]
    private UIManager managerUI;

    [SerializeField]
    PlayerPrefManager ppm;
	[SerializeField]
	List<Collider> zoneQuest3; 

    List<Quest> quests = new List<Quest>();

	private int nbzones;

    Quest quest_001 = new Quest(0, "Suivre le Maitre", "Suivons le maitre, il va m'aider.", 5);
    Quest quest_002 = new Quest(1, "Première Possessin", "Possédez un ilona", 10);
	Quest quest_003 = new Quest(2, "Première Sensation", "Explorez les zones", 10);
	Quest quest_004 = new Quest(3, "Dépossession", "Dépossédez l'ilona", 15);


    // Use this for initialization
    void Start () {
        quests.Add(quest_001);
        quests.Add(quest_002);
		quests.Add(quest_003);
		quests.Add(quest_004);

        if (ppm.GetValue("Quest") <= questIdMax && ppm.GetValue("Quest") > questId)
        {
            questId = ppm.GetValue("Quest");
        }
        managerUI.DisplayActiveQuest(quests[questId].getTitle(), quests[questId].getDescription());

		nbzones = zoneQuest3.Count;
    }

    void FixedUpdate()
    {
        if (questId == 0)
        {
            if (Vector3.Distance(transform.position, joueur.position) < 7 || Vector3.Distance(zoneQuest0.position, joueur.position)<10 || Vector3.Distance(zoneQuest0.position, joueur.position) <= Vector3.Distance(zoneQuest0.position, transform.position))
            {
                transform.position = Vector3.Lerp(transform.position, zoneQuest0.position, Time.deltaTime * vitesse);
            }
            if(Vector3.Distance(transform.position,zoneQuest0.position) <= 1.2f)
            {
                QuestFinish();
            }
        }
		if (questId == 1) 
		{
			if (player.MonstrePossede != null && player.MonstrePossede.Name == "Ilona") 
			{
				QuestFinish();
			}
		}
		if (questId == 2) 
		{
			if (nbzones == 0)
				QuestFinish ();
		}
    }

	void DesableZone(int ind)
	{
		zoneQuest3 [ind].enabled = false;
		nbzones--;
	}

   	void QuestFinish()
    {
        player.AddExp(quests[questId].getExp());
        questId++;
		if(questId < quests.Count)
        	managerUI.DisplayActiveQuest(quests[questId].getTitle(), quests[questId].getDescription());
		//ppm.UpdateQuest ("Quest", questId); <== enregistrer la quête actuel
    }

}
