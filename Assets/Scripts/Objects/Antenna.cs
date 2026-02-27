using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Antenna : InteractObject
{
    [SerializeField] private float speed, currentValue, targetValue, offset;
    float checkTime;
    [SerializeField] GameObject miniGameUI, miniGameLight;
    [SerializeField] Slider miniGameSlider;
    AudioSource audioSource;
    [SerializeField] AudioClip activateClip;
    bool triggered = false;


    private void Start()
    {
        targetValue = Random.Range(4.5f, 10f);
        miniGameSlider.maxValue = 10f;
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = activateClip;
    }

    private void Update()
    {
        if (interacting && !triggered)
        {
            float antennaValue = Input.GetAxisRaw("Vertical");
            currentValue += antennaValue * speed * Time.deltaTime;
            miniGameSlider.value = currentValue;
            if (currentValue <= targetValue + offset && currentValue >= targetValue - offset)
            {
                miniGameLight.GetComponent<Renderer>().material.color = Color.yellow;
                checkTime += Time.deltaTime;
                if (checkTime >= 2.5f)
                {
                    interacting = false;
                    TurnOn();
                }
            }
            else
            {
                miniGameLight.GetComponent<Renderer>().material.color = Color.red;
                checkTime = 0f;
            }
        }
        else
        {
            checkTime = 0f;

            if (!active)
                miniGameLight.GetComponent<Renderer>().material.color = Color.black;
            else if (triggered)
                miniGameLight.GetComponent<Renderer>().material.color = Color.green;
            else
                miniGameLight.GetComponent<Renderer>().material.color = Color.red;
        }

        miniGameUI.SetActive(active && interacting && !triggered);
    }

    public override void Interact()
    {
        base.Interact();
        interacting = !interacting;
        PlayerController.instance.SetState(interacting ? PlayerController.States.interacting : PlayerController.States.idle);
        currentValue = 0f;
        checkTime = 0f;
    }

    void TurnOn()
    {
        miniGameLight.GetComponent<Renderer>().material.color = Color.green;
        audioSource.Play();
        triggered = true;
        Interact();
        m_OnTrigger.Invoke();
    }
}
