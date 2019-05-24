using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canoe : MonoBehaviour
{
    void Update()
    {
        GetComponent<Rigidbody>().AddForce(Vector3.forward * 10, ForceMode.Force);
    }
}
