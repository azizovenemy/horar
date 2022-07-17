using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class CreateLobby : MonoBehaviourPunCallbacks
{
    bool connect;
    void Start()
    {   
        PhotonNetwork.ConnectUsingSettings();
        if (PhotonNetwork.IsConnected)
            connect = true;
    }
    void Update()
    {
        
    }
    public void ConnectRandom()
    {

    }
    public override void OnConnectedToMaster()
    {
        
    }
}
