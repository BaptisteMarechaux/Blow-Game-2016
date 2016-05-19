using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PossessionScript : MonoBehaviour {

    public void Possession(PlayerManager player, MonsterClass monster)
    {
        if (player.PlayerStats.Level >= monster.Level && monster.Player == null)
        {
            if (Vector3.Distance(player.transform.position, monster.transform.position) <= 5)
            {
                player.MonstrePossede = monster;
                monster.Player = player;
                monster.DisableAI();

                this.transform.position = monster.transform.position;
                player.PlayerStats.addExp(player.MonstrePossede.ExpToPossess);
            }
        }
    }
}
