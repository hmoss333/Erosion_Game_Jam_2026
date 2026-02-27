using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerZone : MonoBehaviour
{
    [Header("Event Triggers")]
    public UnityEvent m_OnTrigger = new UnityEvent();
    [SerializeField] bool hasTriggered;
    BoxCollider collider;


    private void Start()
    {
        collider = GetComponent<BoxCollider>();
        ResetTrigger();
    }

    public void ResetTrigger()
    {
        collider.enabled = true;
        hasTriggered = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.tag == "Player")
        {
            collider.enabled = false;
            hasTriggered = true;
            m_OnTrigger.Invoke();
        }
    }
}
