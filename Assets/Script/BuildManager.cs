using UnityEngine;

public class BuildManager : MonoBehaviour
{
    // Reference to the UI that handles interactions with nodes (like upgrading or selling turrets)
    [SerializeField] NodeUI nodeUI;

    // The type of turret currently selected to build
    private TurretBuilding turretToBuild;

    // The currently selected node where the player may build or upgrade a turret
    private Node selectedNode;

    // The effect that plays when a turret is built
    public GameObject buildEffect;

    // Property to check if a turret is selected to be built
    public bool CanBuild { get { return turretToBuild != null; } }

    // Property to check if the player has enough money to build the selected turret
    public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }

    // Singleton instance of the BuildManager
    public static BuildManager instance;

    private void Awake()
    {
        // Ensure that only one instance of BuildManager exists
        if (instance != null)
        {
            Destroy(gameObject); // Destroy any duplicate instances
        }
        else
        {
            instance = this; // Assign the singleton instance
        }
    }

    // Returns the currently selected turret to be built
    public TurretBuilding GetTurretToBuild()
    {
        return turretToBuild;
    }

    // Select a node where a turret may be built or upgraded
    public void SelectNode(Node _selectNode)
    {
        // If the node selected is already selected, deselect it
        if (selectedNode == _selectNode)
        {
            DeselectNode();
            return;
        }

        // Set the selected node and clear the current turret to build
        selectedNode = _selectNode;
        turretToBuild = null;

        // Display the node UI with the selected node as the target
        nodeUI.SetTarget(_selectNode);
    }

    // Deselects the currently selected node and hides the node UI
    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide(); // Hide the UI since no node is selected
    }

    // Selects a turret type to be built and deselects the node
    public void SelectTurretToBuild(TurretBuilding _turretBuild)
    {
        turretToBuild = _turretBuild; // Assign the selected turret to build
        DeselectNode(); // Deselect the node when a turret is selected to build
    }
}
