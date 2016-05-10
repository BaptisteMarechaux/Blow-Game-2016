using UnityEngine;
using System.Collections;

public class Quest : MonoBehaviour
{

    private string title;
    private string nameSave;
    private string description;
    private string objectif;
    private int expReward;
    private int number;
    private string cibleName;

    public string Title
    {
        get
        {
            return title;
        }
    }

    public string NameSave
    {
        get
        {
            return nameSave;
        }
    }

    public string Description
    {
        get
        {
            return description;
        }
    }

    public string Objectif
    {
        get
        {
            return objectif;
        }
    }

    public int ExpReward
    {
        get
        {
            return expReward;
        }
    }

    public int Number
    {
        get
        {
            return number;
        }
    }

    public string CibleName
    {
        get
        {
            return cibleName;
        }
    }

    public Quest(string n, string ns, string desc, string obj, string monst, int exp, int nb)
    {
        this.title = n;
        this.nameSave = ns;
        this.description = desc;
        this.objectif = obj;
        this.expReward = exp;
        this.number = nb;
        this.cibleName = monst;
    }

    public Quest(string n, string ns, string desc, string obj, string monst, int exp)
    {
        this.title = n;
        this.nameSave = ns;
        this.description = desc;
        this.objectif = obj;
        this.expReward = exp;
        this.number = -1;
        this.cibleName = monst;
    }
  
}
