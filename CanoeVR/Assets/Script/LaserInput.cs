using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.Extras;
using Valve.VR.InteractionSystem;

public class LaserInput : MonoBehaviour
{

    public static GameObject currentObject;
    int currentID;
    public Transform canvas;

    // Start is called before the first frame update 
    void Start()
    {
        currentObject = null;
        currentID = 0;
    }

    void Update()
    {
        //Sends out a Raycast and returns an array filled with everything 
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, transform.forward, 100.0F);
        //Goes through all the hit objects and checks if any of them were our button
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            //I use the object Id to determine if I have already run the code for this object 
            int id = hit.collider.gameObject.GetInstanceID();
            //If I haven't then I will run it again but If I have it is unnecessary to keep running it
            if (currentID != id)
            {
                currentID = id;
                currentObject = hit.collider.gameObject;

                //Checks based off the name
                string tag = currentObject.tag;
                //Debug.Log("laser points on " + currentObject);
                if (tag == "Tuto")
                {
                    canvas.gameObject.SetActive(false);
                    gameObject.GetComponent<SteamVR_LaserPointer>().enabled = false;
                }
            }
        }
    }
}



