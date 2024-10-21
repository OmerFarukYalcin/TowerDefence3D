using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    // Static variable to track the player's money (accessible globally)
    public static int Money;

    // Initial amount of money the player starts with
    [SerializeField] int startMoney = 400;

    // Reference to the UI element displaying the player's money
    [SerializeField] TextMeshProUGUI _moneyText;

    // Static variable to track the number of rounds completed (accessible globally)
    public static int Rounds;

    // Called when the game starts
    private void Start()
    {
        // Set the player's money to the starting value
        Money = startMoney;

        // Initialize the rounds to 0 at the start of the game
        Rounds = 0;
    }

    // Called once per frame to update the money text in the UI
    private void Update()
    {
        // Display the player's money in the format "$<amount>" in the UI
        _moneyText.text = "$" + Money;
    }
}
