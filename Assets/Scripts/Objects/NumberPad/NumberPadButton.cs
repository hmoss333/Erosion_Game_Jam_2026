using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberPadButton : InteractObject
{
    [SerializeField] char value;
    bool hasClicked;
    float resetTime;

    public override void Update()
    {
        base.Update();

        if (hasClicked)
        {
            resetTime += Time.deltaTime;
            if (resetTime >= 0.25f)
            {
                hasClicked = false;
                resetTime = 0f;
            }
        }
    }

    public override void Interact()
    {
        if (!hasClicked)
        {
            base.Interact();
            hasClicked = true;
            NumberPadController.instance.ButtonInput(value);
        }
    }
}
