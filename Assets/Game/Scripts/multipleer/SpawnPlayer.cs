using System.Collections;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class SpawnPlayer : MonoBehaviourPunCallbacks
{
    public GameObject waitingWindow; // Окно ожидания
    public GameObject winPanel; // Окно победы
    public GameObject lousePanel; // Окно проигрыша


    public GameObject buttonRestart;

    public TextMeshProUGUI timer;

    public TextMeshProUGUI cosacPic;
    public TextMeshProUGUI cosacInv;

    public GameObject playerPrefab; // Префаб игрока
    public GameObject pickerPrefab; // Префаб игрока

    public Transform positionSpawn;
    public Transform pickerSpawn;
    public float timersTime = 60;

    public Transform _pickerObject;
    [SerializeField] protected float _dropImpulse = 5f;

    private void Start()
    {
        // Скрываем окно ожидания при старте
        waitingWindow.SetActive(false);
        lousePanel.SetActive(false);
        winPanel.SetActive(false);
        cosacInv.gameObject.SetActive(false);
        cosacPic.gameObject.SetActive(false);
        buttonRestart.SetActive(false);

        // Спавним игрока
        SpawnPlayers();
    }

    private void SpawnPlayers()
    {        
        // Проверяем, является ли текущий игрок мастером комнаты
        if (PhotonNetwork.IsMasterClient)
        {
            cosacInv.gameObject.SetActive(true);
            // Если это 1 игрок, даем возможность перемещаться
            PhotonNetwork.Instantiate(playerPrefab.name, positionSpawn.position, Quaternion.identity).AddComponent<ScriptDragDiller>();

            int _pickerObject = PhotonNetwork.Instantiate(pickerPrefab.name, pickerSpawn.position, Quaternion.identity).GetComponent<PhotonView>().ViewID;
            
            photonView.RPC("SinID", RpcTarget.All, _pickerObject);

            StartCoroutine(nameof(Timer));
        }
        else{
            cosacPic.gameObject.SetActive(true);
            waitingWindow.SetActive(true);
        }
            // Если это не первый игрок, показываем окно ожидания
    }
    [PunRPC]
    public void Restart()
    {
        if (PhotonNetwork.IsMasterClient)
            PhotonNetwork.LoadLevel("Game");
    }   

    [PunRPC]
    public void SinID(int id)
    {
        _pickerObject = PhotonView.Find(id).transform;
    }
    /// <summary>
    /// спавн мента
    /// </summary>
    [PunRPC]
    public void SelectPlayerOne()
    {
        if (!PhotonNetwork.IsMasterClient){
            waitingWindow.SetActive(false);
            GameObject ob = PhotonNetwork.Instantiate(playerPrefab.name, positionSpawn.position, Quaternion.identity).AddComponent<ScriptPoliceMan>().gameObject;
        }
        //StartCoroutine(nameof(Timer));

        // спавн мента
    }

    private IEnumerator Timer(){
        int time = 0;

        while(true){

            if(time > timersTime) break;

            time++;
            photonView.RPC("UpdateText", RpcTarget.All, time.ToString());

            yield return new WaitForSeconds(1);
        }

        photonView.RPC("SelectPlayerOne", RpcTarget.All);
        StartCoroutine(nameof(Timer2));
        //меняем игрока на мента 
    }

    private IEnumerator Timer2(){
        int time = 0;

        while(true){

            if(time > timersTime) break;

            time++;
            photonView.RPC("UpdateText", RpcTarget.All, time.ToString());

            yield return new WaitForSeconds(1);
        }

        photonView.RPC("PanelUp", RpcTarget.All);
        //меняем игрока на мента 
    }

    [PunRPC]
    private void PanelUp(){
        if (!PhotonNetwork.IsMasterClient){
            lousePanel.SetActive(true);
        }
        else{
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;

            buttonRestart.SetActive(true);

            winPanel.SetActive(true);
        }
    }
    
    [PunRPC]
    private void PanelDis(){
        if (PhotonNetwork.IsMasterClient){
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;

            buttonRestart.SetActive(true);

            lousePanel.SetActive(true);
        }
        else{
            winPanel.SetActive(true);
        }
    }

    [PunRPC]
    public void UpdateText(string newText)
    {
        timer.text = $"{newText}/{timersTime}";
    }

    [PunRPC]
    public void GetItem(int id)
    {
        _pickerObject.GetComponent<Rigidbody>().isKinematic = true;
        _pickerObject.SetParent(PhotonView.Find(id).transform);
        _pickerObject.localRotation = Quaternion.Euler(0, 0, 0);
    }

    [PunRPC]
    public void DropItem()
    {
        _pickerObject.parent = null;
        _pickerObject.GetComponent<Rigidbody>().isKinematic = false;
        _pickerObject.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * _dropImpulse, ForceMode.Impulse);
    }
}
