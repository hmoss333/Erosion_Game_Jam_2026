using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RadioLock : InteractObject
{
    [SerializeField] private GameObject lightObj;
    private Material lightMat;
    [SerializeField] private Color l_active, l_inactive;


    private void Start()
    {
        lightMat = lightObj.GetComponent<Renderer>().material;
    }

    public override void Update()
    {
        base.Update();

        Color setColor = new Color();
        setColor = active ? l_active : l_inactive;
        lightMat.SetColor("_EmissionColor", setColor);
    }

    public override void Activate()
    {
        base.Activate();

        audioSource.Stop();
        audioSource.PlayOneShot(interactClip);

        m_OnTrigger.Invoke();
    }

    public void ResetLock()
    {
        active = false;
    }
}
