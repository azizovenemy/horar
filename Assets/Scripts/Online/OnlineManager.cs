using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class OnlineManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject playerPrefab;
    void Start()
    {
        Vector3 pos = new Vector3(Random.Range(-10, 10), 0.2f, Random.Range(-10, 10));
        PhotonNetwork.Instantiate(playerPrefab.name, pos, Quaternion.identity);
    }
    void Update()
    {
        
    }
}
