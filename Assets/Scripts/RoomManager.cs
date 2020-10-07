using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public InputField createRoom;
    public InputField joinRoom;
    public void OnCreateRoomButton()
    {
        if (createRoom.text != "")
        {
            PhotonNetwork.CreateRoom(createRoom.text, new RoomOptions { MaxPlayers = 2 });
        }
        else
        {
            Debug.Log("No room id provided.");
        }
    }
    public override void OnCreatedRoom()
    {
        Debug.Log("Room created as: " + createRoom.text);
       // PhotonNetwork.JoinRoom(createRoom.text);
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("in joined room!");
        //PhotonNetwork.LeaveRoom();
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log(message);
        createRoom.placeholder.GetComponent<Text>().text = "Room already exists...";
        //((Text)(createRoom.placeholder)).text = "Room already exists...";
    }
}
