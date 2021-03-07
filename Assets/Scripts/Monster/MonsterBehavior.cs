using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterBehavior : MonoBehaviour
{
    [SerializeField]
    private Animator Animator;
    [SerializeField]
    private GameplayStats MonsterStats;

    [SerializeField]
    private NavMeshAgent NavAgent;

    [SerializeField]
    private Transform PlayerTarget;

    private void Awake()
    {
        Animator = GetComponent<Animator>();
        MonsterStats = GetComponent<GameplayStats>();
        NavAgent = GetComponent<NavMeshAgent>();

        MonsterStats.DeathEvent.AddListener(OnDeath);
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerTarget = FindObjectOfType<PlayerController>().gameObject.transform;
        NavAgent.SetDestination(PlayerTarget.position);
    }

    // Update is called once per frame
    void Update()
    {
        //Animator.SetFloat("Speed", NavAgent.velocity.magnitude);
    }

    private void OnDeath()
    {
        Debug.Log(name + " has died");
    }

}
