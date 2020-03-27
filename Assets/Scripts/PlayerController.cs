﻿using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject projectile;

    GameManager gameManager;
    Rigidbody2D rig;

    float rotationSpeed = 200.0f;
    float engineForce = 2f;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        rig = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleMoving();
        HandleShooting();
        HandleTeleport();
    }

    private void HandleMoving()
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            rig.AddForce(transform.up * engineForce);
        }

        transform.Rotate(0, 0, -Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime);
    }

    private void HandleTeleport()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            var newPos = gameManager.GetRandomPosition();                        
            transform.position = newPos;
            rig.velocity = Vector2.zero;
            rig.angularVelocity = 0f;
        }
    }

    private void HandleShooting()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(projectile, transform.position, transform.rotation);
        }
    }
}
