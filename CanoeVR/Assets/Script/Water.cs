using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] private GameObject canoe;
    [SerializeField] private GameObject hand;

    private Vector3 firstPos;
    private Vector3 secondPos;

    //private bool forwardMovement = false;
    //private bool backwardMovement = false;

    /*void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("oar"))
        {
            Debug.Log("movement 1");
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
            Debug.Log("Paddling");
            GameObject paddle = other.gameObject;
            AudioSource paddlingSound = paddle.transform.Find("PaddlingSound").GetComponentInChildren<AudioSource>();
            paddlingSound.Play();
            if (paddlingSound.time > 0.9f)
            {
                paddlingSound.Stop();
            }
            firstPos = hand.transform.localPosition;
            StartCoroutine(waiter());
        }
    }

    /*void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("oar"))
        {
            secondPos = hand.transform.localPosition;

            float distanceZ = firstPos.z - secondPos.z;
            float distanceX = -firstPos.x;
            canoe.GetComponent<Canoe>().move(distanceZ, distanceX);
        }
    }*/

    void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("oar"))
        {
            /*secondPos = hand.transform.localPosition;

            float distanceZ = firstPos.z - secondPos.z;
            float distanceX = -firstPos.x;
            canoe.GetComponent<Canoe>().move(distanceZ, distanceX);*/
        }
    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(0.5f);

        secondPos = hand.transform.localPosition;

        float distanceZ = firstPos.z - secondPos.z;
        float distanceX = -firstPos.x;
        canoe.GetComponent<Canoe>().move(distanceZ, distanceX);
    }
}
