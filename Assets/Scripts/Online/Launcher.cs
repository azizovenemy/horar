using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{
    [SerializeField] private InputField roomNameConnecting;
    [SerializeField] private InputField roomNameCreating;
    [SerializeField] private Text errorText;        //ошибка когда что-то с фотоном (в отдельной панели)
    [SerializeField] private Text errorCreating;    //не введено название комнаты при создании
    [SerializeField] private Text errorConnecting;  //не введено название комнаты при подключении
    [SerializeField] private int maxPlayers;

    private TypedLobby customLobby = new TypedLobby("customLobby", LobbyType.Default);
    void Start()
    {
        MenuManager.instance.OpenMenuPanel("loading");
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(customLobby);
    }
    public override void OnJoinedLobby()
    {
        MenuManager.instance.OpenMenuPanel("choose");
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        MenuManager.instance.OpenMenuPanel("error");
        errorText.text = cause.ToString();
    }
    public void ConnectRoom()
    {
        if (roomNameConnecting.text != "")
        {
            PhotonNetwork.JoinRoom(roomNameConnecting.text);
        }
        else
            errorConnecting.text = "Please enter the name of the room";
    }
    public void ConnectRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();
    }
    public void CreateRoomConnecting()
    {
        MenuManager.instance.OpenMenuPanel("creating");
    }
    public void CreateRoom()
    {
        if (roomNameCreating.text != "")
        {
            PhotonNetwork.CreateRoom(roomNameCreating.text, new RoomOptions { MaxPlayers = (byte)maxPlayers });
            roomNameCreating.text = "";
        }
        else
            errorCreating.text = "Please enter the name of the room";
    }
    public void EditInput()
    {
        errorConnecting.text = "";
        errorCreating.text = "";
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        MenuManager.instance.OpenMenuPanel("error");
        errorText.text = message;
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        MenuManager.instance.OpenMenuPanel("error");
        errorText.text = message;
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        MenuManager.instance.OpenMenuPanel("error");
        errorText.text = message;
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(1);
    }
}
