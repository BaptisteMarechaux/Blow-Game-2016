using UnityEngine;
using System.Collections;

public class BookQuest : MonoBehaviour {
    
    public class Quest
    {
        public string name;
        public string description;
        public int nb;
        public string cibleName;

        public Quest(string n, string desc, string monst, int nb)
        {
            this.name = n;
            this.description = desc;
            this.nb = nb;
            this.cibleName = monst;
        }

        public Quest(string n, string desc, string monst)
        {
            this.name = n;
            this.description = desc;
            this.nb = -1;
            this.cibleName = monst;
        }
    }
    
    Quest quest_000 = new Quest("Premier corps.", "Prennez possession d'un Golem", "Golem");
    Quest quest_001 = new Quest("Premier combat.", "Tuez 5 Golems", "Golem", 5);

    // Use this for initialization
    void Start ()
    {
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
        PlayerPrefs.SetInt("Quest-Tuez5Golem", 0);

        PlayerPrefs.Save();
    }

    //mettre à jour toutes les quêtes du jeu (ajouté les nouvelles)
    private void VerifyExistingQuest()
    {
        if(!PlayerPrefs.HasKey("Quest-PossessionGolem"))
            PlayerPrefs.SetInt("Quest-PossessionGolem", 0);
        if (!PlayerPrefs.HasKey("Quest-Tuez5Golem"))
            PlayerPrefs.SetInt("Quest-Tuez5Golem", 0);

        PlayerPrefs.Save();
    }
}
