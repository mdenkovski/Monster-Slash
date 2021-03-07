using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameplayStats PlayerStats;

    public BoxCollider BoxCollider;
    public UnityEvent EnemyHitEvent;


    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Monster")) return; //return if not hitting a monster

        //Debug.Log("Hit Enemy");
        GameplayStats EnemyStats = other.gameObject.GetComponent<GameplayStats>();
        EnemyStats.TakeDamage(PlayerStats.GetAttackPower());
        EnemyHitEvent.Invoke();
    }

}
