using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;

public class InteractObject : MonoBehaviour
{
    public bool active;

    public bool highlighted; //testing
    public bool interacting;
    public AudioSource audioSource;
    public AudioClip interactClip;


    [Header("Event Triggers")]
    public UnityEvent m_OnTrigger = new UnityEvent();


    // Update is called once per frame
    public virtual void Update()
    {
        highlighted = GetComponent<Outline>();

        if (PlayerController.instance.interactObj != this || PlayerController.instance.state == PlayerController.States.interacting
            && highlighted == true)
        {
            Destroy(GetComponent<Outline>());
        }
    }

    public virtual void Activate()
    {
        active = true;
        print($"Activated {this.gameObject.name}");
    }

    public virtual void Interact()
    {
        print($"Interacted with {this.gameObject.name}");

        audioSource.Stop();
        audioSource.PlayOneShot(interactClip);
    }
}
