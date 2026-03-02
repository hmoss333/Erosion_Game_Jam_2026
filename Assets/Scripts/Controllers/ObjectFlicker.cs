using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


//Any flicker object should be set as a child of the controller object so that it can be toggled without interrupting the Blink coroutine
public class ObjectFlicker : MonoBehaviour
{
    [SerializeField] GameObject objToFlicker;
    Coroutine flickerRoutine;

    private void Start()
    {
        flickerRoutine = null;
    }

    public void StartFlicker()
    {
        if (flickerRoutine == null)
            flickerRoutine = StartCoroutine(Blink());
    }

    public void StopFlicker()
    {
        StopCoroutine(flickerRoutine);
        objToFlicker.SetActive(true);
    }

    IEnumerator Blink()
    {
        while (true)
        {
            objToFlicker.SetActive(false);
            yield return new WaitForSeconds(Random.Range(0.0f, 0.1f));
            objToFlicker.SetActive(true);
            yield return new WaitForSeconds(Random.Range(0.0f, 0.1f));
        }
    }
}
