using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBuilding standardTurret;
    public TurretBuilding missileTurret;
    public TurretBuilding laserTurret;
    BuildManager buildManager;

    private void Start()
    {
        buildManager = BuildManager.instance;
    }
    public void SelectStandardTurret()
    {
        buildManager.SelectTurretToBuild(standardTurret);
    }
    public void SelectMissileTurret()
    {
        buildManager.SelectTurretToBuild(missileTurret);
    }
    public void SelectLaserTurret()
    {
        buildManager.SelectTurretToBuild(laserTurret);
    }
}
