using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.Networking.Types;
using System.Collections;
using System;
using System.Collections.Generic;

public class NetworkStartScript : MonoBehaviour
{
    [SerializeField]
    NetworkManager mgr;

    public string matchName = "PJAnn";

    bool isHost = false;

    void Start()
    {
        NetworkManager.singleton.StartMatchMaker();
    }

    void Update()
    {

    }

    public void LaunchServer()
    {
        isHost = true;
        GetComponent<NetworkMatch>().CreateMatch(matchName, 4, true, "", "", "", 0, 0, MatchCallback);
    }

    public void LaunchClient()
    {
        isHost = false;
        FindInternetMatch();
    }

    private void MatchCallback(bool success, string extendedInfo, MatchInfo responseData)
    {
        if (responseData != null && success)
        {
            MatchInfo hostInfo = responseData;
            NetworkServer.Listen(hostInfo, 9000);

            NetworkManager.singleton.StartHost(hostInfo);
        }
        else
        {
            Debug.LogError("Create match failed");
        }
    }

    public void FindInternetMatch()
    {
        NetworkManager.singleton.matchMaker.ListMatches(0, 20, matchName, false, 0, 0, OnInternetMatchList);
    }

    private void OnInternetMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> responseData)
    {
        if (success)
        {
            if (responseData.Count != 0)
            {
                NetworkManager.singleton.matchMaker.JoinMatch(responseData[responseData.Count - 1].networkId, "", "", "", 0, 0, OnJoinInternetMatch);
            }
            else
            {
                Debug.Log("No matches in requested room!");
            }
        }
        else
        {
            Debug.LogError("Couldn't connect to match maker");
        }
    }

    private void OnJoinInternetMatch(bool success, string extendedInfo, MatchInfo responseData)
    {
        if (success)
        {
            MatchInfo hostInfo = responseData;
            NetworkManager.singleton.StartClient(hostInfo);
        }
        else
        {
            Debug.LogError("Join match failed");
        }
    }

    public void Quid()
    {
        if (isHost)
        {
            NetworkManager.singleton.StopHost();
            NetworkManager.singleton.StopMatchMaker();
            NetworkManager.singleton.StartMatchMaker();
        }
        else{
            NetworkManager.singleton.StopClient();
            NetworkManager.singleton.StopMatchMaker();
            NetworkManager.singleton.StartMatchMaker();
        }
    }
}
