using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PossessionScript : MonoBehaviour {

    public void Possession(PlayerManager player, MonsterClass monster)
    {
        if (player.Me.Level >= monster.Level && monster.Player == null)
        {
            player.MonstrePossede = monster;
            monster.Player = player;

            this.transform.position = monster.transform.position;
            player.Me.addExp(player.MonstrePossede.ExpToPossess);
        }
    }
}
