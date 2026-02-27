using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;

    public string doorCode { get; private set; }
    public int cycleNum { get; private set; }
    [SerializeField] TMP_Text documentText;

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
        documentText.text = Radio.instance.targetFrequency.ToString("F2") + "kHz"; //Display the current target frequency on a document for the player to discover
        FadeController.instance.StartFade(0.0f, 3f);
    }

    public void IncrementCycle()
    {
        cycleNum++;
        documentText.text = Radio.instance.targetFrequency.ToString("F2") + "kHz";

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
