using Photon.Pun;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject waitingWindow; // Окно ожидания
    public GameObject playerPrefab; // Префаб игрока
    public Transform positionSpawn;
    private bool isPlayerOneSelected = false; // Флаг выбора первого игрока

    void Start()
    {
        // Скрываем окно ожидания при старте
        waitingWindow.SetActive(false);
        
        // Спавним игрока
        SpawnPlayers();
    }

    private void SpawnPlayers()
    {
        // Спавним игрока на позиции (0, 0, 0) или другой заданной позиции
        Vector3 spawnPosition = positionSpawn.position;
        GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, spawnPosition, Quaternion.identity);
        
        // Проверяем, является ли текущий игрок мастером комнаты
        if (PhotonNetwork.IsMasterClient)
        {
            // Если это первый игрок, показываем окно ожидания
            isPlayerOneSelected = true;
            waitingWindow.SetActive(true);
        }
        else
        {
            // Если это второй игрок, даем возможность перемещаться
            //player.GetComponent<PlayerMovement>().enabled = true; // Включаем движение
        }
    }

    public void SelectPlayerOne()
    {
        if (isPlayerOneSelected)
        {
            // Логика для того, чтобы второй игрок мог начать игру
            waitingWindow.SetActive(false);
            // Здесь можно добавить логику для начала игры
        }
    }
}
