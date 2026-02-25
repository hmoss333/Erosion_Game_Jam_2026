using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RadioLock : InteractObject
{
    [SerializeField] Light light;
    [SerializeField] Color l_active, l_inactive;


    public override void Update()
    {
        base.Update();

        light.color = active ? l_active : l_inactive; 
    }
}
