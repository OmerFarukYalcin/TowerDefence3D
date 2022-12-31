using UnityEngine;

public class BuildManager : MonoBehaviour
{
    private TurretBuilding turretToBuild;
    public GameObject standardTurret;
    public GameObject missileTurretPrefab;
    public bool CanBuild { get { return turretToBuild != null; } }

    public static BuildManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

    }

    public void BuildTurretOn(Node node)
    {
        if (PlayerStats.Money < turretToBuild.cost)
        {
            Debug.Log("Not enough money!!!");
            return;
        }
        PlayerStats.Money -= turretToBuild.cost;

        GameObject turret = Instantiate(turretToBuild.prefab, node.GetBuildPosition(turretToBuild.prefab.name), Quaternion.identity);
        node.turret = turret;

        Debug.Log("Turret build! money left:" + PlayerStats.Money);
    }

    public void SelectTurretToBuild(TurretBuilding _turretBuild)
    {
        turretToBuild = _turretBuild;
    }


}