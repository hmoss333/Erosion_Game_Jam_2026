using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticManController : MonoBehaviour
{
    // Adjust the speed for the application.
    public float speed = 1.0f;
    [SerializeField] bool spawnStaticMan;
    ObjectFlicker flickerEffect;
    Rigidbody rb;


    private void Start()
    {
        flickerEffect = GetComponent<ObjectFlicker>();
        rb = GetComponent<Rigidbody>();
    }

    public void SpawnStaticMan()
    {
        spawnStaticMan = true;
    }

    void Update()
    {
        if (spawnStaticMan)
        {
            float dist = Vector3.Distance(transform.position, PlayerController.instance.transform.position);

            // Move our position a step closer to the target.
            //float step = speed * Time.deltaTime; // calculate distance to move
            //transform.position = Vector3.MoveTowards(transform.position, PlayerController.instance.transform.position, step);
            //Vector3 newDirection = Vector3.RotateTowards(transform.position, PlayerController.instance.transform.position, step, 0.0f);
            //transform.rotation = Quaternion.LookRotation(newDirection);

            rb.velocity = transform.forward * speed;
            Vector3 playerPos = new Vector3(PlayerController.instance.transform.position.x, transform.position.y, PlayerController.instance.transform.position.z);
            transform.LookAt(playerPos);

            flickerEffect.StartFlicker(0.5f);

            if (dist <= 2.5f)
            {
                print("Reached player");
            }
        }
    }
}
