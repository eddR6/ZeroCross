using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public InputField createRoom;
    public InputField joinRoom;
    public GameObject waitingScreen;
    public GameObject connectedScreen;
    public void OnCreateRoomButton()
    {
        if (createRoom.text != "")
        {
            PhotonNetwork.CreateRoom(createRoom.text, new RoomOptions { MaxPlayers = 2 });
        }
        else
        {
            Toast.Instance.Show("No room id provided.", 2f, Toast.ToastColor.Red);
        }
    }
    public void OnJoinRoomButton()
    {
        if (joinRoom.text != "")
        {
            PhotonNetwork.JoinRoom(joinRoom.text);
        }
        else
        {
            Toast.Instance.Show("No room id provided.", 2f, Toast.ToastColor.Red);
        }
    }
    
    public override void OnCreatedRoom()
    {
        Debug.Log("Room created as: " + createRoom.text);
        Toast.Instance.Show("Room created as: " + createRoom.text, 3f, Toast.ToastColor.Blue);
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("in joined room!");
        Toast.Instance.Show("Room Joined!", 2f, Toast.ToastColor.Blue);
        if (PhotonNetwork.IsMasterClient)
        {
            waitingScreen.SetActive(true);
        }
        //FOR CLIENT TO LOAD THE LEVEL
        if (PhotonNetwork.PlayerList.Length== 2)
        {
            Debug.Log("Players= "+PhotonNetwork.PlayerList.Length);
            SceneManager.LoadScene("Level1");
        }
        
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log(message);
        Toast.Instance.Show("Room already exists... " + createRoom.text, 2f, Toast.ToastColor.Red);
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log(message);
        Toast.Instance.Show(message, 2f, Toast.ToastColor.Red);
    }
    //Load the level FOR MASTER when second player (CLIENT) enters
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Toast.Instance.Show("New Player entered", 2f, Toast.ToastColor.Green);
        SceneManager.LoadScene("Level1");
    }
    public void OnCancelRoomButton()
    {
        PhotonNetwork.LeaveRoom();
        waitingScreen.SetActive(false);
    }
}
