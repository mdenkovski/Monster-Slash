using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameplayStats : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField]
    private float MaxHealth = 100.0f;
    private float ModifiedMaxHealth;
    [SerializeField]
    private float CurrentHealth;
    [SerializeField]
    private float Attack;
    private float AttackModifier = 0;
    [SerializeField]
    private float Defence;

    private float AttackDifficultyMultiplier = 1.0f;
    private float DefenceDifficultyMultiplier = 1.0f;

    private float DefenceModifier = 0;

    public UnityEvent DeathEvent;

    private HealthBarScript HealthBar;

    private bool Dead = false;

    private void Awake()
    {
        //set current health to max health
        HealthBar = GetComponentInChildren<HealthBarScript>();
        CurrentHealth = MaxHealth;
        ModifiedMaxHealth = MaxHealth;
        if (HealthBar != null)
        {
            HealthBar.Initialize(MaxHealth);
        }
    }

    public void TakeDamage(float damage)
    {
        float resultingDamage = damage - ((Defence + DefenceModifier) * DefenceDifficultyMultiplier);
        if (resultingDamage <= 0)
        {
            resultingDamage = 1; //always have at least one damage go through
        }
        CurrentHealth -= resultingDamage;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, ModifiedMaxHealth);

        if (HealthBar != null)
        {
            HealthBar.SetValue(CurrentHealth);
        }


        if (CurrentHealth <= 0 && !Dead)
        {
            DeathEvent.Invoke();
            Dead = true;
        }

    }

    public bool IsDead()
    {
        return CurrentHealth <= 0.0f;
    }

    public float GetAttackPower()
    {
        return (Attack + AttackModifier) * AttackDifficultyMultiplier;
    }

    public void ResetStats()
    {
        CurrentHealth = MaxHealth;
        Dead = false;
    }

    public void EquipWeapon(WeaponScriptable equippedWeapon)
    {
        AttackModifier = equippedWeapon.AttackPower;
    }

    public void UnequipWeapon()
    {
        AttackModifier = 0;
    }

    internal void EquipArmor(ArmorScriptable equipepdArmor)
    {
        DefenceModifier = equipepdArmor.DefenceBonus;

    }

    internal void UnequipArmor()
    {
        DefenceModifier = 0;
    }

    private void OnDestroy()
    {
        DeathEvent.RemoveAllListeners();
    }

    private void ModifyCurrentHealthScaling(float newHealth)
    {
        ModifiedMaxHealth = newHealth;
        CurrentHealth = ModifiedMaxHealth;
        if (HealthBar != null)
        {
            HealthBar.Initialize(ModifiedMaxHealth);
        }
    }

    public void SetDifficulty(int difficulty)
    {
        AttackDifficultyMultiplier = 1 + 1.2f * (difficulty-1);
        DefenceDifficultyMultiplier = 1 + 1.8f * (difficulty-1);
        ModifyCurrentHealthScaling(MaxHealth + 1.8f * (difficulty - 1));
    }
}
