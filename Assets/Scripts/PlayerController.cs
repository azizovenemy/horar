using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Space]
    [SerializeField] private float sensitivity;
    [SerializeField] private float jumpForce;
    
    private Vector3 playerMovementInput;
    private Vector2 playerMouseInput;
    private Rigidbody playerBody;
    private float rotation;
    private Transform playerCamera;
    
    public float speed;

    private void Start()
    {
        playerBody = GetComponent<Rigidbody>();
        playerCamera = FindObjectOfType<Camera>().transform;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        playerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        playerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        
        MovePlayer();
        MovePlayerCamera();
    }

    private void MovePlayer()
    {
        Vector3 MoveVector = transform.TransformDirection(playerMovementInput) * speed;
        playerBody.velocity = new Vector3(MoveVector.x, playerBody.velocity.y, MoveVector.z);
    }

    private void MovePlayerCamera()
    {
        rotation -= playerMouseInput.y * sensitivity;
        if (rotation > 85f) rotation = 85f;
        if (rotation < -60f) rotation = -60f;

        playerCamera.transform.localRotation = Quaternion.Euler(rotation, 0f, 0f);
        transform.Rotate(0f, playerMouseInput.x * sensitivity, 0f);
    }
}
