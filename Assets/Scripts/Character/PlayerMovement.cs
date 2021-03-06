using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float WalkSpeed;
    [SerializeField] private float RunSpeed;


    private PlayerController PlayerController;
    private Animator PlayerAnimator;
    private Rigidbody PlayerRigidbody;

    private Transform PlayerTransform;

    private Vector2 InputVector = Vector2.zero;
    private Vector3 MoveDirection = Vector3.zero;

    private void Awake()
    {
        PlayerTransform = transform;
        PlayerController = GetComponent<PlayerController>();
        PlayerAnimator = GetComponent<Animator>();
        PlayerRigidbody = GetComponent<Rigidbody>();
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

        PlayerTransform.position += movementDirection;
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
