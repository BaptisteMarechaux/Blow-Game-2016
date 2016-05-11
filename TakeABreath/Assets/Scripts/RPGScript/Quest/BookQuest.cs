using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BookQuest : MonoBehaviour {

    public Quest[] allQuests = new Quest[2];

    Quest quest_000 = new Quest("Premier corps.", "Quest-PossessionGolem",
        "Avant de vous vengez contre les anciennes Divinités, il faut que vous investissez des petites créature pour vous habituez à la possession.", 
        "Prennez possession d'un Golem", "Golem", 30);
    Quest quest_001 = new Quest("Premier combat.", "Quest-Tuez5Golem",
        "Maintenant que vous avez cette première créature, attaquez d'autres golems.", 
        "Tuez 5 Golems", "Golem", 100 , 5);
    
    // Use this for initialization
    void Start ()
    {
        allQuests[0] = quest_000;
        allQuests[1] = quest_001;

        Debug.Log(quest_000);
        Debug.Log(allQuests.Length);
        Debug.Log(allQuests[0]);

        if (!PlayerPrefs.HasKey("BookQuest"))
        {
            CreateBookQuest();
        }
        VerifyExistingQuest();
    }

    private void CreateBookQuest()
    {
        //Création de la key pour dire qu'on a les quetes
        PlayerPrefs.SetInt("BookQuest", 1);

        //Création des autres key de quêtes
        PlayerPrefs.SetInt("Quest-PossessionGolem", 0);

        PlayerPrefs.SetInt("Quest-Tuez5Golem", -1);
        PlayerPrefs.SetInt("Quest-Tuez5Golem-Avancement", 0);

        PlayerPrefs.Save();
    }

    //mettre à jour toutes les quêtes du jeu (ajouté les nouvelles)
    private void VerifyExistingQuest()
    {
        if(!PlayerPrefs.HasKey("Quest-PossessionGolem"))
            PlayerPrefs.SetInt("Quest-PossessionGolem", 0);

        if (!PlayerPrefs.HasKey("Quest-Tuez5Golem"))
        {
            PlayerPrefs.SetInt("Quest-Tuez5Golem", -1);
            PlayerPrefs.SetInt("Quest-Tuez5Golem-Avancement", 0);
        }
        PlayerPrefs.Save();
    }
    
    //Mettre à jour le playerprefs lorsqu'une quête est finie
    public void ChageStatQuest(string name, int nb)
    {
        //-1 = non dispo, 0 = dispo, 1 = activ, 2= fini
        PlayerPrefs.SetInt(name, nb);
    }
    
}
