using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrailer : MonoBehaviour
{
    [SerializeField] private Transform camTransform;
    
    public int moveIndex = 0;
    public float moveSpeed = 0;
    public float turnSpeed = 1f;
    public Transform[] movePoints;
    
    void Update()
    {
        Vector3 pointDirection = movePoints[moveIndex].position - transform.position;
        float singleStep = turnSpeed * Time.deltaTime;
        
        Vector3 offsetDirection = (movePoints[moveIndex].position - camTransform.position).normalized;
        Vector3 lookDirection = Vector3.RotateTowards(camTransform.forward, offsetDirection, singleStep * 2, 0.0f); 
        camTransform.rotation = Quaternion.LookRotation(lookDirection);

        camTransform.position = Vector3.MoveTowards(camTransform.position,movePoints[moveIndex].position,moveSpeed*Time.deltaTime);
        
        if (Vector3.Distance(camTransform.position, movePoints[moveIndex].position) <= 0.001f && moveIndex<movePoints.Length)
        {
            if (moveIndex <= movePoints.Length - 2)
                moveIndex += 1;
            else
                moveIndex = 0;
        }
    }
}
