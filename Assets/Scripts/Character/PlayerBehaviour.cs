using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{

    private GameplayStats PlayerStats;

    private void Awake()
    {
        PlayerStats = GetComponent<GameplayStats>();
        PlayerStats.DeathEvent.AddListener(OnDeath);
    }

    private void OnDisable()
    {
        PlayerStats.DeathEvent.RemoveListener(OnDeath);
    }

    private void OnDeath()
    {
        Debug.Log("Player Died");
    }
}
