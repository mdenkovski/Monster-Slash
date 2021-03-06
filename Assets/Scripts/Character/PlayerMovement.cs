using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;
using Cinemachine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float WalkSpeed;
    [SerializeField] private float RunSpeed;
    [SerializeField] private float RotationSpeed = 30.0f;


    private PlayerController PlayerController;
    private Animator PlayerAnimator;


    [SerializeField] private float MoveDirectionBuffer = 1.0f;
    private Vector3 NextPositionCheck;
    private Vector2 InputVector = Vector2.zero;
    private Vector3 MoveDirection = Vector3.zero;

    [SerializeField]
    private WeaponBehaviour WeaponBehaviour;

    [SerializeField]
    private GameObject PlayerCamera;

    private void Awake()
    {
        PlayerController = GetComponent<PlayerController>();
        PlayerAnimator = GetComponent<Animator>();
        WeaponBehaviour.BoxCollider.enabled = false;


        WeaponBehaviour.EnemyHitEvent.AddListener(OnEnemyHit);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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

    public void OnLook(InputValue value)
    {
        //Debug.Log(value.Get());
        transform.Rotate(Vector3.up, RotationSpeed * value.Get<float>() * Time.deltaTime);
    }

    public void OnTilt(InputValue value)
    {
        float TiltAmount = value.Get<float>();
        //Debug.Log(TiltAmount);
        PlayerCamera.transform.Rotate(-TiltAmount * RotationSpeed * Time.deltaTime, 0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.IsAttacking) return; //dont want to move when attacking

        MoveDirection = transform.forward * InputVector.y + transform.right * InputVector.x ;

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
            Invoke("EnableWeaopnCollider", 0.5f);
        }
    }

    public void StopAttacking()
    {
        WeaponBehaviour.BoxCollider.enabled = false;
        PlayerController.IsAttacking = false;
    }


    private void EnableWeaopnCollider()
    {
        WeaponBehaviour.BoxCollider.enabled = true;
    }

    private void OnEnemyHit()
    {
        //Debug.Log("Hit enemy. Disabling collider");
        WeaponBehaviour.BoxCollider.enabled = false;
    }

    private void OnDisable()
    {
        WeaponBehaviour.EnemyHitEvent.RemoveListener(OnEnemyHit);
    }
}
