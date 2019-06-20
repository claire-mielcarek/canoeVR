using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canoe : MonoBehaviour
{
    [SerializeField] public Transform canoe;
    [SerializeField] public Rigidbody rb;
    [SerializeField] public float speedTranslation;
    [SerializeField] public float speedRotation;
    [SerializeField] public float speedBuoyancy;

    private bool inWater;

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

        if(!inWater) {
            rb.AddForce(-canoe.up * speedBuoyancy, ForceMode.Acceleration);
        }
    }

    void OnTriggerStay(Collider other) {
        //Debug.Log("Trigger water");
        if(other.gameObject.CompareTag("water")) {
            inWater = true;
            buoyancy();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.CompareTag("rightSide"))
        {
            Debug.Log("Good side !");
            Ring();
        }
        else
        {
            Debug.Log("Wrong side ! :(");
        }
    }

    void OnTriggerExit(Collider other) {
        inWater = false;
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


    void Ring()
    {
        Debug.Log("Ajouter un son cool ici");
    }
}
