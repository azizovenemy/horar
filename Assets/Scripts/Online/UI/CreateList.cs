using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class CreateList : MonoBehaviourPunCallbacks
{
    [SerializeField] private ListView listView;

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (RoomInfo game in roomList)
        {
            GameObject element = listView.Add();

            ListElement listElement = element.GetComponent<ListElement>();
            listElement.SetSettings(game.Name, game.PlayerCount, game.MaxPlayers);
            listElement.connectButton.onClick.AddListener(() =>
            {
                PhotonNetwork.JoinRoom(game.Name);
            });
        }
    }
}
