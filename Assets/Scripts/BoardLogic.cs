using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class BoardLogic : MonoBehaviour
{
    public PhotonView pv;
    public GameObject[] blocks;
    private int[] blockUsed;

    private void Start()
    {
        blockUsed = new int[9];
        //Start condition to disable client controls while first turn goes to Master client
        if (PhotonNetwork.IsMasterClient)
        {
            Toast.Instance.Show("Your turn...", 3f, Toast.ToastColor.Green);
        }
        else
        {
            foreach (GameObject block in blocks)
            {
                block.GetComponent<Button>().enabled = false;
            }
            Toast.Instance.Show("Opponent's turn...", 2f, Toast.ToastColor.Blue);
        }
    }

    public void UpdateBoard(int id)
    {
        if (PhotonNetwork.IsMasterClient && blockUsed[id] == 0)
        {
            pv.RPC("SetBoard", RpcTarget.All, id,"Master");
        }
        else if(!PhotonNetwork.IsMasterClient && blockUsed[id] == 0)
        {
            pv.RPC("SetBoard", RpcTarget.All, id,"Client");
        }
    } 
    [PunRPC]
    private void SetBoard(int id,string str)
    {
        if (str=="Master" )
        {
            blocks[id].GetComponentInChildren<Text>().text = "o";
            blockUsed[id] = 1;
        }
        else if (str == "Client")
        {
            blocks[id].GetComponentInChildren<Text>().text = "x";
            blockUsed[id] = 1;
        }

        ToggleActiveBoard(str);
    }

    private void ToggleActiveBoard(string str)
    {
        if (PhotonNetwork.IsMasterClient && str == "Master")
        {
            foreach (GameObject block in blocks)
            {
                block.GetComponent<Button>().enabled = false;
            }
            Toast.Instance.Show("Opponent's turn...", 2f, Toast.ToastColor.Blue);
        }
        else if (PhotonNetwork.IsMasterClient && str == "Client")
        {
            foreach (GameObject block in blocks)
            {
                block.GetComponent<Button>().enabled = true;
            }
            Toast.Instance.Show("Your turn...", 2f, Toast.ToastColor.Green);
        }
        else if (!PhotonNetwork.IsMasterClient && str == "Client")
        {
            foreach (GameObject block in blocks)
            {
                block.GetComponent<Button>().enabled = false;
            }
            Toast.Instance.Show("Opponent's turn...", 2f, Toast.ToastColor.Blue);
        }
        else if (!PhotonNetwork.IsMasterClient && str == "Master")
        {
            foreach (GameObject block in blocks)
            {
                block.GetComponent<Button>().enabled = true;
            }
            Toast.Instance.Show("Your turn...", 2f, Toast.ToastColor.Green);
        }
    }
}
