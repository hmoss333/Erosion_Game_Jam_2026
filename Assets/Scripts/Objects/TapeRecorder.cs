using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TapeRecorder : InteractObject
{
    [SerializeField] List<DialogueObj> dialogueObjs;

    Coroutine playRecordingRoutine;


    public override void Interact()
    {
        if (active)
        {
            base.Interact();

            StartPlayRecordingRoutine();
            m_OnTrigger.Invoke();
        }
    }

    private void StartPlayRecordingRoutine()
    {
        if (playRecordingRoutine == null)
            playRecordingRoutine = StartCoroutine(PlayRecording());
    }

    IEnumerator PlayRecording()
    {
        PlayerController.instance.SetState(PlayerController.States.interacting);

        int i = 0;
        while (true)
        {
            DialogueController.instance.UpdateText(dialogueObjs[GameplayController.instance.cycleNum].lines[i], false);
            if (Input.GetMouseButtonDown(0))
            {
                i++;
                if (i >= dialogueObjs[GameplayController.instance.cycleNum].lines.Length)
                    break;
            }
            yield return null;
        }

        active = false;
        DialogueController.instance.UpdateText(string.Empty, false);
        PlayerController.instance.SetState(PlayerController.States.idle);
        playRecordingRoutine = null;
    }
}


[System.Serializable]
struct DialogueObj
{
    public string[] lines;
}
