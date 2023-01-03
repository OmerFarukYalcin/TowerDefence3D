using UnityEngine;

public class BuildManager : MonoBehaviour
{
    [SerializeField] NodeUI nodeUI;
    private TurretBuilding turretToBuild;
    private Node selectedNode;
    public GameObject buildEffect;
    public bool CanBuild { get { return turretToBuild != null; } }
    public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }

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

    public TurretBuilding GetTurretToBuild()
    {
        return turretToBuild;
    }

    public void SelectNode(Node _selectNode)
    {
        if (selectedNode == _selectNode)
        {
            DeselectNode();
            return;
        }
        selectedNode = _selectNode;
        turretToBuild = null;

        nodeUI.SetTarget(_selectNode);
    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }

    public void SelectTurretToBuild(TurretBuilding _turretBuild)
    {
        turretToBuild = _turretBuild;
        DeselectNode();

    }


}