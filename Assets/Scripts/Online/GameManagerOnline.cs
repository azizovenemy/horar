using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManagerOnline : MonoBehaviourPunCallbacks
{
    [SerializeField] List<GameObject> pickableItems;
    [SerializeField] List<Vector3> coordPickableItems;

    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            foreach (GameObject item in pickableItems)
            {
                PhotonNetwork.Instantiate(item.name, coordPickableItems[Random.Range(0, coordPickableItems.Count)], Quaternion.identity);
            }
        }
    }
}
