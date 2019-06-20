using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderSound : MonoBehaviour
{

    [SerializeField] private bool triggered = false;
    private AudioSource rightSideSound;
    private AudioSource wrongSideSound;
    private CylinderCollision leftCollision;
    private CylinderCollision rightCollision;

    private void Awake()
    {
        GameObject sounds = GameObject.Find("Sounds");
        leftCollision = transform.GetChild(0).gameObject.GetComponent<CylinderCollision>();
        rightCollision = transform.GetChild(1).gameObject.GetComponent<CylinderCollision>();

        if (sounds)
        {
            rightSideSound = sounds.transform.Find("rightSideSound").GetComponentInChildren<AudioSource>();
            wrongSideSound = sounds.transform.Find("wrongSideSound").GetComponentInChildren<AudioSource>();
        }
    }


    private void Update()
    {
        if (!triggered && leftCollision.getTriggered())
        {
            Debug.Log("triggered left");
            Ring(true);
            triggered = true;
        }
        else if(!triggered && rightCollision.getTriggered())
        {
            Debug.Log("triggered right");
            Ring(false);
            triggered = true;
        }
    }

    void Ring(bool rightSound)
    {
        if (rightSound)
        {
            rightSideSound.Play();
        }
        else
        {
            wrongSideSound.Play();
        }
    }
}
