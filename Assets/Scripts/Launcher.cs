using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{
    public GameObject connectedScreen;
    public GameObject disconnectedScreen;

    public void OnConnectButton()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("Master Connected.");
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }
    public override void OnJoinedLobby()
    {
        Debug.Log("In Lobby!");
        disconnectedScreen.SetActive(false);
        connectedScreen.SetActive(true);
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        connectedScreen.SetActive(false);
        disconnectedScreen.SetActive(true);
    }
}
