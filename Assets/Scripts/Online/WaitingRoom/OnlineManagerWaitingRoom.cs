using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

[RequireComponent(typeof(PhotonView))]
public class OnlineManagerWaitingRoom : MonoBehaviourPunCallbacks, IPunObservable
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private float timer;

    private GameObject loadScenePanelUI;
    private Text timerText;
    private bool connecting;
    private GameObject thisPlayer;
    private PhotonView view;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting && PhotonNetwork.IsMasterClient)
        {
            stream.SendNext(connecting);
            stream.SendNext(timer);
        }
        else
        {
            connecting = (bool)stream.ReceiveNext();
            timer = (float)stream.ReceiveNext();
        }
    }

    void Start()
    {
        view = GetComponent<PhotonView>();
        PhotonNetwork.AutomaticallySyncScene = true;
        Vector3 pos = new Vector3(Random.Range(-10, 10), 0.2f, Random.Range(-10, 10));
        thisPlayer = PhotonNetwork.Instantiate(playerPrefab.name, pos, Quaternion.identity);
        timerText = thisPlayer.GetComponent<PlayerControllerOnline>().timerText;
        loadScenePanelUI = thisPlayer.GetComponent<PlayerControllerOnline>().loadScenePanelUI;
    }
    private void Update()
    {
        if (connecting && timer <= 0)
        {
            PhotonNetwork.LoadLevel(2);
            connecting = false;
            return;
        }
        else if (connecting && timer > 0)
        {
            timer -= Time.deltaTime;
            timerText.text = timer.ToString("F1");
            return;
        }

        if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers && PhotonNetwork.IsMasterClient)
        {
            loadScenePanelUI.SetActive(true);

            if (Input.GetKey(KeyCode.M))
            {
                connecting = true;
                view.RPC("ActivateTimer", RpcTarget.AllBuffered);
            }
        }
        else
        {
            loadScenePanelUI.SetActive(false);
            timerText.gameObject.SetActive(false);
        }
    }
    [PunRPC]
    public void ActivateTimer()
    {
        timerText.gameObject.SetActive(true);
    }
}
