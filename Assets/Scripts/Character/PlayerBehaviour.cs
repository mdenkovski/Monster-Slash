using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{

    private GameplayStats PlayerStats;
    private Animator PlayerAnimator;

    private void Awake()
    {
        PlayerStats = GetComponent<GameplayStats>();
        PlayerAnimator = GetComponent<Animator>();
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
        GetComponent<PlayerInput>().enabled = true;
    }
}
