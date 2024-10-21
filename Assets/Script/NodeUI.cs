using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    // References to the UI elements for upgrade cost, sell amount, and the main UI
    [SerializeField] Text UpgradeCost;
    [SerializeField] Text SellAmount;
    [SerializeField] GameObject UI;
    [SerializeField] Button upgradeButton;

    // The node currently being interacted with
    private Node target;

    // Method to set the target node and update the UI based on the selected node
    public void SetTarget(Node _target)
    {
        // Set the target to the passed-in node
        target = _target;

        // Set the position of the UI to the turret's position on the selected node
        transform.position = target.GetBuildPosition(_target.turret.name);

        // If the turret on the node hasn't been upgraded yet, display the upgrade cost
        if (!target.isUpgraded)
        {
            UpgradeCost.text = "Upgrade $" + target.turretStats.upgradeCost;
            upgradeButton.interactable = true;  // Enable the upgrade button
        }
        else
        {
            // If the turret has already been upgraded, show "DONE" and disable the upgrade button
            UpgradeCost.text = "DONE";
            upgradeButton.interactable = false; // Disable the upgrade button
        }

        // Update the sell amount in the UI
        SellAmount.text = "Sell $" + target.turretStats.GetSellAmount();

        // Show the UI
        UI.SetActive(true);
    }

    // Method to hide the UI
    public void Hide()
    {
        UI.SetActive(false);
    }

    // Method to upgrade the turret on the target node
    public void Upgrade()
    {
        target.UpgradeTurret(); // Upgrade the turret
        BuildManager.instance.DeselectNode(); // Deselect the node after upgrading
    }

    // Method to sell the turret on the target node
    public void Sell()
    {
        target.SellTurret(); // Sell the turret
        BuildManager.instance.DeselectNode(); // Deselect the node after selling
    }
}
