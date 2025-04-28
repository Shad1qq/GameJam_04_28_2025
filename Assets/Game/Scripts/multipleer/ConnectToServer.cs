using UnityEngine;
using Photon.Pun;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    public GameObject loadText;//текст о подключении к серверу
    public GameObject panelMu;//панель подключения к румам
    public GameObject paneдCreateRoom;//панель создания румы

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        loadText.SetActive(true);
        panelMu.SetActive(false);
        paneдCreateRoom.SetActive(false);
    }
    public override void OnConnectedToMaster()
    {
        loadText.SetActive(false);
        panelMu.SetActive(true);
    }//приконектились к серверу

    /// <summary>
    /// метод вызывается когда игрок конектится к комнате
    /// </summary>
    public override void OnJoinedRoom()
    {
        paneдCreateRoom.SetActive(false);
    }

}
