using System.Collections;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class SpawnPlayer : MonoBehaviourPunCallbacks
{
    public GameObject waitingWindow; // Окно ожидания
    public TextMeshProUGUI timer;
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
        
        // Спавним игрока
        SpawnPlayers();
    }

    private void SpawnPlayers()
    {        
        // Проверяем, является ли текущий игрок мастером комнаты
        if (PhotonNetwork.IsMasterClient)
        {
            // Если это 1 игрок, даем возможность перемещаться
            PhotonNetwork.Instantiate(playerPrefab.name, positionSpawn.position, Quaternion.identity).AddComponent<ScriptDragDiller>();

            int _pickerObject = PhotonNetwork.Instantiate(pickerPrefab.name, pickerSpawn.position, Quaternion.identity).GetComponent<PhotonView>().ViewID;
            
            photonView.RPC("SinID", RpcTarget.All, _pickerObject);

            StartCoroutine(nameof(Timer));
        }
        else
            waitingWindow.SetActive(true);
            // Если это не первый игрок, показываем окно ожидания
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
        //меняем игрока на мента 
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
