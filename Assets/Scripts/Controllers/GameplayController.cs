using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;

    public string doorCode { get; private set; }
    public int cycleNum { get; private set; }
    [Header("Final Cycle Triggers")]
    public UnityEvent m_OnTrigger = new UnityEvent();


    private void Start()
    {
        if (instance != null)
            Destroy(this);
        else
            instance = this;

        cycleNum = 0;
        GenerateNewCode(); //Generate a random code at the start of a session
        FadeController.instance.StartFade(0.0f, 3f);
    }

    public void IncrementCycle()
    {
        cycleNum++;

        if (cycleNum > 3)
        {
            m_OnTrigger.Invoke();
        }
    }

    private void GenerateNewCode()
    {
        doorCode = string.Empty;

        for (int i = 0; i < 4; i++)
        {
            int randChar = Random.Range(0, 10);
            doorCode += randChar;
        }

        print($"New door code: {doorCode}");
    }
}
