using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;
    [SerializeField] GameObject[] environmentalObjs;
    [SerializeField] Material[] environmentalMats;
    public int cycleNum { get; private set; }   
    [Header("Final Cycle Triggers")]
    public UnityEvent m_OnTrigger = new UnityEvent();


    private void Start()
    {
        if (instance != null)
            Destroy(this);
        else
            instance = this;

        environmentalObjs = FindGameObjectsInLayer(7);
        cycleNum = 0;
        FadeController.instance.StartFade(0.0f, 3f);
    }

    GameObject[] FindGameObjectsInLayer(int layer)
    {
        var goArray = FindObjectsOfType(typeof(GameObject)) as GameObject[];
        var goList = new List<GameObject>();
        for (int i = 0; i < goArray.Length; i++)
        {
            if (goArray[i].layer == layer)
            {
                goList.Add(goArray[i]);
            }
        }
        if (goList.Count == 0)
        {
            return null;
        }
        return goList.ToArray();
    }

    public void IncrementCycle()
    {
        cycleNum++;

        foreach(GameObject envObj in environmentalObjs)
        {
            List<Material> mats = envObj.GetComponent<Renderer>().materials.ToList();
            mats[0] = environmentalMats[cycleNum - 1];
            envObj.GetComponent<Renderer>().materials = mats.ToArray();
        }

        if (cycleNum >= 3)
        {
            m_OnTrigger.Invoke();
        }
    }
}
