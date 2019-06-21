using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA : MonoBehaviour
{

    [SerializeField] private GameObject canoe;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(waiter());
    }
    IEnumerator waiter()
    {
        yield return new WaitForSeconds(2.0f);

        if (Random.Range(0, 4) == 0)
        {
            canoe.GetComponent<Canoe>().moveTopLeft();
        } else
        {
            canoe.GetComponent<Canoe>().moveTopRight();
        }
    }
}
