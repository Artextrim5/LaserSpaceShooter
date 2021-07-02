using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpouner : MonoBehaviour
{
    [SerializeField] List<WaveConfig> waveConfigs;
    [SerializeField] int startingWave = 0;
    [SerializeField] bool looping = false; // переменная для зацикливания

    // Start is called before the first frame update
    IEnumerator Start() // метод старт возвращает корутину
    {
        do
        {
            yield return StartCoroutine(SpawnAllWaves()); // повторяем запуск корутины, пока looping правда
        }
        while (looping);
    }

    private IEnumerator SpawnAllWaves()
    {
        for ( int waveNumber = startingWave; waveNumber < waveConfigs.Count; waveNumber++)
        {
            var currentWave = waveConfigs[waveNumber];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }

    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        for (int enemyCount = 0; enemyCount < waveConfig.GetNumberOfEnemies(); enemyCount++)
        {
            var newEneme = Instantiate(waveConfig.GetEnemyPrefab(), waveConfig.GetWayPoints()[0].transform.position, Quaternion.identity);
            newEneme.GetComponent<EnemyPath>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(waveConfig.GetTimeBrtweenSpawns());
        }
    }
}
 