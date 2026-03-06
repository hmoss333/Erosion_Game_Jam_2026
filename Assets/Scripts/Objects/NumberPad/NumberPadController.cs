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
    [SerializeField] GameObject indicator;
    Renderer indicatorRenderer;

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
        indicatorRenderer = indicator.GetComponent<Renderer>();
        indicator.SetActive(false);

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
            indicatorRenderer.material.color = Color.green;
            hasTriggered = true;
            m_OnTrigger.Invoke();
        }
        else
        {
            indicatorRenderer.material.color = Color.red;
            tempCode = string.Empty;
        }


        displayText.text = string.Empty;
        indicator.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        indicator.SetActive(false);

        startCodeCheckRoutine = null;
    }
}
