using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float lookSpeed;
    
    [SerializeField] private Transform cameraTransform;
    
    private float hAxis;
    private float vAxis;
    private float hLook;
    private float vLook;
    private float cameraPitch;

    private CharacterController characterController;
    private GameManager gameManager;

    public Vector3 movement;
    public Vector3 rotate;

    public GameObject manager;
    
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        gameManager = manager.GetComponent<GameManager>();
    }

    void Update()
    {
        hAxis = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime; // Left Analog [Horizontal ONLY] for both controllers [X-Axis] [Left-Right Movement] [Works As Intended]
        vAxis = Input.GetAxis("Mouse Y") * -moveSpeed * Time.deltaTime;  // Bound to triggers in F310, works as Left Analog [Vertical ONLY] in DualShock 4 [3rd Axis] [Forwards-Backwards Movement] [Works As Intended]
        hLook = Input.GetAxis("Mouse X") * lookSpeed * Time.deltaTime;  // Right Analog for both controllers [4th Axis(I think?)] [Left-Right Rotation] [Works As Intended]
        vLook = Input.GetAxis("Vertical") * lookSpeed * Time.deltaTime; // Bound to D-Pad [Vertical ONLY] [INVERTED] in F310, works as Right Analog [Vertical ONLY] in DualShock 4 [7th Axis] [Up-Down Rotation] [Works As Intended]

        movement = new Vector3 (-hAxis, 0, -vAxis);
        rotate = new Vector3(-hLook, 0, -vLook);
        
        cameraPitch = Mathf.Clamp(cameraPitch + vLook, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(cameraPitch, 0, 0);
        
        characterController.Move(transform.forward * vAxis + transform.right * hAxis);
        transform.Rotate(transform.up * -hLook);

        if (Mathf.Abs(Input.GetAxis("Horizontal"))>.1f || Mathf.Abs(Input.GetAxis("Vertical"))>.1f || Mathf.Abs(Input.GetAxis("Mouse X"))>.1f || Mathf.Abs(Input.GetAxis("Mouse Y"))>.1f)
        {
            gameManager.counter = 20;
        }
    }
}
