using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    [Header("Turret buildPos")]
    public BuildingTurretPosition buildPos;
    [Header("Colors")]
    [SerializeField] Color hovorColor;
    [SerializeField] Color notEnoughColor;
    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBuilding turretStats;
    [HideInInspector]
    public bool isUpgraded = false;
    private Renderer rend;
    private Color startColor;
    BuildManager buildManager;

    private void Start()
    {
        buildManager = BuildManager.instance;
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
    }

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (turret != null)
        {
            buildManager.SelectNode(this);
            return;
        }

        if (!buildManager.CanBuild)
            return;

        BuildTurret(buildManager.GetTurretToBuild());
    }

    public Vector3 GetBuildPosition(string _name)
    {
        return transform.position + buildPos.GetOffsetPositionByName(_name);
    }

    void BuildTurret(TurretBuilding _turret)
    {
        if (PlayerStats.Money < _turret.cost)
        {
            Debug.Log("Not enough money!!!");
            return;
        }
        PlayerStats.Money -= _turret.cost;

        GameObject _Turret = Instantiate(_turret.prefab, GetBuildPosition(_turret.prefab.name), Quaternion.identity);
        turret = _Turret;

        turretStats = _turret;

        GameObject _buildEffect = Instantiate(buildManager.buildEffect, GetBuildPosition(_turret.prefab.name), Quaternion.identity);
        Destroy(_buildEffect, 3f);
        Debug.Log("Turret build! money left:" + PlayerStats.Money);
    }

    public void UpgradeTurret()
    {
        if (PlayerStats.Money < turretStats.upgradeCost)
        {
            Debug.Log("Not enough money!!!");
            return;
        }
        PlayerStats.Money -= turretStats.upgradeCost;

        Destroy(turret);

        GameObject _Turret = Instantiate(turretStats.upgradedPrefab, GetBuildPosition(turretStats.prefab.name), Quaternion.identity);
        turret = _Turret;

        GameObject _buildEffect = Instantiate(buildManager.buildEffect, GetBuildPosition(turretStats.prefab.name), Quaternion.identity);
        Destroy(_buildEffect, 3f);

        isUpgraded = true;
        Debug.Log("Turret build! money left:" + PlayerStats.Money);
    }

    public void SellTurret()
    {
        PlayerStats.Money += turretStats.GetSellAmount();
        Destroy(turret);
        turretStats = null;
    }

    private void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!buildManager.CanBuild)
            return;

        if (buildManager.HasMoney)
        {
            rend.material.color = hovorColor;
        }
        else
        {
            rend.material.color = notEnoughColor;
        }
    }

    private void OnMouseExit()
    {
        rend.material.color = startColor;
    }
}
