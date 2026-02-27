using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractObject
{
    [SerializeField] GameObject doorRotation;
    [SerializeField] float openRotation = 170.0f;
    BoxCollider collider;
    bool openDoor;

    private void Start()
    {
        collider = GetComponent<BoxCollider>();
    }

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
            collider.enabled = false;
        }
    }

    public void ResetDoor()
    {
        active = false;
        collider.enabled = true;
        doorRotation.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
    }
}
