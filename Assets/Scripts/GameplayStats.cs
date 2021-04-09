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
    [SerializeField]
    private float CurrentHealth;
    [SerializeField]
    private float Attack;
    private float AttackModifier = 0;
    [SerializeField]
    private float Defence;

   

    private float DefenceModifier = 0;

    public UnityEvent DeathEvent;

    private HealthBarScript HealthBar;

    private void Awake()
    {
        //set current health to max health
        CurrentHealth = MaxHealth;
        HealthBar = GetComponentInChildren<HealthBarScript>();
        if (HealthBar != null)
        {
            HealthBar.Initialize(MaxHealth);
        }
    }

    public void TakeDamage(float damage)
    {
        float resultingDamage = damage - (Defence + DefenceModifier);
        if (resultingDamage <= 0)
        {
            resultingDamage = 1; //always have at least one damage go through
        }
        CurrentHealth -= resultingDamage;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);

        if (HealthBar != null)
        {
            HealthBar.SetValue(CurrentHealth);
        }


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
        return Attack + AttackModifier;
    }

    public void ResetStats()
    {
        CurrentHealth = MaxHealth;
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

}
