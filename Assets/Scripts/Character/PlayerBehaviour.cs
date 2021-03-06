using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{

    private GameplayStats PlayerStats;
    private Animator PlayerAnimator;

    private Vector3 StartPosition;
    private Quaternion StartRotation;

    private void Awake()
    {
        PlayerStats = GetComponent<GameplayStats>();
        PlayerAnimator = GetComponent<Animator>();
        StartPosition= transform.position;
        StartRotation = transform.rotation;
    }
    private void OnEnable()
    {
        PlayerStats.DeathEvent.AddListener(OnDeath);
    }
   
    private void OnDisable()
    {
        PlayerStats.DeathEvent.RemoveListener(OnDeath);
    }

    private void OnDeath()
    {
        PlayerAnimator.SetTrigger("Dead");
        GetComponent<PlayerInput>().enabled = false;
        Debug.Log("Player Died");
    }


    public void DeathCleanup()
    {
        PlayerStats.ResetStats();
        Time.timeScale = 0.0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        FindObjectOfType<GameMenuController>().GoToGameOver();


    }

    public void RestartPlayer()
    {
        Time.timeScale = 1.0f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        transform.position = StartPosition;
        transform.rotation = StartRotation;
        GetComponent<PlayerInput>().enabled = true;
    }
}
