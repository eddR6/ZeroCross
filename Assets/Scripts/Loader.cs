using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class Loader : MonoBehaviour
{
    public GameObject board;
    public PhotonView pv;

    public void OnBoardButton()
    {
        Toast.Instance.Show("Button clicked", 2f, Toast.ToastColor.Blue);
        if (PhotonNetwork.IsMasterClient)
        {
            pv.RPC("BoardUpdate", RpcTarget.All, "Master");
        }
        else
        {
            pv.RPC("BoardUpdate", RpcTarget.All, "Client");
        }
    }
    [PunRPC]
    private void BoardUpdate(string msg)
    {
        board.GetComponentInChildren<Text>().text = msg;
        if(msg=="Master" && PhotonNetwork.IsMasterClient)
        {
            board.GetComponent<Button>().enabled = false;
            board.GetComponent<Image>().color = Color.gray;
        }
        if(msg == "Master" && !PhotonNetwork.IsMasterClient)
        {
            board.GetComponent<Button>().enabled = true;
            board.GetComponent<Image>().color = Color.white;
        }
        if (msg == "Client" && PhotonNetwork.IsMasterClient)
        {
            board.GetComponent<Button>().enabled = true;
            board.GetComponent<Image>().color = Color.white;
        }
        if (msg == "Client" && !PhotonNetwork.IsMasterClient)
        {
            board.GetComponent<Button>().enabled = false;
            board.GetComponent<Image>().color = Color.gray;
        }
    }

}
