using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Reference to the pause panel (UI) that will be shown/hidden when the game is paused/unpaused
    [SerializeField] GameObject pausePanel;

    // Called once per frame to check for input
    void Update()
    {
        // Check if the Escape key is pressed to toggle the pause menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Toggle(); // Call the Toggle function to either show or hide the pause menu
        }
    }

    // Method to toggle the pause menu
    public void Toggle()
    {
        // Toggle the active state of the pause panel
        pausePanel.SetActive(!pausePanel.activeSelf);

        // If the pause panel is active, pause the game by setting Time.timeScale to 0
        if (pausePanel.activeSelf)
        {
            Time.timeScale = 0f; // Freeze the game
        }
        else
        {
            Time.timeScale = 1f; // Resume the game
        }
    }

    // Method to restart the level (retry)
    public void Retry()
    {
        Toggle(); // Close the pause menu and resume the game
        SceneManager.LoadScene(0); // Reload the first scene (index 0 in the build settings)
    }

    // Method to exit the game
    public void Exit()
    {
        Debug.Log("Exiting"); // Log the exit action to the console (for testing purposes)
        Application.Quit(); // Close the application (won't work in the editor)
    }
}
