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
    private GameObject PlayerCharacter;
    [SerializeField]
    private Transform PlayerTarget;
    [SerializeField]
    private BoxCollider Collider;

    [SerializeField]
    private float AttackRange = 2.0f;
    private bool IsAttacking = false;
    [SerializeField]
    private float AttackSpeed = 2.0f;
    private IEnumerator AttackCoroutine;

    private void Awake()
    {
        Animator = GetComponent<Animator>();
        MonsterStats = GetComponent<GameplayStats>();
        NavAgent = GetComponent<NavMeshAgent>();
        Collider = GetComponent<BoxCollider>();

    }

    private void OnEnable()
    {
        MonsterStats.DeathEvent.AddListener(OnDeath);

    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerCharacter = FindObjectOfType<PlayerController>().gameObject;
        PlayerTarget = PlayerCharacter.transform;
        NavAgent.SetDestination(PlayerTarget.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (IsAttacking) return;

        NavAgent.SetDestination(PlayerTarget.position);
        
        CheckInRange();

        Animator.SetFloat("Speed", NavAgent.velocity.magnitude);

    }

    private void CheckInRange()
    {
        

        if (Vector3.Distance(transform.position, PlayerTarget.position) <= AttackRange)
        {
            if (!IsAttacking && !PlayerCharacter.GetComponent<GameplayStats>().IsDead())
            {
                if (AttackCoroutine == null) // only attack when attack delay occurs
                {
                    AttackCoroutine = Attack();
                    StartCoroutine(AttackCoroutine);
                }
            }
        }
    }

    IEnumerator Attack()
    {
        transform.LookAt(PlayerTarget.position);
        //NavAgent.speed = 0;

        Animator.SetTrigger("Attack");
        StartCoroutine(HitPlayer());
        yield return new WaitForSeconds(AttackSpeed);
        AttackCoroutine = null;
    }

    IEnumerator HitPlayer()
    {
        yield return new WaitForSeconds(0.5f);

        PlayerCharacter.GetComponent<GameplayStats>().TakeDamage(MonsterStats.GetAttackPower());
    }

    public void StopAttacking()
    {
        IsAttacking = false;
    }

    private void OnDeath()
    {
        Collider.enabled = false;
        NavAgent.isStopped = true;
        //NavAgent.enabled = false;
        Animator.SetBool("IsDead", true);
        Debug.Log(name + " has died");
    }

    public void DeathCleanup()
    {
        Debug.Log("Cleaning Up");
        Destroy(this.gameObject);
    }

}
