using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticManController : MonoBehaviour
{
    // Adjust the speed for the application.
    public float speed = 1.0f;
    [SerializeField] bool spawnStaticMan;
    ObjectFlicker flickerEffect;
    [SerializeField] GameObject baseModel;

    Rigidbody rb;
    Animator animator;


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
                print("Reached player");
            }
        }

        baseModel.SetActive(spawnStaticMan);
        rb.isKinematic = !spawnStaticMan || isPlaying("Standing");
    }

    public bool isPlaying(string stateName)
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(stateName))
            return true;
        else
            return false;
    }
}
