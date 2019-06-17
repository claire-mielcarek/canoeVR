using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cylinder : MonoBehaviour
{

    [SerializeField] private string side;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(side))
        {
            Debug.Log("Good side !");
            Ring();
        }
        else
        {
            Debug.Log("Wrong side ! :(");
        }
    }

    void Ring()
    {
        Debug.Log("Ajouter un son cool ici");
    }
}