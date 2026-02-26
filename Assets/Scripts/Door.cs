using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractObject
{
    [SerializeField] GameObject doorRotation;
    [SerializeField] float openRotation = 170.0f;
    bool openDoor;


    public override void Update()
    {
        base.Update();

        if (openDoor)
        {
            doorRotation.transform.Rotate(0.0f, openRotation * Time.deltaTime, 0.0f, Space.Self);

            if (Mathf.Abs(doorRotation.transform.localRotation.eulerAngles.y) > Mathf.Abs(openRotation))
                openDoor = false;
        }
    }

    public override void Interact()
    {
        if (active)
        {
            base.Interact();
            Activate();
            openDoor = true;
            BoxCollider collider = GetComponent<BoxCollider>();
            collider.enabled = false;
        }
    }
}
