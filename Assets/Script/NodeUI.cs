using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour
{
    [SerializeField] Text UpgradeCost;
    [SerializeField] Text SellAmount;
    [SerializeField] GameObject UI;
    [SerializeField] Button upgradeButton;
    private Node target;
    public void SetTarget(Node _target)
    {
        target = _target;
        transform.position = target.GetBuildPosition(_target.turret.name);

        if (!target.isUpgraded)
        {
            UpgradeCost.text = "Upgrade $" + target.turretStats.upgradeCost;
            upgradeButton.interactable = true;
        }
        else
        {
            UpgradeCost.text = "DONE";
            upgradeButton.interactable = false;
        }
        SellAmount.text = "Sell $" + target.turretStats.GetSellAmount();

        UI.SetActive(true);
    }

    public void Hide()
    {
        UI.SetActive(false);
    }

    public void Upgrade()
    {
        target.UpgradeTurret();
        BuildManager.instance.DeselectNode();
    }

    public void Sell()
    {
        target.SellTurret();
        BuildManager.instance.DeselectNode();
    }
}
