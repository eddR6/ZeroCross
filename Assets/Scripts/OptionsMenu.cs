using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    public GameObject mainmenu;

    private void Start()
    {
    }

    public void OnOptionsButton()
    {
        mainmenu.SetActive(true);
    }
    public void OnResumeButton()
    {
        mainmenu.SetActive(false);
        
        SaveSystem.SaveGame();
    }
    public void OnMainMenuButton()
    {
        SceneManager.LoadScene("Main");
    }


}
