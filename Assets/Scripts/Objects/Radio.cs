using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Radio : InteractObject
{
    public static Radio instance;

    [SerializeField] Transform focusPoint;
    [SerializeField] SpriteRenderer arrowLeft, arrowRight;
    [SerializeField] Color arrowDefault, arrowActive;
    [SerializeField] [Range(30, 120)] float currentFrequency; //Use LF (low frequency) band for radio stations
    [SerializeField] float targetFrequency;// { get; private set; } //public in order to display frequency on documents

    [SerializeField] TMP_Text radioText;
    [SerializeField] float rotateSpeed;
    private float focusTime = 0f;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip targetAudio, staticAudio, badAudio;

    bool triggered;


    public void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);

        InitializeRadio();
    }

    private void OnDisable()
    {
        currentFrequency = 36.9f;
        radioText.text = currentFrequency.ToString("F2");
    }

    public void InitializeRadio()
    {
        interacting = false;
        triggered = false;
        audioSource.clip = staticAudio;
        audioSource.loop = true;
        audioSource.Play();
        currentFrequency = 36.9f;
        targetFrequency = Random.Range(30.0f, 120.0f);
        GameplayController.instance.UpdateDocumentText(targetFrequency.ToString("F2") + "kHz");
    }

    public override void Update()
    {
        base.Update();

        currentFrequency = Mathf.Clamp(currentFrequency, 30f, 120f);
        radioText.text = currentFrequency.ToString("F2") + "kHz";
        radioText.gameObject.SetActive(interacting);
        arrowLeft.gameObject.SetActive(interacting);
        arrowRight.gameObject.SetActive(interacting);


        //TODO
        //Add logic to have the player tune the radio to a randomized station value in order to get the instructions for the current document
        if (interacting && !triggered)
        {
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                //scroll frequency down
                currentFrequency += Time.deltaTime * rotateSpeed;
            }
            else if (Input.GetAxisRaw("Horizontal") < 0)
            {
                //scroll frequency up
                currentFrequency -= Time.deltaTime * rotateSpeed;
            }

            //Check if current frequency matches target frequency
            if (active && currentFrequency <= targetFrequency + 1.5f && currentFrequency >= targetFrequency - 1.5f)
            {
                focusTime += Time.deltaTime;
                if (focusTime >= 2.5f)
                {
                    focusTime = 0f;
                    triggered = true;
                    m_OnTrigger.Invoke();
                    Interact();

                    ////Play station audio
                    if (audioSource.clip != targetAudio)
                    {
                        audioSource.Stop();
                        audioSource.clip = targetAudio;
                        audioSource.Play();
                    }

                    DialogueController.instance.UpdateText(GameplayController.instance.doorCode[GameplayController.instance.cycleNum].ToString(), true);
                }
            }
            else
            {
                focusTime = 0f;
            }
        }
    }

    public override void Interact()
    {
        base.Interact();
        interacting = !interacting;
        PlayerController.instance.SetState(interacting ? PlayerController.States.interacting : PlayerController.States.idle);

        //if (interacting)
        //{
        //    CamFocusController.instance.FocusTarget(focusPoint);
        //}
        //else
        //{
        //    CamFocusController.instance.FocusReset();
        //}
    }
}
