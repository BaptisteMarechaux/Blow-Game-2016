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

    void Awake()
    {
        quest_000 = new Quest(0,"Suivez le maître","Suivre le maître",5);


        allQuests[0] = quest_000;

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

        ppm.Save();
    }

    //mettre à jour toutes les quêtes du jeu (ajouté les nouvelles)
    private void VerifyExistingQuest()
    {
        ppm.Save();
    }
    
    //Mettre à jour le playerprefs lorsqu'une quête est finie
    public void ChangeStatQuest(string name, int nb)
    {
        //-3 = non dispo, -2 = dispo, -1 = activ, 0= fini
        ppm.UpdateQuest(name, nb);
    }
    
    public void CleanBookSave()
    {
        ppm.CleanBookQuest();
    }
}
