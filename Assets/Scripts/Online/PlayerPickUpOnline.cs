using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerPickUpOnline : MonoBehaviourPunCallbacks
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
        if (view.IsMine)
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
                    Drop();
                }
            }
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward,
                out hit, hitRange, pickableLayerMask))
            {
                pickUpUI.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    PickUp();
                }
            }
        }
    }

    private void PickUp()
    {
        Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
        if (hit.collider.GetComponent<Item>())
        {
            inHandItem = hit.collider.gameObject;
            inHandItem.transform.position = Vector3.zero;
            inHandItem.transform.rotation = Quaternion.identity;
            inHandItem.transform.SetParent(pickUpParent, false);
            hit.collider.enabled = false;
            if (rb != null)
            {
                rb.isKinematic = true;
            }
            GetComponent<PlayerControllerOnline>().speed = 1;
        }
    }

    private void Drop()
    {
        inHandItem.transform.SetParent(null);
        Rigidbody rb = inHandItem.GetComponent<Rigidbody>();
        inHandItem.GetComponent<Collider>().enabled = true;
        if (rb != null)
        {
            rb.isKinematic = false;
        }
        inHandItem = null;
        GetComponent<PlayerControllerOnline>().speed = 2;
    }
}
