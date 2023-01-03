using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    private GameManager gManager;
    public static int EnemiesAlive = 0;
    public Wave[] waves;
    [SerializeField] Transform spawnPoint;
    [SerializeField] TextMeshProUGUI waveCountDownText;
    [SerializeField] float timeBetweenSpawn = 5f;
    private float countDown = 2f;
    private int waveIndex = 0;
    void Start()
    {
        gManager = GetComponent<GameManager>();
    }

    void Update()
    {
        if (EnemiesAlive > 0)
        {
            return;
        }

        if (waveIndex == waves.Length && House.instance.GetCurretHealt() > 0)
        {
            gManager.WinLevel();
            this.enabled = false;
        }

        if (GameManager.gameEnded)
        {
            this.enabled = false;
            return;
        }

        if (countDown <= 0f)
        {
            StartCoroutine(spawnWave());
            countDown = timeBetweenSpawn;
            return;
        }
        countDown -= Time.deltaTime;
        countDown = Mathf.Clamp(countDown, 0f, Mathf.Infinity);
        waveCountDownText.text = string.Format("{0:00.00}", countDown);
    }

    IEnumerator spawnWave()
    {
        PlayerStats.Rounds++;
        Wave wave = waves[waveIndex];
        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.Enemy);
            yield return new WaitForSeconds(1f / wave.rate);
        }
        waveIndex++;
    }

    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
        EnemiesAlive++;
    }
}
