using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;


public class NumberPadController : MonoBehaviour
{
    public static NumberPadController instance;

    [SerializeField] TMP_Text displayText;
    [SerializeField] string code;
    [SerializeField] string tempCode;
    bool hasTriggered;

    Coroutine startCodeCheckRoutine;


    [Header("Event Triggers")]
    public UnityEvent m_OnTrigger = new UnityEvent();

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        InitializeLock();
    }

    public void InitializeLock()
    {
        code = GameplayController.instance.doorCode; //randomly generated from GameplayController class
        tempCode = string.Empty;
        hasTriggered = false;
    }

    public void ButtonInput(char inputVal)
    {
        if (tempCode.Length < 4)
        {
            tempCode += inputVal;
            tempCode = tempCode.ToLower();
            displayText.text = tempCode;
        }

        if (!hasTriggered && tempCode.Length >= 4)
            StartCodeCheck();
    }

    private void StartCodeCheck()
    {
        if (startCodeCheckRoutine == null)
            startCodeCheckRoutine = StartCoroutine(CheckCode());
    }

    IEnumerator CheckCode()
    {
        yield return new WaitForSeconds(0.5f);

        if (tempCode == code)
        {
            hasTriggered = true;
            m_OnTrigger.Invoke();
        }
        else
        {
            tempCode = string.Empty;
        }

        displayText.text = string.Empty;
        startCodeCheckRoutine = null;
    }
}
