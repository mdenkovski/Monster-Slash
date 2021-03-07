using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameplayStats : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField]
    private float MaxHealth = 100.0f;
    [SerializeField]
    private float CurrentHealth;
    [SerializeField]
    private float Attack;
    [SerializeField]
    private float Defence;

    public UnityEvent DeathEvent;

    private void Awake()
    {
        //set current health to max health
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage - Defence;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);

        if (CurrentHealth <=0)
        {
            DeathEvent.Invoke();
        }
    }

    public bool IsDead()
    {
        return CurrentHealth <= 0.0f;
    }

    public float GetAttackPower()
    {
        return Attack;
    }

    public void ResetStats()
    {
        CurrentHealth = MaxHealth;
    }

}
