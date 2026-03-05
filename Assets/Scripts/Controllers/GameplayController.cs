using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System.Linq;
using TMPro;

public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;
    [SerializeField] GameObject[] environmentalObjs;
    [SerializeField] Material[] environmentalMats;
    [SerializeField] GameObject staticRoom, finallHallway;
    public string doorCode { get; private set; }
    public int cycleNum { get; private set; }
    [SerializeField] TMP_Text documentText;
    [SerializeField] AudioSource ambientAudioSource;

    [SerializeField] GameObject pauseMenu;
    public bool isPaused { get; private set; }

    [Header("Final Cycle Triggers")]
    public UnityEvent m_OnTrigger = new UnityEvent();

    [SerializeField] GameObject staticManTriggerZone;


    private void Start()
    {
        if (instance != null)
            Destroy(this);
        else
            instance = this;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        environmentalObjs = FindGameObjectsInLayer(7);

        staticManTriggerZone.SetActive(false);
        staticRoom.SetActive(true);
        finallHallway.SetActive(false);

        cycleNum = 0;
        isPaused = false;

        GenerateNewCode(); //Generate a random code at the start of a session

        FadeController.instance.StartFade(0.0f, 3f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && PlayerController.instance.state != PlayerController.States.interacting)
        {
            isPaused = !isPaused;
        }

        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isPaused;
        pauseMenu.SetActive(isPaused);
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

        //Restart ambient audio
        ambientAudioSource.Stop();
        ambientAudioSource.Play();

        UpdateMats(cycleNum);

        if (cycleNum == 2)
            staticManTriggerZone.SetActive(true);

        //If completing the last cycle, trigger final hallway
        if (cycleNum >= 3)
        {
            m_OnTrigger.Invoke();
        }
    }

    public void UpdateMats(int cycle)
    {
        //Update all wall textures
        foreach (GameObject envObj in environmentalObjs)
        {
            List<Material> mats = envObj.GetComponent<Renderer>().materials.ToList();
            mats[0] = environmentalMats[cycle - 1];
            envObj.GetComponent<Renderer>().materials = mats.ToArray();
        }
    }

    private void GenerateNewCode()
    {
        doorCode = string.Empty;

        for (int i = 0; i < 4; i++)
        {
            int randChar = Random.Range(0, 10);
            doorCode += randChar;
        }

        print($"New door code: {doorCode}");
    }

    public void UpdateDocumentText(string docText)
    {
        documentText.text = docText;
        print("Updated document text");
    }


    //Pause Menu Functions
    public void ResumeGame()
    {
        isPaused = false;
    }

    public void ExitGame()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
