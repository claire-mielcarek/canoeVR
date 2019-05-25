using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canoe : MonoBehaviour
{
    [SerializeField] private Transform canoe;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speedTranslation;
    [SerializeField] private float speedRotation;

    void Start() {
        speedTranslation = 10.0f;
        speedRotation = 5.0f;
    }

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
    }
}
