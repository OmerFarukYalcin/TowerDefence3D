using UnityEngine;

public class Shop : MonoBehaviour
{
    // References to the different turret types that the player can purchase
    public TurretBuilding standardTurret;
    public TurretBuilding missileTurret;
    public TurretBuilding laserTurret;

    // Reference to the BuildManager to handle turret placement
    BuildManager buildManager;

    // Called when the script instance is being loaded
    private void Start()
    {
        // Get the singleton instance of the BuildManager
        buildManager = BuildManager.instance;
    }

    // Method to select the standard turret to build
    public void SelectStandardTurret()
    {
        // Tell the BuildManager to select the standard turret for building
        buildManager.SelectTurretToBuild(standardTurret);
    }

    // Method to select the missile turret to build
    public void SelectMissileTurret()
    {
        // Tell the BuildManager to select the missile turret for building
        buildManager.SelectTurretToBuild(missileTurret);
    }

    // Method to select the laser turret to build
    public void SelectLaserTurret()
    {
        // Tell the BuildManager to select the laser turret for building
        buildManager.SelectTurretToBuild(laserTurret);
    }
}
