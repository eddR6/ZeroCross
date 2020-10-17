using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{
    public GameObject connectedScreen;

    public void OnConnectButton()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            PhotonNetwork.JoinLobby(TypedLobby.Default);
        }
        
    }
    public void OnQuitButton()
    {
        Application.Quit();
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("Master Connected.");
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }
    public override void OnJoinedLobby()
    {
        Debug.Log("In Lobby!");
        connectedScreen.SetActive(true);
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        connectedScreen.SetActive(false);
    }
    public void OnBackLobbyButton()
    {
        PhotonNetwork.LeaveLobby();
        connectedScreen.SetActive(false);
    }
}
