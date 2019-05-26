using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] private GameObject canoe;
    [SerializeField] private GameObject hand;

    private bool movement1 = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("oar"))
        {
            if (hand.transform.localPosition.z * 10 > 0)
            {
                movement1 = true;
                Debug.Log("movement1");
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("oar"))
        {
            if (hand.transform.localPosition.x * 10 < 0 && hand.transform.localPosition.z * 10 < 0 && movement1)
            {
                canoe.GetComponent<Canoe>().moveTopRight();
                movement1 = false;
                Debug.Log("Droite");
            }
            else if (hand.transform.localPosition.x * 10 > 0 && hand.transform.localPosition.z * 10 < 0 && movement1)
            {
                canoe.GetComponent<Canoe>().moveTopLeft();
                movement1 = false;
                Debug.Log("Gauche");
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("oar"))
        {
            movement1 = false;
        }
    }
}
