using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] GameObject mainMenu, creditsMenu;
    [SerializeField] TMP_Text versionNumber;

    [SerializeField] AudioSource clickAudioSource;
    [SerializeField] AudioClip clickAudio;

    bool startingGame;
    Coroutine startRoutine;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        versionNumber.text = $"v{Application.version}";

        startingGame = false;
        startRoutine = null;

        FadeController.instance.StartFade(0f, 3f);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickAudioSource.Stop();
            clickAudioSource.PlayOneShot(clickAudio);
        }
    }

    public void StartGame()
    {
        if (!startingGame && startRoutine == null)
        {
            startingGame = true;
            startRoutine = StartCoroutine(StartGameRoutine());
        }
    }

    IEnumerator StartGameRoutine()
    {
        FadeController.instance.StartFade(1f, 1.5f);

        while (FadeController.instance.isFading)
            yield return null;

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(1);

        startRoutine = null;
    }

    public void Credits()
    {
        if (!startingGame)
        {
            print("Open credits menu here");
            mainMenu.SetActive(false);
            creditsMenu.SetActive(true);
        }
    }

    public void Back()
    {
        mainMenu.SetActive(true);
        creditsMenu.SetActive(false);
    }

    public void Exit()
    {
        if (!startingGame && startRoutine == null)
        {
            startingGame = true;
            startRoutine = StartCoroutine(CloseGameRoutine());
        }
    }

    IEnumerator CloseGameRoutine()
    {
        FadeController.instance.StartFade(1f, 0.5f);

        while (FadeController.instance.isFading)
            yield return null;

        yield return new WaitForSeconds(1f);

        Application.Quit();

        startRoutine = null;
    }
}
