using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCount : MonoBehaviour
{
    [SerializeField] private int score;
    [SerializeField] private int scoreMax;
    [SerializeField] public GameObject tutoCanevas;
    [SerializeField] public Text text;
    private bool messageDisabled;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        scoreMax = transform.childCount;
        messageDisabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!messageDisabled && score == scoreMax)
        {
            text.text = "The end !\n You had " + score + " obstacles out of  " + scoreMax;
            messageDisabled = true;
            tutoCanevas.SetActive(true);
        }
    }

    public void incrementScore()
    {
        score = score + 1;
    }
}
