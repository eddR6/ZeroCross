using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockLogic : MonoBehaviour
{
    public int id;
    public Button button;
    public BoardLogic boardLogic;
    private void Start()
    {
        button.onClick.AddListener(UpdateBlock);
    }
    void UpdateBlock()
    {
        boardLogic.UpdateBoard(id);
    }
    
}
