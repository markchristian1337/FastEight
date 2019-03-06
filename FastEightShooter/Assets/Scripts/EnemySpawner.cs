using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] GameObject bossPrefab;
    [SerializeField] GameObject bossPathPrefab;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool looping = false;
    [SerializeField] int numberOfWaves = 3;

    int waveCount = 0;
    private bool inMainGame = false;
    private bool inTransition = false;
    private bool inBossFight = false;

    public bool InTransition
    {
        get
        {
            return inTransition;
        }

        set
        {
            inTransition = value;
        }
    }

    public bool InMainGame
    {
        get
        {
            return inMainGame;
        }

        set
        {
            inMainGame = value;
        }
    }

    public bool InBossFight
    {
        get
        {
            return inBossFight;
        }

        set
        {
            inBossFight = value;
        }
    }

    // Use this for initialization
    IEnumerator Start () {
        InMainGame = true;
        while (looping)
        {
            yield return StartCoroutine(SpawnAllWaves(numberOfWaves, waveCount, looping));
        }
        

    }


    public List<Transform> GetBossWaypoints()
    {
        var waveWaypoints = new List<Transform>();
        foreach (Transform child in bossPathPrefab.transform)
        {
            waveWaypoints.Add(child);
        }

        return waveWaypoints;
    }
    private IEnumerator SpawnAllWaves(int numOfWaves, int currentWaveCount, bool waveLoopBool)
    {
        while (waveCount <= numOfWaves)
        {
            Debug.Log("waveCount: " + waveCount);
            for (int waveIndex = startingWave; waveIndex < waveConfigs.Count; waveIndex++)
            {
                var currentWave = waveConfigs[waveIndex];
                yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));

            }
            waveCount += 1;
        }
        StartCoroutine(WaitAndSpawn(4f));
        looping = false;
        InMainGame = false;
    }

    IEnumerator WaitAndSpawn(float delay)
    {
        yield return new WaitForSeconds(delay);
        SpawnBoss();
    }

    private void SpawnBoss()
    {
        var boss = Instantiate(bossPrefab,GetBossWaypoints()[0].transform.position, Quaternion.identity);
    }


    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        for (int enemyCount = 0; enemyCount < waveConfig.GetNumberOfEnemies(); enemyCount++)
        {

            var newEnemy = Instantiate(waveConfig.GetEnemyPrefab(), waveConfig.GetWaypoints()[0].transform.position, Quaternion.identity);
            newEnemy.GetComponent<EnemyPathing>().SetWaveConfig(waveConfig); 
            yield return new WaitForSeconds(waveConfig.GetTimeBetweenSpawns());
        }
    }
}
