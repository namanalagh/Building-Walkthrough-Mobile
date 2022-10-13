using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    private float mouseX;
    private float mouseY;
    
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    private GameManager gameManager;
    public GameObject manager;
    
    private float xRotation = 0f;
    void Start()
    {
        gameManager = manager.GetComponent<GameManager>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        playerBody.Rotate(Vector3.up * mouseX);

        if (mouseX != 0 || mouseY != 0)
        {
            gameManager.counter = 30f;
        }
    }
}
