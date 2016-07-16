using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Chouette_forest : MonoBehaviour {


    [SerializeField]
    private List<Transform> zoneQuest0;
    [SerializeField]
    private Transform joueur;
    [SerializeField]
    private PlayerCharacter player;
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
	private bool parler = false;
	private int nbzones;

    Quest quest_001 = new Quest(0, "Suivre le Maitre", "Suivons le maitre, il va m'aider.", 5);
    Quest quest_002 = new Quest(1, "Première Possessin", "Possédez un ilona", 10);
	Quest quest_003 = new Quest(2, "Première Sensation", "Explorez les zones", 10);
	Quest quest_004 = new Quest(3, "Dépossession", "Dépossédez l'ilona", 15);
	Quest quest_005 = new Quest(4, "Augmentation des compétences", "Augmenter vos stats", 15);
	Quest quest_006 = new Quest(5, "Devenez agressif", "Posséder un quabi", 15);
	Quest quest_007 = new Quest(6, "Work in progress", "Ajouter plus de quête !!!", 15);

	public int QuestId {
		get
		{
			return questId;
		}
	}

    // Use this for initialization
    void Start () {
        quests.Add(quest_001);
        quests.Add(quest_002);
		quests.Add(quest_003);
		quests.Add(quest_004);
		quests.Add(quest_005);
		quests.Add(quest_006);

        if (ppm.GetValue("Quest") <= questIdMax && ppm.GetValue("Quest") > questId)
        {
            questId = ppm.GetValue("Quest");
        }
        managerUI.DisplayActiveQuest(quests[questId].getTitle(), quests[questId].getDescription());

		nbzones = 0;
    }

    void FixedUpdate()
    {
        if (questId == 0)
        {
			Transform lastZone = zoneQuest0[zoneQuest0.Count-1];

			if (Vector3.Distance(transform.position, joueur.position) < 7 || Vector3.Distance(lastZone.position, joueur.position)<10 || Vector3.Distance(lastZone.position, joueur.position) <= Vector3.Distance(lastZone.position, transform.position))
            {
				
				transform.position = Vector3.Lerp(transform.position, zoneQuest0[nbzones].position, Time.deltaTime * vitesse);
				transform.LookAt(Vector3.Lerp(transform.position, zoneQuest0[nbzones].position, Time.deltaTime * vitesse));
				if (Vector3.Distance (transform.position, zoneQuest0 [nbzones].position) < 4 && nbzones<zoneQuest0.Count-1)
					nbzones++;
			}
			if(Vector3.Distance(transform.position,lastZone.position) <= 1.2f)
			{
				nbzones = zoneQuest3.Count;
                QuestFinish();
            }
        }
		if (questId == 1) 
		{
			if (player.possessedMonster != null && player.possessedMonster.monsterName == "Ilona") 
			{
				QuestFinish();
			}
		}
		if (questId == 2) 
		{
			if (nbzones == 0) 
			{
				managerUI.DisplayActiveQuest(quests[questId].getTitle(), "Retournez voir le maître");
				if (parler) 
				{
					parler = false;
					QuestFinish ();
				}
			}
		}
		if (questId == 5) 
		{
			if (player.possessedMonster != null && player.possessedMonster.monsterName == "Quabi") 
			{
				QuestFinish();
			}
		}
    }

	public void DisableZone(int ind)
	{
		zoneQuest3 [ind].gameObject.SetActive(false);
		nbzones--;
	}

	public void Talk()
	{
		parler = true;
	}

   	public void QuestFinish()
    {
        player.AddExperience(quests[questId].getExp());
        questId++;
		if(questId < quests.Count)
        	managerUI.DisplayActiveQuest(quests[questId].getTitle(), quests[questId].getDescription());
		//ppm.UpdateQuest ("Quest", questId); <== enregistrer la quête actuel
    }

}
