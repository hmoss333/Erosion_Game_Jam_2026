using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;

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
}
