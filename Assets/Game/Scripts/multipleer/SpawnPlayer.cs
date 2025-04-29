using System.Collections;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class SpawnPlayer : MonoBehaviourPunCallbacks
{
    public GameObject waitingWindow; // Окно ожидания
    public TextMeshProUGUI timer;
    public GameObject playerPrefab; // Префаб игрока
    public Transform positionSpawn;
    public float timersTime = 60;

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
            PhotonNetwork.Instantiate(playerPrefab.name, positionSpawn.position, Quaternion.identity);
            StartCoroutine(nameof(Timer));
        }
        else
        {
            // Если это не первый игрок, показываем окно ожидания
            waitingWindow.SetActive(true);
        }
    }

    /// <summary>
    /// спавн мента
    /// </summary>
    [PunRPC]
    public void SelectPlayerOne()
    {
        if (!PhotonNetwork.IsMasterClient){
            waitingWindow.SetActive(false);
            PhotonNetwork.Instantiate(playerPrefab.name, positionSpawn.position, Quaternion.identity);
        }
            // Логика для того, чтобы второй игрок мог начать игру
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
}
