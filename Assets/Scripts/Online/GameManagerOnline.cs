using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManagerOnline : MonoBehaviourPunCallbacks//, IPunObservable
{
    [SerializeField] private List<GameObject> pickableItems;
    [SerializeField] private List<Vector3> coordPickableItems;
    [SerializeField] private List<Vector3> latestCoordsItems;
    private bool changePosItems;

    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            foreach (GameObject item in pickableItems)
            {
                Vector3 pos = coordPickableItems[Random.Range(0, coordPickableItems.Count)];
                PhotonNetwork.Instantiate(item.name, pos, Quaternion.identity);
                latestCoordsItems.Add(pos);
            }
        }
    }
    private void Update()
    {
        try
        {
            for (int i = 0; i < latestCoordsItems.Count; i++)
            {
                if (latestCoordsItems[i] != pickableItems[i].transform.position)
                {
                    changePosItems = true;
                }
            }
        }
        catch
        {
            print("вы не являетесь мастерклиентом");
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (changePosItems)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(pickableItems);

                foreach (GameObject item in pickableItems)
                {
                    stream.SendNext(item.transform.position);
                    stream.SendNext(item.transform.rotation);
                }
            }
            else
            {
                List<GameObject> allPickableItems = new List<GameObject> { (GameObject)stream.ReceiveNext() };
                print(allPickableItems);

                foreach (GameObject item in allPickableItems)
                {
                    item.transform.position = (Vector3)stream.ReceiveNext();
                    item.transform.rotation = (Quaternion)stream.ReceiveNext();
                }
            }

            changePosItems = false;
        }
    }

}
