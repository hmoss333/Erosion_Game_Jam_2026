using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class PlayerController : MonoBehaviour
{
    public enum State { idle, move, interact }
    public State state;
    [SerializeField] float speed;
    [SerializeField] float interactDist;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetState(State setState)
    {
        state = setState;
    }
}
