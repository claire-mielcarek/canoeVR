using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] private GameObject canoe;
    [SerializeField] private GameObject hand;
    [SerializeField] private GameObject oar;

    private Vector3 firstPos;
    private Vector3 secondPos;

    //private bool forwardMovement = false;
    //private bool backwardMovement = false;

    //private bool paddling = false;

    /*void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("oar"))
        {
            if (hand.transform.localPosition.z * 10 > 0)
            {
                forwardMovement = true;
            } else {
                backwardMovement = true;
            }
        }
    }*/

    /*void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("oar")) {
            if (hand.transform.localPosition.x * 10 < 0 && hand.transform.localPosition.z * 10 < 0 && forwardMovement) {
                canoe.GetComponent<Canoe>().moveTopRight();
                forwardMovement = false;
            } else if (hand.transform.localPosition.x * 10 > 0 && hand.transform.localPosition.z * 10 < 0 && forwardMovement) {
                canoe.GetComponent<Canoe>().moveTopLeft();
                forwardMovement = false;
            } else if (hand.transform.localPosition.x * 10 < 0 && hand.transform.localPosition.z * 10 > 0 && backwardMovement) {
                canoe.GetComponent<Canoe>().moveBotRight();
                backwardMovement = false;
            } else if (hand.transform.localPosition.x * 10 > 0 && hand.transform.localPosition.z * 10 > 0 && backwardMovement) {
                canoe.GetComponent<Canoe>().moveBotLeft();
                backwardMovement = false;
            }
        }
    }*/

    /*void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("oar"))
        {
            forwardMovement = false;
            backwardMovement = false;
        }
    }*/

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("oar"))
        {
            //firstPos = oar.transform;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                Debug.Log("Point of contact: "+ hit.point);
            }
            firstPos = hit.point;
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("oar"))
        {
            /*secondPos = oar.transform;
            float distanceY = firstPos.transform.position.y - secondPos.transform.position.y;
            float distanceX = firstPos.transform.position.x - secondPos.transform.position.x;
            canoe.GetComponent<Canoe>().move(distanceY, distanceX);*/

            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                Debug.Log("Point of contact: "+ hit.point);
            }
            secondPos = hit.point;

            float distanceY = firstPos.y - secondPos.y;
            float distanceX = -firstPos.x;
            canoe.GetComponent<Canoe>().move(distanceY, distanceX);
        }
    }
}
