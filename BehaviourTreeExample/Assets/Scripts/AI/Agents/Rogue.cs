using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Rogue : MonoBehaviour
{

    private BTBaseNode tree;
    private NavMeshAgent agent;
    private Animator animator;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        //TODO: Create your Behaviour tree here
    }

    private void FixedUpdate()
    {
        tree?.Tick();
    }
}
