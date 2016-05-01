using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetworkManagerCustom : NetworkManager {

    [SerializeField]
    //Transform[] spawnPoints;
    Vector3[] spawnPoints;

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        int index = Random.Range(0, spawnPoints.Length);
        GameObject player = (GameObject)Instantiate(playerPrefab, spawnPoints[index], Quaternion.identity/*spawnPoints[index].rotation*/);
        player.GetComponent<Move>().myColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }
}
