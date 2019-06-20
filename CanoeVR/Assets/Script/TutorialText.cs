using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;
using UnityEngine.UI;
using System;

public class TutorialText : MonoBehaviour
{
    public Transform canvas; 
    //-------------------------------------------------
    protected virtual void Awake()
    {
        Button button = GetComponent<Button>();
        if (button)
        {
            button.onClick.AddListener(OnButtonClick);
        }
    }


    protected virtual void OnButtonClick()
    {
        canvas.gameObject.SetActive(false);
    }
}
