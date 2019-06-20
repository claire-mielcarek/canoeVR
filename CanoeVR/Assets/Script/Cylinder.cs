using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cylinder : MonoBehaviour
{

    [SerializeField] public bool goodSide;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(goodSide + " " + other.gameObject.tag);
        if (goodSide)
        {
            Debug.Log("Good side !");
            Ring();
        }
        else
        {
            Debug.Log("Wrong side ! :(");
        }
    }

    void OnTriggerStay(Collider other)
    {
        Debug.Log(goodSide + " " + other.gameObject.tag);
        if (goodSide)
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