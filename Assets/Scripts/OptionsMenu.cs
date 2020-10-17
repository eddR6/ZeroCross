using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class OptionsMenu : MonoBehaviourPunCallbacks
{
    public GameObject mainmenu;
    public void OnOptionsButton()
    {
        mainmenu.SetActive(true);
    }
    public void OnResumeButton()
    {
        mainmenu.SetActive(false);
    }
    public void OnMainMenuButton()
    {
        PhotonNetwork.Disconnect();
        SceneManager.LoadScene("Main");

    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Toast.Instance.Show("Opponent Left the game...", 3f, Toast.ToastColor.Red);
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        SceneManager.LoadScene("Main");
    }
}
