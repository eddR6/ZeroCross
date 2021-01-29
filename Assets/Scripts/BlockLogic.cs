using UnityEngine;
using UnityEngine.UI;

public class BlockLogic : MonoBehaviour
{
    [SerializeField]
    private int id;
    private Button button;
    [SerializeField]
    private BoardLogic boardLogic;
    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(UpdateBlock);
    }
    void UpdateBlock()
    {
        boardLogic.UpdateBoard(id);
    }
    
}
