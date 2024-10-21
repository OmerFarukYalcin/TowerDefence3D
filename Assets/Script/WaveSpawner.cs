using System.Collections;
using UnityEngine;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    // Reference to the GameManager to manage game states
    private GameManager gManager;

    // Tracks the number of enemies currently alive in the scene
    public static int EnemiesAlive = 0;

    // Array of waves that define which enemies to spawn, how many, and at what rate
    public Wave[] waves;

    // Transform that determines where the enemies will spawn
    [SerializeField] Transform spawnPoint;

    // UI text element to show the countdown to the next wave
    [SerializeField] TextMeshProUGUI waveCountDownText;

    // Time in seconds between each wave
    [SerializeField] float timeBetweenSpawn = 5f;

    // Countdown timer for the next wave to spawn
    private float countDown = 2f;

    // Index to track which wave is currently being spawned
    private int waveIndex = 0;

    // Called at the start of the game
    void Start()
    {
        // Get reference to the GameManager component
        gManager = GetComponent<GameManager>();
    }

    // Called once per frame to update the wave countdown and spawn logic
    void Update()
    {
        // If there are still enemies alive, do not spawn the next wave
        if (EnemiesAlive > 0)
        {
            return;
        }

        // If all waves are completed and the player's base (House) is still alive, trigger a win
        if (waveIndex == waves.Length && House.instance.GetCurretHealt() > 0)
        {
            gManager.WinLevel(); // Call WinLevel method from GameManager
            this.enabled = false; // Disable this script to stop further updates
        }

        // If the game has ended, stop wave spawning
        if (GameManager.gameEnded)
        {
            this.enabled = false;
            return;
        }

        // If the countdown reaches zero, start spawning the next wave
        if (countDown <= 0f)
        {
            StartCoroutine(spawnWave()); // Coroutine to spawn enemies over time
            countDown = timeBetweenSpawn; // Reset countdown for the next wave
            return;
        }

        // Decrease the countdown timer
        countDown -= Time.deltaTime;

        // Clamp the countdown so it never goes below 0
        countDown = Mathf.Clamp(countDown, 0f, Mathf.Infinity);

        // Update the UI text to display the remaining time until the next wave spawns
        waveCountDownText.text = string.Format("{0:00.00}", countDown);
    }

    // Coroutine to spawn a wave of enemies
    IEnumerator spawnWave()
    {
        // Increment the round counter in PlayerStats
        PlayerStats.Rounds++;

        // Get the current wave to spawn
        Wave wave = waves[waveIndex];

        // Loop to spawn the number of enemies specified in the wave
        for (int i = 0; i < wave.count; i++)
        {
            SpawnEnemy(wave.Enemy); // Spawn an individual enemy
            yield return new WaitForSeconds(1f / wave.rate); // Wait between spawns based on the rate
        }

        // Move to the next wave after the current one is finished
        waveIndex++;
    }

    // Method to spawn a single enemy at the spawn point
    void SpawnEnemy(GameObject enemy)
    {
        // Instantiate the enemy at the spawn point's position and rotation
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);

        // Increment the number of enemies alive
        EnemiesAlive++;
    }
}
