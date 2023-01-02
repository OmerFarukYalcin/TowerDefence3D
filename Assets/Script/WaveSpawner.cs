using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;
    public Transform spawnPoint;
    public TextMeshProUGUI waveCountDownText;
    public float timeBetweenSpawn = 5f;
    private float countDown = 2f;
    private int waveIndex = 1;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (countDown <= 0f)
        {
            StartCoroutine(spawnWave());
            countDown = timeBetweenSpawn;
        }
        countDown -= Time.deltaTime;
        countDown = Mathf.Clamp(countDown, 0f, Mathf.Infinity);
        waveCountDownText.text = string.Format("{0:00.00}", countDown);
    }

    IEnumerator spawnWave()
    {
        waveIndex++;
        for (int i = 0; i < waveIndex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
