using UnityEngine;
using System.Collections;

public class Quest
{
    private int id;
    private string name;
    private string description;
    private int exp;

    public Quest (int id, string name, string descrition, int exp)
    {
        this.id = id;
        this.name = name;
        this.description = descrition;
        this.exp = exp;
    }
    
    public int getExp()
    {
        return exp;
    }
}
