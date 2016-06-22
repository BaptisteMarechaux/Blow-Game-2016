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

    List<Quest> quests = new List<Quest>();

    Quest quest_001 = new Quest(0, "Suivre le Maitre", "Suivons le maitre, il va m'aider.", 5);


	// Use this for initialization
	void Start () {
        quests.Add(quest_001);
        if (ppm.GetValue("Quest") <= questIdMax && ppm.GetValue("Quest") > questId)
        {
            questId = ppm.GetValue("Quest");
        }
        managerUI.DisplayActiveQuest(quests[questId].getTitle(), quests[questId].getDescription());
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
    }


   void QuestFinish()
    {
        player.addExp(quests[questId].getExp());
        questId++;
    }
}
