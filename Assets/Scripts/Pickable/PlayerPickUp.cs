using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUp : MonoBehaviour
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


    private GameObject inHandItem;
    private Transform playerCamera;
    private LayerMask pickableLayerMask = 1<<9;
    private RaycastHit hit;

    private void Start()
    {
        playerCamera = FindObjectOfType<Camera>().transform;
    }

    private void Update()
    {
        Debug.DrawRay(playerCamera.position, playerCamera.forward * hitRange, Color.red);
        if(hit.collider != null)
        {
            pickUpUI.SetActive(false);
        }
        if(inHandItem != null)
        {
            return;
        }
        if(Physics.Raycast(playerCamera.position, playerCamera.forward, 
            out hit, hitRange, pickableLayerMask))
        {
            pickUpUI.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                PickUp();
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
            if(rb != null)
            {
                rb.isKinematic = true;
            }
            return;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Drop();
        }
    }

    private void Drop()
    {
        print("can't drops");
        if(inHandItem != null)
        {
            print("drops");
            inHandItem.transform.SetParent(null);
            inHandItem = null;
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
            }
        }
    }
}
