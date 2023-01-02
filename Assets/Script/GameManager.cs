using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool gameEnded = false;
    private void Update()
    {
        if (gameEnded)
            return;
        if (House.instance.GetCurretHealt() <= 0f)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        gameEnded = true;
        Debug.Log("Game Over!");
    }
}