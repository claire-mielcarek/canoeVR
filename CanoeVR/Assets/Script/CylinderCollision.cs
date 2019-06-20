using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderCollision : MonoBehaviour
{

    [SerializeField] private bool triggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("canoe"))
        {
            triggered = true;
        }
    }

    public bool getTriggered()
    {

        return triggered;
    }

}