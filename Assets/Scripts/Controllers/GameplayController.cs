using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;

    public int cycleNum;


    private void Start()
    {
        if (instance != null)
            Destroy(this);
        else
            instance = this;

        FadeController.instance.StartFade(0.0f, 3f);
    }

    public void IncrementCycle()
    {
        cycleNum++;
    }
}
