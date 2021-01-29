using System.Collections;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BoardLogic : MonoBehaviour
{
    [SerializeField]
    private GameObject[] blocks;
    private string[] str;
    private CurrentPlayer currentPlayer;

    private void Start()
    {
        currentPlayer=CurrentPlayer.Player;
        str = new string[blocks.Length];
    }

    public void UpdateBoard(int id)
    {
        string blockText = blocks[id].GetComponentInChildren<Text>().text;

        if (blockText != "")
        {
            return;
        }
        if (currentPlayer == CurrentPlayer.Player)
        {
            blocks[id].GetComponentInChildren<Text>().text = "x";
            if (!CheckForWinCondition())
            {
                currentPlayer = CurrentPlayer.CPU;
                CPUTurn();
            } 
        }  
    }

    private void CPUTurn()
    {
        while (true)
        {
            int index = Random.Range(0, 9);
           // Debug.Log("rr " + index);
            if (blocks[index].GetComponentInChildren<Text>().text == "")
            {
                blocks[index].GetComponentInChildren<Text>().text = "o";
                if (!CheckForWinCondition())
                {
                    currentPlayer = CurrentPlayer.Player;
                    
                }
                break;
            }
        }
    }

    private bool CheckForWinCondition()
    {
        int i = 0;
        foreach(GameObject block in blocks)
        {
            str[i] = block.GetComponentInChildren<Text>().text;
            i++;
        }
        //check win condition
        
        if(str[0]!="" && str[1]==str[0] && str[2]==str[1])
        {
            DisplayWin(currentPlayer);
            return true;
        }
        else if (str[3] != "" && str[4] == str[3] && str[5] == str[4])
        {
            DisplayWin(currentPlayer);
            return true;
        }
        else if (str[6] != "" && str[7] == str[6] && str[8] == str[7])
        {
            DisplayWin(currentPlayer);
            return true;
        }
        else if (str[0] != "" && str[3] == str[0] && str[6] == str[3])
        {
            DisplayWin(currentPlayer);
            return true;
        }
        else if (str[1] != "" && str[4] == str[1] && str[7] == str[4])
        {
            DisplayWin(currentPlayer);
            return true;
        }
        else if (str[2] != "" && str[5] == str[2] && str[8] == str[5])
        {
            DisplayWin(currentPlayer);
            return true;
        }
        else if (str[0] != "" && str[4] == str[0] && str[8] == str[4])
        {
            DisplayWin(currentPlayer);
            return true;
        }
        else if (str[2] != "" && str[4] == str[2] && str[6] == str[4])
        {
            DisplayWin(currentPlayer);
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
        ResetBoard();
    }

    private void DisplayWin(CurrentPlayer player)
    {
        if (player == CurrentPlayer.Player)
        {
            Toast.Instance.Show("Player Wins!", 4f, Toast.ToastColor.Green);
            ScoreManager.UpdateScore(10);
        }
        else
        {
            Toast.Instance.Show("CPU wins!", 4f, Toast.ToastColor.Red);
            ScoreManager.UpdateScore(0);
        }
        ResetBoard();
    }

    void ResetBoard()
    {
        StartCoroutine(SceneReset());
    }

    IEnumerator SceneReset()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Level1");
    }
}
