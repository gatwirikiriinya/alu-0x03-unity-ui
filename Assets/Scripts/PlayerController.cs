using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 40f;
    private Rigidbody rb;

    private float horizontal;
    private float vertical;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void FixedUpdate()
    {
        PlayerMovements();
    }


    public void PlayerMovements()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        
        rb.AddForce(new Vector3(horizontal, 0, vertical) * (speed * Time.deltaTime), ForceMode.Acceleration);
    }
}
