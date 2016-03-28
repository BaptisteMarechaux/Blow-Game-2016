using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.Networking.Types;
using System.Collections;
using System;
using System.Collections.Generic;

public class NetworkStartScript : MonoBehaviour
{
    //private NetworkMatch networkMatch;
    [SerializeField]
    NetworkManager mgr;

    void Start()
    {
        //PlayerPrefs.SetString("CloudNetworkingId", "XXXX");
        //mgr.matchMaker.SetProgramAppID((AppID)XXXX);

        NetworkManager.singleton.StartMatchMaker();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (networkMatch == null)
        {
            var nm = GetComponent<NetworkMatch>();
            if (nm != null)
            {
                networkMatch = nm as NetworkMatch;
                UnityEngine.Networking.Types.AppID appid;
                //appid = 94451;
                networkMatch.SetProgramAppID(appid);
            }
        }*/
    }

    public void LaunchServer()
    {
        /*mgr.StartMatchMaker();
        mgr.StartHost();

        mgr.StartServer();
        mgr.StopHost();*/

        GetComponent<NetworkMatch>().CreateMatch("PJAnn", 4, true, "", "", "", 0, 0, MatchCallback);
    }

    public void LaunchClient()
    {
        FindInternetMatch();
    }

    private void MatchCallback(bool success, string extendedInfo, MatchInfo responseData)
    {
        if (responseData != null && success)
        {
            //Debug.Log("Create match succeeded");

            MatchInfo hostInfo = responseData;
            NetworkServer.Listen(hostInfo, 9000);

            NetworkManager.singleton.StartHost(hostInfo);
        }
        else
        {
            Debug.LogError("Create match failed");
        }
    }

    //public virtual void OnMatchCreate(CreateMatchResponse matchInfo)


    //call this method to find a match through the matchmaker
    public void FindInternetMatch()
    {
        NetworkManager.singleton.matchMaker.ListMatches(0, 20, "PJAnn", false, 0, 0, OnInternetMatchList);
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
            //Debug.Log("Able to join a match");


            MatchInfo hostInfo = responseData;
            NetworkManager.singleton.StartClient(hostInfo);
        }
        else
        {
            Debug.LogError("Join match failed");
        }
    }
}
