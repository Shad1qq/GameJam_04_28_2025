using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviourPunCallbacks
{
    public void clickStartButton()
    {
        PlayerPrefs.SetString("Milestone", "Spawn");
        clickLoadButton();
    }

    public void clickLoadButton()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("Milestone"));
    }

    public void clickQuitButton()
    {
        if (PhotonNetwork.InRoom)
            PhotonNetwork.LeaveRoom();
        Application.Quit();
    }
}
