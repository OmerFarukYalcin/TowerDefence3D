using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool gameEnded;
    [SerializeField] GameObject gameOverUI;
    [SerializeField] GameObject LevelCompleteCanvas;
    private void Start()
    {
        gameEnded = false;
    }
    private void Update()
    {
        if (gameEnded)
            return;
        if (House.instance.GetCurretHealt() <= 0f)
        {
            EndGame();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            EndGame();
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene(0);
    }

    void EndGame()
    {
        gameEnded = true;
        gameOverUI.SetActive(true);
    }

    public void WinLevel()
    {
        gameEnded = true;
        LevelCompleteCanvas.SetActive(true);
    }
}