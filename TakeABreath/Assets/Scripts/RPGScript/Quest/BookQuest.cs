using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BookQuest : MonoBehaviour {

    [SerializeField]
    private bool erase;

    [SerializeField]
    private PlayerPrefManager ppm;

    public Quest[] allQuests = new Quest[2];

    Quest quest_000;
    Quest quest_001;

    void Awake()
    {
        quest_000 = new Quest("Premier corps.", "Quest-PossessionGolem",
          "Avant de vous vengez contre les anciennes Divinités, il faut que vous investissez des petites créatures pour vous habituez à la possession.",
          "Prennez possession d'un Golem", "Golem", 30);
        quest_001 = new Quest("Premier combat.", "Quest-Tuez5Golem",
            "Maintenant que vous avez cette première créature, attaquez d'autres golems.",
            "Tuez 5 Golems", "Golem", 100, 5);

        allQuests[0] = quest_000;
        allQuests[1] = quest_001;

        if (erase)
            CleanBookSave();
    }

    // Use this for initialization
    void Start ()
    {
        if (!ppm.Existing("BookQuest"))
        {
            CreateBookQuest();
        }
        VerifyExistingQuest();
    }

    private void CreateBookQuest()
    {
        //Création de la key pour dire qu'on a les quetes
        ppm.CreateDiarieQuest();

        //Création des autres key de quêtes
        ppm.UpdateQuest("Quest-PossessionGolem", 0);

        ppm.CreateQuest("Quest-Tuez5Golem");
        ppm.UpdateQuest("Quest-Tuez5Golem-Avancement", 0);

        ppm.Save();
    }

    //mettre à jour toutes les quêtes du jeu (ajouté les nouvelles)
    private void VerifyExistingQuest()
    {
        if(!ppm.Existing("Quest-PossessionGolem"))
            ppm.UpdateQuest("Quest-PossessionGolem", 0);

        if (!ppm.Existing("Quest-Tuez5Golem"))
        {
            ppm.CreateQuest("Quest-Tuez5Golem");
            ppm.UpdateQuest("Quest-Tuez5Golem-Avancement", 0);
        }

        ppm.Save();
    }
    
    //Mettre à jour le playerprefs lorsqu'une quête est finie
    public void ChageStatQuest(string name, int nb)
    {
        //-3 = non dispo, -2 = dispo, -1 = activ, 0= fini
        ppm.UpdateQuest(name, nb);
    }
    
    public void CleanBookSave()
    {
        ppm.CleanBookQuest();
    }
}
