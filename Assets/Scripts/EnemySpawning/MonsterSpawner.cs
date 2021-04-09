using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject[] EnemyPrefabs;

    public SpawnVolume[] SpawnVolume;

    [SerializeField]
    private int EnemiesToSpawn;
    [SerializeField]
    private int EnemiesRemaining;
    [SerializeField]
    private float SpawnDelay;
    private float TimeSinceLastSpawn =0;
    private bool CanSpawn = true;
    private int WaveNumber = 0;

    [SerializeField]
    private GameHUDWidget GameHUD;

    // Start is called before the first frame update
    void Start()
    {
        GameHUD.UpdateWaveInfo(WaveNumber);
    }

    // Update is called once per frame
    void Update()
    {
        TimeSinceLastSpawn += Time.deltaTime;

        if (CanSpawn && TimeSinceLastSpawn >= SpawnDelay)
        {
            CanSpawn = false;
            WaveNumber++;
            GameHUD.UpdateWaveInfo(WaveNumber);
            for (int i = 0; i < EnemiesToSpawn; i++)
            {
                SpawnEnemy();
            }
        }
    }

    private void SpawnEnemy()
    {
        GameObject EnemyToSpawn = EnemyPrefabs[Random.Range(0, EnemyPrefabs.Length)];
        SpawnVolume spawnVolume = SpawnVolume[Random.Range(0, SpawnVolume.Length)];

        GameObject enemy = Instantiate(EnemyToSpawn, spawnVolume.GetPositionInBounds(), spawnVolume.transform.rotation);
        GameplayStats monsterStats = enemy.GetComponent<GameplayStats>();
        monsterStats.DeathEvent.AddListener(ReduceNumActiveEnemies);
        MonsterBehavior monsterBehavior = enemy.GetComponent<MonsterBehavior>();
        monsterBehavior.ModifyDifficulty(WaveNumber);
        EnemiesRemaining++;

    }

    private void ReduceNumActiveEnemies()
    {
        EnemiesRemaining--;
        if (EnemiesRemaining <= 0)
        {
            CanSpawn = true;
            //can move to next wave
            TimeSinceLastSpawn = 0;
        }
    }

    public void ResetGame()
    {
        GameObject[] monstersInGame = GameObject.FindGameObjectsWithTag("Monster");
        foreach (GameObject monster in monstersInGame)
        {
            Destroy(monster);
        }
        EnemiesRemaining = 0;
        CanSpawn = true;
        TimeSinceLastSpawn = 0;
        WaveNumber = 0;
        GameHUD.UpdateWaveInfo(WaveNumber);
    }

    public int GetCurrentWave()
    {
        return WaveNumber;
    }
}
