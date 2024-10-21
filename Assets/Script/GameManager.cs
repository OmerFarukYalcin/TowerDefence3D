using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // A flag to track whether the game has ended
    public static bool gameEnded;

    // References to the Game Over UI and Level Complete UI
    [SerializeField] GameObject gameOverUI;
    [SerializeField] GameObject LevelCompleteCanvas;

    // This method is called at the start of the game
    private void Start()
    {
        // Initialize gameEnded as false since the game has not ended
        gameEnded = false;
    }

    // This method is called once per frame
    private void Update()
    {
        // If the game has already ended, stop further execution
        if (gameEnded)
            return;

        // Check if the player's base (House) has zero or less health
        if (House.instance.GetCurretHealt() <= 0f)
        {
            // End the game if the house's health is depleted
            EndGame();
        }

        // If the player presses the 'E' key, manually end the game (for testing or shortcut)
        if (Input.GetKeyDown(KeyCode.E))
        {
            EndGame();
        }
    }

    // Method to restart the level (Retry the game)
    public void Retry()
    {
        // Reload the first scene (index 0 in the build settings)
        SceneManager.LoadScene(0);
    }

    // Method to handle game over logic
    void EndGame()
    {
        // Set the gameEnded flag to true
        gameEnded = true;

        // Show the Game Over UI
        gameOverUI.SetActive(true);
    }

    // Method to handle level completion logic
    public void WinLevel()
    {
        // Set the gameEnded flag to true
        gameEnded = true;

        // Show the Level Complete UI
        LevelCompleteCanvas.SetActive(true);
    }
}
