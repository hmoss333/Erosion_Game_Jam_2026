using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StaticManController : MonoBehaviour
{
    // Adjust the speed for the application.
    public float speed = 1.0f;
    [SerializeField] bool spawnStaticMan;
    ObjectFlicker flickerEffect;
    [SerializeField] GameObject baseModel;
    [SerializeField] Transform focusPoint;

    Rigidbody rb;
    Animator animator;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip catchPlayerClip;
    Coroutine catchPlayerRoutine;


    private void Start()
    {
        flickerEffect = GetComponent<ObjectFlicker>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        baseModel.SetActive(false);
    }

    public void SpawnStaticMan()
    {
        spawnStaticMan = true;
    }

    void Update()
    {
        if (spawnStaticMan)
        {
            flickerEffect.StartFlicker(0.75f);

            rb.velocity = transform.forward * speed;
            Vector3 playerPos = new Vector3(PlayerController.instance.transform.position.x, transform.position.y, PlayerController.instance.transform.position.z);
            transform.LookAt(playerPos);

            float dist = Vector3.Distance(transform.position, PlayerController.instance.transform.position);
            if (dist <= 2f)
            {
                print("Caught player");
                speed = 0f;
                if (catchPlayerRoutine == null)
                    catchPlayerRoutine = StartCoroutine(CatchPlayer());
            }
        }

        baseModel.SetActive(spawnStaticMan);
        rb.isKinematic = !spawnStaticMan || isPlaying("Standing") || catchPlayerRoutine != null;
    }

    public bool isPlaying(string stateName)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(stateName))
            return true;
        else
            return false;
    }

    IEnumerator CatchPlayer()
    {
        PlayerController.instance.SetState(PlayerController.States.interacting);
        CamFocusController.instance.FocusTarget(focusPoint);

        animator.SetBool("Catch", true);

        audioSource.PlayOneShot(catchPlayerClip);

        FadeController.instance.StartFade(1.0f, 2.5f);

        while (FadeController.instance.isFading)
            yield return null;

        yield return new WaitForSeconds(2.5f);

        SceneManager.LoadSceneAsync(0);
        catchPlayerRoutine = null;
    }
}
