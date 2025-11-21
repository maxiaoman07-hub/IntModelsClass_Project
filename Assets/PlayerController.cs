using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    public Rigidbody rb;
    public Transform head;
    public Camera camera;

    [Header("Configurations")]

    public float WalkSpeed;
    public float RunSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Vector3 newVelocity = Vector3.up*rb.linearVelocity.y;
        float Speed = Input.GetKey(KeyCode.LeftShift) ? RunSpeed : WalkSpeed;
        newVelocity.z = Input.GetAxis("Vertical") * Speed;    
        newVelocity.x = Input.GetAxis("Horizontal") * Speed;
        rb.linearVelocity = newVelocity;
    }
}
