using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class OnlineManagerWaitingRoom : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject playerPrefab;

    [SerializeField] private int playersCount = 1;
    void Start()
    {
        Vector3 pos = new Vector3(Random.Range(-10, 10), 0.2f, Random.Range(-10, 10));
        PhotonNetwork.Instantiate(playerPrefab.name, pos, Quaternion.identity);
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        print("+");
        playersCount++;
        if (PhotonNetwork.IsMasterClient && playersCount >= 2) PhotonNetwork.LoadLevel(2);
    }
}
