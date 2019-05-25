﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canoe : MonoBehaviour
{
    [SerializeField] private Transform canoe;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speedTranslation;
    [SerializeField] private float speedRotation;
    [SerializeField] private float speedBuoyancy;

    void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.K)) {
            rb.AddForce(canoe.forward * speedTranslation, ForceMode.Acceleration);
            rb.AddForce(-canoe.right * speedRotation, ForceMode.Acceleration);
            rb.AddTorque(-canoe.up * speedRotation, ForceMode.Acceleration);
        }

        if(Input.GetKeyDown(KeyCode.M)) {
            rb.AddForce(canoe.forward * speedTranslation, ForceMode.Acceleration);
            rb.AddForce(canoe.right * speedRotation, ForceMode.Acceleration);
            rb.AddTorque(canoe.up * speedRotation, ForceMode.Acceleration);
        }

        if(Input.GetKeyDown(KeyCode.O)) {
            rb.AddForce(canoe.up * speedBuoyancy, ForceMode.Acceleration);
        }
    }

    void OnTriggerStay(Collider other) {
        Debug.Log("Trigger water");
        if(other.gameObject.CompareTag("water")) {
            buoyancy();
        }
    }

    public void buoyancy() {
        rb.AddForce(canoe.up * speedBuoyancy, ForceMode.Acceleration);
    }

    public void moveTopRight() {
        rb.AddForce(canoe.forward * speedTranslation, ForceMode.Acceleration);
        rb.AddForce(canoe.right * speedRotation, ForceMode.Acceleration);
        rb.AddTorque(canoe.up * speedRotation, ForceMode.Acceleration);
    }

    public void moveTopLeft() {
        rb.AddForce(canoe.forward * speedTranslation, ForceMode.Acceleration);
        rb.AddForce(-canoe.right * speedRotation, ForceMode.Acceleration);
        rb.AddTorque(-canoe.up * speedRotation, ForceMode.Acceleration);
    }

    public void moveBotRight() {
        rb.AddForce(-canoe.forward * speedTranslation, ForceMode.Acceleration);
        rb.AddForce(canoe.right * speedRotation, ForceMode.Acceleration);
        rb.AddTorque(-canoe.up * speedRotation, ForceMode.Acceleration);
    }

    public void moveBotLeft() {
        rb.AddForce(-canoe.forward * speedTranslation, ForceMode.Acceleration);
        rb.AddForce(-canoe.right * speedRotation, ForceMode.Acceleration);
        rb.AddTorque(canoe.up * speedRotation, ForceMode.Acceleration);
    }
}
