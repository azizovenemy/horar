using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using Photon.Pun;

public class PlayerPickUpOnline : MonoBehaviourPunCallbacks//, IPunObservable
{
    [SerializeField]
    private GameObject pickUpUI;

    [SerializeField]
    private Transform pickUpParent;

    [SerializeField]
    [Min(1)]
    private float hitRange = 2;

    [Space]
    [SerializeField]
    private AudioSource pickUpAudio;

    [SerializeField]
    private Camera playerCamera;

    private GameObject inHandItem;
    private LayerMask pickableLayerMask = 1 << 9;
    private RaycastHit hit;
    private PhotonView view;

    private void Start()
    {
        view = GetComponent<PhotonView>();
    }

    private void Update()
    {
        Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * hitRange, Color.red);
        if (hit.collider != null)
        {
            pickUpUI.SetActive(false);
        }
        if (inHandItem != null)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                view.RPC("Drop", RpcTarget.AllBuffered, inHandItem.GetComponent<PhotonView>().ViewID);
            }
        }
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward,
            out hit, hitRange, pickableLayerMask))
        {
            pickUpUI.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                inHandItem = hit.collider.gameObject;
                view.RPC("PickUp", RpcTarget.AllBuffered, inHandItem.GetComponent<PhotonView>().ViewID, view.ViewID);
            }
        }
    }

    [PunRPC]
    private void PickUp(int itemID, int playerId)
    {
        if (view.IsMine)
        {
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
            if (hit.collider.GetComponent<Item>())
            {

                inHandItem.transform.parent = pickUpParent;
                inHandItem.transform.localPosition = Vector3.zero;
                inHandItem.transform.localRotation = Quaternion.identity;
                hit.collider.enabled = false;
                if (rb != null)
                {
                    rb.isKinematic = true;
                }
                GetComponent<PlayerControllerOnline>().speed /= 2;
            }
        }
        else
        {
            GameObject item = PhotonView.Find(itemID).gameObject;
            GameObject parent = PhotonView.Find(playerId).transform.GetChild(1).GetChild(0).gameObject;
            item.GetComponent<Collider>().enabled = false;
            item.GetComponent<Rigidbody>().isKinematic = true;
            item.transform.parent = parent.transform;
            item.transform.localPosition = Vector3.zero;
            item.transform.localRotation = Quaternion.identity;
        }
    }

    [PunRPC]
    private void Drop(int itemID)
    {
        if (view.IsMine)
        {
            inHandItem.transform.parent = null;
            Rigidbody rb = inHandItem.GetComponent<Rigidbody>();
            inHandItem.GetComponent<Collider>().enabled = true;
            if (rb != null)
            {
                rb.isKinematic = false;
            }
            inHandItem = null;
            GetComponent<PlayerControllerOnline>().speed *= 2;
        }
        else
        {
            GameObject item = PhotonView.Find(itemID).gameObject;
            item.transform.parent = null;
            item.GetComponent<Collider>().enabled = true;
            item.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
