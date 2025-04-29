using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviourPunCallbacks
{
    public TMP_InputField FieldCreate;//ввод названия румы для создания румы
    public TMP_InputField FieldConnect;//ввод названия румы для конекта

    public GameObject panelRoom;//в руме панель
    public GameObject playButton;//кнопка старта игры

    private void Start()
    {
        panelRoom.SetActive(false);
    }

    #region button
    public void CreateRoom()
    {
        RoomOptions roomOptions = new();
        roomOptions.MaxPlayers = 2;
        PhotonNetwork.CreateRoom(FieldCreate.text, roomOptions);
    }
    public void ConnectRoom()
    {
        PhotonNetwork.JoinRoom(FieldConnect.text);
    }
    public void ExitRoom(){
        if (PhotonNetwork.InRoom)
            PhotonNetwork.LeaveRoom();
    }
    #endregion

    /// <summary>
    /// метод вызывается когда игрок конектится к комнате
    /// </summary>
    public override void OnJoinedRoom()
    {
        panelRoom.SetActive(true);

        if (PhotonNetwork.IsMasterClient)
        {
            playButton.SetActive(true); // Активируем кнопку для создателя
            playButton.GetComponent<Button>().onClick.AddListener(StartGame);
        }
        else
            playButton.SetActive(false);
    }

    /// <summary>
    /// ливнул с румы вызвался метод
    /// </summary>
    public override void OnLeftRoom()
    {
        // Этот метод вызывается после успешного выхода из комнаты
        panelRoom.SetActive(false);
        // Здесь можно добавить логику для перехода на главный экран или другую сцену
    }

    // Метод для загрузки уровня для всех игроков
    private void StartGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("Game");
            PhotonNetwork.AutomaticallySyncScene = true;
        }   
    }
}
