using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField, Range(1, 100)] float UserSpeed = 12f;

    public bool isGrounded;
    public float gravity = -9.81f;
    public float groundDistance = 0.4f;
    private float x;
    private float z;
    
    public Transform groundCheck;
    public CharacterController controller;
    
    
    public LayerMask groundMask;

    private float speed;
    private Vector3 velocity;
    
    private GameManager gameManager;
    public GameObject manager;

    private void Start()
    {
        gameManager = manager.GetComponent<GameManager>();
        speed = UserSpeed;
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (velocity.magnitude != 0)
        {
            gameManager.counter = 30f;
        }
        
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

   

    

    
}
