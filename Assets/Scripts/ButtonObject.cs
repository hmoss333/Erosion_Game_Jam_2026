using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonObject : InteractObject
{
    public override void Interact()
    {
        base.Interact();

        m_OnTrigger.Invoke();
    }
}
