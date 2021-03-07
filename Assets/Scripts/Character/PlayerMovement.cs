using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float WalkSpeed;
    [SerializeField] private float RunSpeed;


    private PlayerController PlayerController;
    private Animator PlayerAnimator;


    [SerializeField] private float MoveDirectionBuffer = 1.0f;
    private Vector3 NextPositionCheck;
    private Vector2 InputVector = Vector2.zero;
    private Vector3 MoveDirection = Vector3.zero;

    private void Awake()
    {
        PlayerController = GetComponent<PlayerController>();
        PlayerAnimator = GetComponent<Animator>();
    }

    public void OnMove(InputValue value)
    {
        InputVector = value.Get<Vector2>();
        PlayerAnimator.SetFloat("MovementX", InputVector.x);
        PlayerAnimator.SetFloat("MovementY", InputVector.y);
    }



    public void OnRun(InputValue value)
    {
        PlayerController.IsRunning = value.isPressed;
        PlayerAnimator.SetBool("IsRunning", value.isPressed);
    }


    // Update is called once per frame
    void Update()
    {
        if (PlayerController.IsAttacking) return; //dont want to move when attacking

        MoveDirection = transform.forward * InputVector.y + transform.right * InputVector.x;

        float currentSpeed = PlayerController.IsRunning ? RunSpeed : WalkSpeed;

        Vector3 movementDirection = MoveDirection * (currentSpeed * Time.deltaTime);

        NextPositionCheck = transform.position + MoveDirection * MoveDirectionBuffer;

        if (NavMesh.SamplePosition(NextPositionCheck, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
        {
            transform.position += movementDirection;
        }

    }

    public void OnAttack(InputValue value)
    {
        if (value.isPressed && !PlayerController.IsAttacking)
        {
            PlayerAnimator.SetTrigger("Attack");
            PlayerController.IsAttacking = value.isPressed;
        }
    }
}
