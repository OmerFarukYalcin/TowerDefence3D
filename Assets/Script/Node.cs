using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    // Holds information about the turret building position on this node
    [Header("Turret buildPos")]
    public BuildingTurretPosition buildPos;

    // Colors for when the player hovers over the node
    [Header("Colors")]
    [SerializeField] Color hovorColor;         // Color when the player can build
    [SerializeField] Color notEnoughColor;     // Color when the player doesn't have enough money

    // References to the turret that is built on this node and its stats
    public GameObject turret;
    public TurretBuilding turretStats;

    // Boolean to track if the turret on this node has been upgraded
    public bool isUpgraded = false;

    // Reference to the node's renderer and its starting color
    private Renderer rend;
    private Color startColor;

    // Reference to the BuildManager instance
    BuildManager buildManager;

    // Called once when the object is initialized
    private void Start()
    {
        // Get the singleton instance of the BuildManager
        buildManager = BuildManager.instance;

        // Get the Renderer component of the node
        rend = GetComponent<Renderer>();

        // Store the initial color of the node
        startColor = rend.material.color;
    }

    // Called when the player clicks on the node
    private void OnMouseDown()
    {
        // If the mouse is over a UI element, do nothing
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        // If a turret is already placed on this node, select the node in the BuildManager
        if (turret != null)
        {
            buildManager.SelectNode(this);
            return;
        }

        // If there is no turret to build, do nothing
        if (!buildManager.CanBuild)
            return;

        // Build the turret on this node
        BuildTurret(buildManager.GetTurretToBuild());
    }

    // Returns the build position for the turret based on its name
    public Vector3 GetBuildPosition(string _name)
    {
        return transform.position + buildPos.GetOffsetPositionByName(_name);
    }

    // Method to build the turret on this node
    void BuildTurret(TurretBuilding _turret)
    {
        // Check if the player has enough money to build the turret
        if (PlayerStats.Money < _turret.cost)
        {
            Debug.Log("Not enough money!!!");
            return;
        }

        // Deduct the turret cost from the player's money
        PlayerStats.Money -= _turret.cost;

        // Instantiate the turret at the specified build position
        GameObject _Turret = Instantiate(_turret.prefab, GetBuildPosition(_turret.prefab.name), Quaternion.identity);
        turret = _Turret;

        // Store the turret stats
        turretStats = _turret;

        // Instantiate a build effect and destroy it after 3 seconds
        GameObject _buildEffect = Instantiate(buildManager.buildEffect, GetBuildPosition(_turret.prefab.name), Quaternion.identity);
        Destroy(_buildEffect, 3f);
        Debug.Log("Turret built! money left:" + PlayerStats.Money);
    }

    // Method to upgrade the turret on this node
    public void UpgradeTurret()
    {
        // Check if the player has enough money to upgrade the turret
        if (PlayerStats.Money < turretStats.upgradeCost)
        {
            Debug.Log("Not enough money!!!");
            return;
        }

        // Deduct the upgrade cost from the player's money
        PlayerStats.Money -= turretStats.upgradeCost;

        // Destroy the old turret
        Destroy(turret);

        // Instantiate the upgraded turret
        GameObject _Turret = Instantiate(turretStats.upgradedPrefab, GetBuildPosition(turretStats.prefab.name), Quaternion.identity);
        turret = _Turret;

        // Instantiate a build effect for the upgrade and destroy it after 3 seconds
        GameObject _buildEffect = Instantiate(buildManager.buildEffect, GetBuildPosition(turretStats.prefab.name), Quaternion.identity);
        Destroy(_buildEffect, 3f);

        // Mark the turret as upgraded
        isUpgraded = true;
        Debug.Log("Turret upgraded! money left:" + PlayerStats.Money);
    }

    // Method to sell the turret on this node
    public void SellTurret()
    {
        // Add the turret's sell amount back to the player's money
        PlayerStats.Money += turretStats.GetSellAmount();

        // Destroy the turret
        Destroy(turret);

        // Clear the turret stats
        turretStats = null;
    }

    // Called when the mouse hovers over the node
    private void OnMouseEnter()
    {
        // If the mouse is over a UI element, do nothing
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        // If there is no turret to build, do nothing
        if (!buildManager.CanBuild)
            return;

        // Change the node color based on whether the player can afford the turret
        if (buildManager.HasMoney)
        {
            rend.material.color = hovorColor;
        }
        else
        {
            rend.material.color = notEnoughColor;
        }
    }

    // Called when the mouse stops hovering over the node
    private void OnMouseExit()
    {
        // Reset the node's color to its original color
        rend.material.color = startColor;
    }
}
