﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canoe : MonoBehaviour
{
    [SerializeField] private Transform canoe;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speedTranslationZ;
    [SerializeField] private float speedTranslationX;
    [SerializeField] private float speedRotationY;
    [SerializeField] private float speedBuoyancy;

    [SerializeField] private bool inWater;

    Object[] woodSounds;
    public AudioSource audioBoat;
    private AudioSource rightSideSound;
    private AudioSource wrongSideSound;

    private void Awake()
    {
        woodSounds = Resources.LoadAll("WoodSounds", typeof(AudioClip));
        audioBoat = GetComponent<AudioSource>();
        audioBoat.clip = woodSounds[0] as AudioClip;
        GameObject sounds = GameObject.Find("Sounds");
        if (sounds)
        {
            rightSideSound = sounds.transform.Find("rightSideSound").GetComponentInChildren<AudioSource>();
            wrongSideSound = sounds.transform.Find("wrongSideSound").GetComponentInChildren<AudioSource>();
        }


    }

    private void Start()
    {
        InvokeRepeating("PlayRandomWoodSound", 0.0f, 8.0f);
    }

    void PlayRandomWoodSound()
    {
        if (!audioBoat.isPlaying)
        {
            audioBoat.clip = woodSounds[Random.Range(0, woodSounds.Length)] as AudioClip;
            audioBoat.volume = 0.01f;
            audioBoat.Play();
        }
    }

    void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            moveTopLeft();
        }

        if(Input.GetKeyDown(KeyCode.M)) {
            moveTopRight();
        }

        if(Input.GetKeyDown(KeyCode.O)) {
            rb.AddForce(canoe.up * speedBuoyancy, ForceMode.Acceleration);
        }

        if(!inWater) {
            rb.AddForce(-canoe.up * speedBuoyancy, ForceMode.Acceleration);
            //Debug.Log("Down");
        } else {
            rb.AddForce(canoe.up * speedBuoyancy, ForceMode.Acceleration);
            //Debug.Log("Up");
        }
    }

    void Update()
    {
        if (transform.eulerAngles.x != 0 || transform.eulerAngles.z != 0)
        {
            //Debug.Log("x = " + transform.eulerAngles.x);
            //Debug.Log("z = " + transform.eulerAngles.z);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }
    }

    void OnTriggerStay(Collider other) {
        if(other.gameObject.CompareTag("water")) {
            inWater = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("rightSide"))
        {
            Debug.Log("Good side !");
            Ring();
        }
        else if (other.gameObject.CompareTag("wrongSide"))
        {
            Debug.Log("Wrong side ! :(");
            wrongSideSound.Play();
        }
    }

    void OnTriggerExit(Collider other) {
        inWater = false;
    }

    public void buoyancy() {
        rb.AddForce(canoe.up * speedBuoyancy, ForceMode.Acceleration);
    }

    public void move(float translationZ, float translationX) {
        speedTranslationZ = translationZ * 300;
        //Debug.Log("translation " + speedTranslationZ);
        speedRotationY = translationX * 30;
        //Debug.Log("rotation " + speedRotationY);

        rb.AddForce(Vector3.zero, ForceMode.Acceleration);
        rb.AddTorque(Vector3.zero, ForceMode.Acceleration);

        rb.AddForce(canoe.forward * speedTranslationZ, ForceMode.Acceleration);
        rb.AddTorque(canoe.up * speedRotationY, ForceMode.Acceleration);
    }

    public void moveTopRight() {
        rb.AddForce(Vector3.zero, ForceMode.Acceleration);
        rb.AddTorque(Vector3.zero, ForceMode.Acceleration);

        rb.AddForce(canoe.forward * speedTranslationZ, ForceMode.Acceleration);
        rb.AddTorque(canoe.up * speedRotationY, ForceMode.Acceleration);
    }

    public void moveTopLeft() {
        rb.AddForce(Vector3.zero, ForceMode.Acceleration);
        rb.AddTorque(Vector3.zero, ForceMode.Acceleration);

        rb.AddForce(canoe.forward * speedTranslationZ, ForceMode.Acceleration);
        rb.AddTorque(-canoe.up * speedRotationY, ForceMode.Acceleration);
    }

    public void moveBotRight() {
        rb.AddForce(Vector3.zero, ForceMode.Acceleration);
        rb.AddTorque(Vector3.zero, ForceMode.Acceleration);

        rb.AddForce(-canoe.forward * speedTranslationZ, ForceMode.Acceleration);
        rb.AddTorque(-canoe.up * speedRotationY, ForceMode.Acceleration);
    }

    public void moveBotLeft() {
        rb.AddForce(Vector3.zero, ForceMode.Acceleration);
        rb.AddTorque(Vector3.zero, ForceMode.Acceleration);

        rb.AddForce(-canoe.forward * speedTranslationZ, ForceMode.Acceleration);
        rb.AddTorque(canoe.up * speedRotationY, ForceMode.Acceleration);
    }


    void Ring()
    {
        Debug.Log("Ajouter un son cool ici");
        rightSideSound.Play();
    }
}
