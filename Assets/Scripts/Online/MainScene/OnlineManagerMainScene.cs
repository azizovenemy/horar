using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class OnlineManagerMainScene : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject playerPrefab;
    void Start()
    {
        Vector3 pos = new Vector3(Random.Range(-4, 4), 0.5f, Random.Range(-4, 4));
        PhotonNetwork.Instantiate(playerPrefab.name, pos, Quaternion.identity);
    }
    void Update()
    {
        
    }
}
