using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private float dirX;
    private float moveSpeed;
    private Rigidbody rb;
    void Start()
    {
        
        rb = GetComponent<Rigidbody>();
        dirX = -1f;
        moveSpeed = 2.5f;
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("wall"))
        {
            dirX *= -1f;
            if (transform.rotation.y > 0)
                transform.Rotate(0, 180 * dirX, 0, Space.World);
        }
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector3(dirX * moveSpeed, rb.velocity.y, rb.velocity.z);
    }
}