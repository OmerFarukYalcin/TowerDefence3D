using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class roundsSurvived : MonoBehaviour
{
    // Reference to the UI text element that will display the rounds survived
    [SerializeField] Text roundsText;

    // Called when the object is enabled, for example when the game is over or the player reaches the end of a level
    private void OnEnable()
    {
        // Start the coroutine to animate the text showing the rounds survived
        StartCoroutine(AnimateText());
    }

    // Coroutine to animate the rounds survived count in the UI
    IEnumerator AnimateText()
    {
        // Initialize the text to display "0" at the beginning
        roundsText.text = "0";

        // Initialize a local variable to track the current round being displayed
        int round = 0;

        // Wait for 0.7 seconds before starting the animation
        yield return new WaitForSeconds(.7f);

        // Loop to animate the incrementing rounds count, up to the number of rounds completed by the player
        while (round < PlayerStats.Rounds)
        {
            round++; // Increment the local round counter

            // Update the text to show the current round number
            roundsText.text = round.ToString();

            // Wait for 0.05 seconds between each increment to create the animation effect
            yield return new WaitForSeconds(.05f);
        }
    }
}
