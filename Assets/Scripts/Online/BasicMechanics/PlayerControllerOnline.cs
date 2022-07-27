using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerOnline : MonoBehaviourPunCallbacks
{
    [Space]
    [SerializeField] private float sensitivity;
    [SerializeField] private float jumpForce;
    [SerializeField] private Camera cam;

    private Vector3 playerMovementInput;
    private Vector2 playerMouseInput;
    private Rigidbody playerBody;
    private float rotation;
    private PhotonView view;

    public float speed;

    private void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        view = GetComponent<PhotonView>();
        playerBody = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        if (!view.IsMine)
        {
            cam.enabled = false;
        }
    }

    private void Update()
    {
        if (view.IsMine)
        {
            playerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            playerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

            MovePlayer();
            MovePlayerCamera();
        }
    }

    private void MovePlayer()
    {
        Vector3 MoveVector = transform.TransformDirection(playerMovementInput) * speed;
        playerBody.velocity = new Vector3(MoveVector.x, playerBody.velocity.y, MoveVector.z);
    }

    private void MovePlayerCamera()
    {
        if (view.IsMine)
        {
            rotation -= playerMouseInput.y * sensitivity;
            if (rotation > 85f) rotation = 85f;
            if (rotation < -60f) rotation = -60f;

            cam.transform.localRotation = Quaternion.Euler(rotation, 0f, 0f);
            transform.Rotate(0f, playerMouseInput.x * sensitivity, 0f);
        }
    }
}
