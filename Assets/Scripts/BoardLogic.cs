using System.Collections;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
            StartCoroutine(InitialClientMessage());
        }
    }

    IEnumerator InitialClientMessage()
    {
        yield return new WaitForSeconds(1);
        Toast.Instance.Show("Opponent's turn...", 0.2f, Toast.ToastColor.Blue);
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
    private void SetBoard(int id,string player)
    {
        if (player=="Master" )
        {
            blocks[id].GetComponentInChildren<Text>().text = "o";
            blockUsed[id] = 1;
        }
        else if (player == "Client")
        {
            blocks[id].GetComponentInChildren<Text>().text = "x";
            blockUsed[id] = 1;
        }

        //
        if (!CheckForWinCondition(player))
        {
            ToggleActiveBoard(player);
        }
    }

    private bool CheckForWinCondition(string player)
    {
        string[] str=new string[blocks.Length];
        int i = 0;
        foreach(GameObject block in blocks)
        {
            str[i] = block.GetComponentInChildren<Text>().text;
            i++;
        }
        //check win condition
        if(str[0]!="" && str[1]==str[0] && str[2]==str[1])
        {
            DisplayWin(player);
            return true;
        }
        else if (str[3] != "" && str[4] == str[3] && str[5] == str[4])
        {
            DisplayWin(player);
            return true;
        }
        else if (str[6] != "" && str[7] == str[6] && str[8] == str[7])
        {
            DisplayWin(player);
            return true;
        }
        else if (str[0] != "" && str[3] == str[0] && str[6] == str[3])
        {
            DisplayWin(player);
            return true;
        }
        else if (str[1] != "" && str[4] == str[1] && str[7] == str[4])
        {
            DisplayWin(player);
            return true;
        }
        else if (str[2] != "" && str[5] == str[2] && str[8] == str[5])
        {
            DisplayWin(player);
            return true;
        }
        else if (str[0] != "" && str[4] == str[0] && str[8] == str[4])
        {
            DisplayWin(player);
            return true;
        }
        else if (str[2] != "" && str[4] == str[2] && str[6] == str[4])
        {
            DisplayWin(player);
            return true;
        }
        //TIE CONDITION
        bool isFull = true;
        foreach(string s in str)
        {
            if (s == "")
            {
                isFull = false;
                break;
            }
        }
        if (isFull)
        {
            DisplayTie();
            return true;
        }
        return false;
    }
    private void DisplayTie()
    {
        Toast.Instance.Show("Its a Tie!", 4f, Toast.ToastColor.Blue);
        //Resets the Board
        StartCoroutine(ResetBoard());

    }
    private void DisplayWin(string player)
    {
        if (PhotonNetwork.IsMasterClient && player == "Master")
        {   
            Toast.Instance.Show("You win!", 4f, Toast.ToastColor.Green);
        }
        else if (PhotonNetwork.IsMasterClient && player == "Client")
        {
            Toast.Instance.Show("You lose!", 4f, Toast.ToastColor.Red);
        }
        else if (!PhotonNetwork.IsMasterClient && player == "Client")
        {
            Toast.Instance.Show("You win!", 4f, Toast.ToastColor.Green);
        }
        else if (!PhotonNetwork.IsMasterClient && player == "Master")
        {
            Toast.Instance.Show("You lose!", 4f, Toast.ToastColor.Red);
        }
        //Resets the Board
        StartCoroutine(ResetBoard());
    }
    IEnumerator ResetBoard()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Level1");
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
