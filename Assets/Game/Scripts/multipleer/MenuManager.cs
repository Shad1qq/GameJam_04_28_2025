using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviourPunCallbacks
{
#region param
    public TMP_InputField FieldCreate;//ввод названия румы для создания румы
    public TMP_InputField FieldConnect;//ввод названия румы для конекта
    public TMP_InputField FieldName;//ввод названия румы для конекта

    public GameObject prefabPanelUser;
    public GameObject panelUsers;

    public GameObject panelRoom;//в руме панель
    public GameObject panelMu;//панель подключения к румам
    public GameObject playButton;//кнопка старта игры
    private int uiElementID;
    #endregion

    private void Start()
    {
        panelRoom.SetActive(false);
    }

    #region button
    public void CreateRoom()
    {
        if(string.IsNullOrEmpty(FieldCreate.text)) return;
        
        RoomOptions roomOptions = new();
        roomOptions.MaxPlayers = 2;
        PhotonNetwork.CreateRoom(FieldCreate.text, roomOptions);
    }
    public void ConnectRoom()
    {
        panelMu.SetActive(false);
        PhotonNetwork.JoinRoom(FieldConnect.text);
    }
    public void ExitRoom(){
        if (PhotonNetwork.InRoom)
            PhotonNetwork.LeaveRoom();
    }
    public void SaveName(){
        photonView.RPC("CreateUserPanel", RpcTarget.All, uiElementID, FieldName.text);
    }
    #endregion

    /// <summary>
    /// метод вызывается когда игрок конектится к комнате
    /// </summary>
    public override void OnJoinedRoom()
    {
        panelRoom.SetActive(true);

        PhotonNetwork.AutomaticallySyncScene = true;

        if (PhotonNetwork.IsMasterClient)
        {
            playButton.SetActive(true); // Активируем кнопку для создателя
            playButton.GetComponent<Button>().onClick.AddListener(StartGame);
        }
        else
            playButton.SetActive(false);
        
        GameObject uiElement = PhotonNetwork.Instantiate(prefabPanelUser.name, Vector3.zero, Quaternion.identity, 0);
        uiElementID = uiElement.GetComponent<PhotonView>().ViewID;

        photonView.RPC("CreateUserPanel", RpcTarget.All, uiElementID, "User");
    }

    [PunRPC]
    private void CreateUserPanel(int viewID, string name){
        PhotonView photonView = PhotonView.Find(viewID);
        photonView.gameObject.transform.parent = panelUsers.transform;
        photonView.gameObject.GetComponentInChildren<TextMeshProUGUI>().text = name;
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

    /// <summary>
    /// Метод для загрузки уровня для всех игроков
    /// </summary>
    [PunRPC]
    private void StartGame()
    {
        if (PhotonNetwork.IsMasterClient)
            PhotonNetwork.LoadLevel("Game");
    }   
}
