using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListElement : MonoBehaviour
{
    public Button connectButton;

    [SerializeField] RectTransform pos;
    [SerializeField] Text nameRoom;
    [SerializeField] Text players;

    public void SetSettings(string name, int nowPlayers, int maxPlayers)
    {
        nameRoom.text = name;
        players.text = (nowPlayers + "/" + maxPlayers).ToString();
    }
}
